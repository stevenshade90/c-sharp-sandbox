using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace File_Deduplicator.Methods
{
    sealed class DisplayMethods
    {
        public static void DisplayMessageGeneric<T>(T value1, string text2 = "")
        {
            if (value1 is List<FileInfo> listValues)
            {
                if (listValues.Count == 0)
                {
                    Console.WriteLine("\tNo duplicate files found.");
                    Environment.Exit(0);
                }
                else
                {
                    listValues.ForEach(f => Console.WriteLine($"\t{f.FullName}"));
                }
            }
            else if (value1 is IDictionary<string, ConcurrentBag<FileInfo>> convertedData)
            {
                foreach (KeyValuePair<string, ConcurrentBag<FileInfo>> kvp in convertedData)
                {
                    string hashString = kvp.Key;
                    ConcurrentBag<FileInfo> files = kvp.Value;

                    if (files.Count <= 1) continue;

                    Console.WriteLine($"Hash: {hashString}");
                    foreach (FileInfo file in files)
                    {
                        Console.WriteLine($"\tFile: {file.FullName}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine(convertedData.Count());
            }
            else if (value1 is string text1)
            {
                Console.WriteLine(string.IsNullOrEmpty(text2)
                    ? $"********** {text1} **********"
                    : $"\t{text1}\n\t{text2}");
            }
        }

        public static string DisplayProcessedException(Exception ex, string additionalInfo)
        {
            return ex switch
            {
                UnauthorizedAccessException => ConversionMethods.ConvertTextToSubstring($"Access denied: {additionalInfo}"),
                IOException => ConversionMethods.ConvertTextToSubstring($"I/O Error: {additionalInfo}"),
                _ => ConversionMethods.ConvertTextToSubstring($"Unexpected error: {additionalInfo}")
            };
        }

        [Obsolete("Currently unused")]
        public static void DisplayLoadingImage(Task awaitingTask)
        {
            int iterator = 1;
            Console.ForegroundColor = ConsoleColor.Green;
            char[] loading = { '|', '/', '-', '\\' };

            while (!awaitingTask.IsCompleted)
            {
                Console.Write("\rLoading information... {0}", loading[iterator % 4]);
                iterator++;
                Thread.Sleep(100);
            }
            Console.ResetColor();
        }
    }

    sealed class ReadMethods
    {
        public static async Task<IList<FileInfo>> ReadDirectoriesAsync(DirectoryInfo[] directories)
        {
            ConcurrentBag<FileInfo> allFiles = new ConcurrentBag<FileInfo>();
            ParallelOptions parOpts = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };

            await Parallel.ForEachAsync(directories, parOpts, async (directory, _) =>
            {
                try
                {
                    FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories).ToArray();
                    await Parallel.ForEachAsync(files, parOpts, async (file, _) =>
                    {
                        allFiles.Add(file);
                    });
                }
                catch (Exception ex)
                {
                    string message = $"{ConversionMethods.ConvertTextToSubstring(directory)} -- Exception: {ex.Message}";
                    Console.WriteLine(DisplayMethods.DisplayProcessedException(ex, message));
                }
            });
            DisplayMethods.DisplayMessageGeneric("Done processing all files!", $"Total Files: {allFiles.Count(): #,###}\n");
            await Task.Delay(3000);

            return allFiles.ToList();
        }
    }

    sealed class ConversionMethods
    {
        private const long BYTES_IN_MB = 1024 * 1024;
        private const long BYTES_IN_GB = 1024 * 1024 * 1024;

        public static async Task<IDictionary<string, ConcurrentBag<FileInfo>>> ConvertFileToHashStringAsync(IEnumerable<FileInfo> filesToConvert)
        {
            ConcurrentDictionary<string, ConcurrentBag<FileInfo>> fileAndHashString = new ConcurrentDictionary<string, ConcurrentBag<FileInfo>>();
            ParallelOptions parOpts = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };

            await Parallel.ForEachAsync(filesToConvert, parOpts, async (file, _) =>
            {
                try
                {
                    if (file.Attributes.HasFlag(FileAttributes.Offline)) return;
                    if (file.GetType() != typeof(FileInfo)) return;

                    using (FileStream fs = File.OpenRead(file.FullName))
                    {
                        byte[] hashBytes = SHA256.HashData(fs);
                        string hashString = Convert.ToHexString(hashBytes);

                        ConcurrentBag<FileInfo> filebag = fileAndHashString.GetOrAdd(hashString, _ => new ConcurrentBag<FileInfo>());
                        filebag.Add(file);
                    }
                }
                catch (Exception ex)
                {
                    string message = $"{ConversionMethods.ConvertTextToSubstring(file)} -- Exception: {ex.Message}";
                    Console.WriteLine(DisplayMethods.DisplayProcessedException(ex, message));
                }
            });
            Console.WriteLine("\tFinished converting files to Hash Strings\n");
            return fileAndHashString.ToDictionary();
        }

        public static string ConvertTextToSubstring<T>(T parameterValue)
        {
            StringBuilder sb = new StringBuilder();
            if (parameterValue is FileSystemInfo file)
            {
                sb.Append(file.FullName.Length >= 30
                    ? file.FullName.Substring(0, 30) + "..."
                    : file.FullName);
            }
            else if (parameterValue is string text)
            {
                sb.Append(text.Length >= Console.WindowWidth
                    ? text.Substring(0, Console.WindowWidth - 3) + "..."
                    : text);
            }
            return sb.ToString();
        }

        public static string ConvertBytes(long bytes)
        {
            return bytes switch
            {
                >= BYTES_IN_GB => $"{bytes / (double)BYTES_IN_GB:F2} GB",
                >= BYTES_IN_MB => $"{bytes / (double)BYTES_IN_MB:F2} MB",
                _ => $"{bytes:#,###} bytes"
            };
        }
    }

    sealed class DeleteMethods
    {
    }
}