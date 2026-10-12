using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace File_Deduplicator.Methods
{
    sealed class DisplayMethods
    {
        public static void DisplaySectionHeader(string text)
        {
            Console.WriteLine($"********** {text} **********");
        }

        public static async Task GetDriveInfo(DriveInfo[] drives)
        {
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Drive: {drive.Name}");
                Console.WriteLine($"Drive Type: {drive.DriveType}");

                if (drive.IsReady)
                {
                    Console.WriteLine($"\tVolume Label: {drive.VolumeLabel}");
                    Console.WriteLine($"\tFile System: {drive.DriveFormat}");
                    Console.WriteLine($"\tAvailable Space: {drive.AvailableFreeSpace / Math.Pow(1024, 3):F2} GB");
                    Console.WriteLine($"\tTotal Size: {drive.TotalSize / Math.Pow(1024, 3):F2} GB");
                }
                else
                {
                    Console.WriteLine("Drive is not ready.");
                }
                Console.WriteLine();
            }
            await Task.Delay(5000);
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
                FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories);

                await Parallel.ForEachAsync(files, parOpts, async (file, _) =>
                {
                    allFiles.Add(file);
                });
            });
            Console.WriteLine("Done processing all files!");
            Console.WriteLine($"Total Files: {allFiles.Count()}");

            return allFiles.ToList();
        }

        public static void ReadConvertedData(Task<IDictionary<FileInfo, string>> convertedData)
        {
            foreach (KeyValuePair<FileInfo, string> kvp in convertedData.Result)
            {
                FileInfo file = kvp.Key;
                string hashString = kvp.Value;

                Console.WriteLine($"File: {file.FullName} -- Hash: {hashString}");
                Console.WriteLine();
            }
        }
    }

    sealed class ConversionMethods
    {
        private const long BYTES_IN_MB = 1024 * 1024;
        private const long BYTES_IN_GB = 1024 * 1024 * 1024;

        public static async Task<IDictionary<FileInfo, string>> ConvertFileToHashStringAsync(IEnumerable<FileInfo> filesToConvert)
        {
            /* 
             * Gathering FileInfo object and the file's hashString to use for comparison later to
             * see if files are duplicates
             */
            ConcurrentDictionary<FileInfo, string> fileAndHashString = new ConcurrentDictionary<FileInfo, string>();
            ParallelOptions parOpts = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };

            await Parallel.ForEachAsync(filesToConvert, parOpts, async (file, _) =>
            {
                // Will need to update the method to accept files > 2GB in size, but for now skip them 
                if (!(file.Length > (BYTES_IN_GB * 2)))
                {
                    /********************************************************************************************
                     * Is this reading and opening the file, causing the cloud downloading issue? FileStream?   *
                     ********************************************************************************************/
                    byte[] dataBytes = File.ReadAllBytes(file.FullName); 
                    byte[] hashBytes = SHA256.HashData(dataBytes);
                    string hashString = Convert.ToHexString(hashBytes); // COMPARE ON THIS LATER

                    fileAndHashString[file] = hashString;
                }
            });
            return fileAndHashString.ToDictionary();
        }

        public static string BytesConversion(long bytes)
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