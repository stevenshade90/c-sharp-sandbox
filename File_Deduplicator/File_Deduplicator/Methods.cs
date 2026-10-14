using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace File_Deduplicator.Methods
{
    sealed class DisplayMethods
    {
        public static void DisplayMessage(string text1, string text2="")
        {
            Console.WriteLine(string.IsNullOrEmpty(text2) 
                ? $"********** {text1} **********"
                : text1 + "\n" + text2);
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
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ConversionMethods.ConvertTextToSubstring($"Access denied to directory: {ConversionMethods.ConvertTextToSubstring(directory)} -- Exception: {ex.Message}"));
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ConversionMethods.ConvertTextToSubstring($"I/O error with directory: {ConversionMethods.ConvertTextToSubstring(directory)} -- Exception: {ex.Message}"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ConversionMethods.ConvertTextToSubstring($"Unexpected error with directory: {ConversionMethods.ConvertTextToSubstring(directory)} -- Exception: {ex.Message}"));
                }
            });
            DisplayMethods.DisplayMessage("Done processing all files!", $"Total Files: {allFiles.Count(): #,###}\n");
            await Task.Delay(3000);

            return allFiles.ToList();
        }

        public static void ReadConvertedData(IDictionary<string, ConcurrentBag<FileInfo>> convertedData)
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
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ConvertTextToSubstring($"Access denied to file: {ConvertTextToSubstring(file)} -- Exception: {ex.Message}"));
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ConvertTextToSubstring($"I/O error with file: {ConvertTextToSubstring(file)} -- Exception: {ex.Message}"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ConvertTextToSubstring($"Unexpected error with file: {ConvertTextToSubstring(file)} -- Exception: {ex.Message}"));
                }
            });
            return fileAndHashString.ToDictionary();
        }

        public static string ConvertTextToSubstring(object parameterValue)
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
}