namespace File_Deduplicator.UserClass
{
    internal class User : IDisposable
    {
        public DriveInfo[] drives { get; private set; }

        public DirectoryInfo dir { get; private set; } 
        public DirectoryInfo[] directories { get; private set; } 

        public User()
        {
            drives = DriveInfo.GetDrives();
            dir = new DirectoryInfo(SetDirectoryInfo());
            directories = dir.GetDirectories();
        }

        public void GetDriveInfo()
        {
            foreach (DriveInfo drive in this.drives)
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

        public string SetDirectoryInfo()
        {
            while (true)
            {
                Console.Write("Enter the directory path to scan for files:");
                string? inputPath = Console.ReadLine();
                if (Directory.Exists(inputPath))
                {
                    Console.WriteLine("Directory found!");
                    return inputPath;
                }
                else
                {
                    Console.WriteLine("Invalid directory path. Please try again.");
                }  
            }
        }

        public void Dispose()
        {
            // Not needed for DriveInfo and DirectoryInfo
            // May use for FileStream later
            Console.WriteLine("Resources disposed!");
        }
    }
}