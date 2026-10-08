using System.IO;

namespace File_Deduplicator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            DirectoryInfo dir1 = new DirectoryInfo(@"C:\Users\steve\OneDrive\Desktop");
            DirectoryInfo[] directories = dir1.GetDirectories();

            GetDriveInfo(drives);
            await Task.Delay(1500);

            await ReadDirectoriesAsync(directories);


            //foreach (string userDirectory in allDirectories)
            //{
            //    Console.WriteLine($"Current Directory: {userDirectory}");
            //    string[] files = Directory.GetFiles(userDirectory, "*.*", SearchOption.AllDirectories);

            //    Console.WriteLine($"Number of files: {files.Length}");
            //    Console.WriteLine();
            //}
        }

        static void GetDriveInfo(DriveInfo[] drives)
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
        }

        static async Task ReadDirectoriesAsync(DirectoryInfo[] directories)
        {
            //now parallel.foreachasync
            foreach (DirectoryInfo directory in directories)
            {
                FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories);

                Console.WriteLine($"Current Directory: {directory.FullName}");
                Console.WriteLine($"Number of files: {files.Count()}");

                int i = 1;
                foreach (FileInfo file in files.OrderBy(f => f.Length))
                {
                    Console.WriteLine($"\tFile {i}: {file.Name} (Size: {file.Length:#,###} bytes)");
                    i++;
                }
                Console.WriteLine();
            }

        }
    }
}
