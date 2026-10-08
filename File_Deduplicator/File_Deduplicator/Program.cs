using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace File_Deduplicator
{
    internal class Program
    {
        public const long BYTES_IN_MB = 1024 * 1024;
        public const long BYTES_IN_GB = 1024 * 1024 * 1024;

        static async Task Main(string[] args)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            DirectoryInfo dir1 = new DirectoryInfo(@"C:\Users\steve\Downloads");
            DirectoryInfo[] directories = dir1.GetDirectories();

            DisplaySectionHeader("Drive Information");
            await GetDriveInfo(drives);


            DisplaySectionHeader("Gathering All Files");
            ConcurrentBag<FileInfo> allFilesBag = await ReadDirectoriesAsync(directories); // Contains every file from all directories 
            List<FileInfo> filesConvertedToList = allFilesBag.ToList();

            Dictionary<FileInfo, string> convertedData = ConvertData(filesConvertedToList);

            /*
             * 
             * to continue from here
             * 
             */

            List<FileInfo> distinctFiles = new List<FileInfo>(); // Contains only the distinct files from allFiles

            List<FileInfo> duplicatesToRemove = new List<FileInfo>(allFilesBag);

            //Console.WriteLine("\n");
            //Console.WriteLine("*********************************************");
            //Console.WriteLine("All your files!");
            //Console.WriteLine($"Count: {allFilesBag.Count()}");

            //int i = 1;
            foreach (FileInfo file in allFilesBag.DistinctBy(f => f.Name).OrderBy(f => f.Length))
            {
                distinctFiles.Add(file);
                //Console.WriteLine($"File {i}: {file.FullName} (Size: {BytesConversion(file.Length)})");
                //i++;
            }

            Console.WriteLine($"Total size: {BytesConversion(allFilesBag.Sum(f => f.Length))}");
            //Console.WriteLine($"Distinct Files ({distinctFiles.Count()})");
            //distinctFiles.ForEach(f => Console.WriteLine(f.Name));

            distinctFiles.ForEach(f =>
            {
                if (duplicatesToRemove.Contains(f))
                {
                    duplicatesToRemove.Remove(f);
                }
            });

            Console.WriteLine("Viewing remaining files...");
            Console.WriteLine($"Amount: {duplicatesToRemove.Count}");
            Console.WriteLine($"Storage Saved: {BytesConversion(duplicatesToRemove.Sum(f => f.Length))}");
            duplicatesToRemove.ForEach(f => Console.WriteLine($"\tDuplicate File: {f.FullName} (Size: {BytesConversion(f.Length)})"));
        }

        static string DisplaySectionHeader(string text) => $"********** {text} **********";

        static async Task GetDriveInfo(DriveInfo[] drives)
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

        static async Task<ConcurrentBag<FileInfo>> ReadDirectoriesAsync(DirectoryInfo[] directories)
        {
            //int runningTotal = 0;
            ConcurrentBag<FileInfo> allFiles = new ConcurrentBag<FileInfo>();
            ParallelOptions parOpts = new ParallelOptions() { MaxDegreeOfParallelism = 4 };

            // switch to parallel.foreachasync
            foreach (DirectoryInfo directory in directories)
            {
                FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories);

                //Console.WriteLine($"Current Directory: {directory.FullName}");
                //Console.WriteLine($"Number of files: {files.Count()}");

                //int i = 1;
                foreach (FileInfo file in files)
                {
                    allFiles.Add(file);

                    //Console.WriteLine($"\tFile {i++}: {file.Name} (Size: {BytesConversion(file.Length)})");
                }
                //runningTotal += files.Count();
                //Console.WriteLine();
            }
            //Console.WriteLine($"Total number of files: {runningTotal}");
            return allFiles;
        }

        static Dictionary<FileInfo, string> ConvertData(List<FileInfo> filesToConvert)
        {
            /* 
             * Gathering FileInfo object and the file's hashString to use for comparison later to
             * see if files are duplicates
             */
            Dictionary<FileInfo, string> fileAndHashString = new Dictionary<FileInfo, string>();

            foreach (FileInfo file in filesToConvert)
            {
                byte[] dataBytes = File.ReadAllBytes(file.FullName);
                byte[] hashBytes = SHA256.HashData(dataBytes);
                string hashString = Convert.ToHexString(hashBytes); // COMPARE ON THIS LATER

                fileAndHashString[file] = hashString;
            }
            return fileAndHashString;
        }


        //static void SelectDuplicates(ConcurrentBag<FileInfo> allFiles)
        //{
        //    IEnumerable<FileInfo> duplicates = from file in allFiles
        //                                       select file;
        //}

        //static void DuplicatesToRemove(List<FileInfo> duplicates)
        //{
        //    Console.Write("Enter Y to delete duplicate files: ");
        //    string userResponse = Console.ReadLine();

        //    if (userResponse.Equals("Y", StringComparison.OrdinalIgnoreCase))
        //    {
        //        duplicates.ForEach(file => File.Delete(file.ToString()));
        //    }
        //    else
        //    {
        //        Environment.Exit(0);
        //    }
        //}

        static string BytesConversion(long bytes)
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