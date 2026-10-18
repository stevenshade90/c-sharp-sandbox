using File_Deduplicator.Methods;

namespace File_Deduplicator.UserClass
{
    internal class User : IDisposable
    {
        public DriveInfo[] Drives { get; private set; }
        public DirectoryInfo[] Directories { get; private set; }
        public DirectoryInfo PrimaryDirectory { get; private set; }

        public string CurrentAvailableStorage { get; init; }
        public string StorageFreedUp { get; set; } 

        public User()
        {
            Drives = DriveInfo.GetDrives();
            PrimaryDirectory = new DirectoryInfo(SetDirectoryInfo());
            Directories = PrimaryDirectory.GetDirectories();
            CurrentAvailableStorage = SetCurrentAvailableStorage();
        }

        public void GetDriveInfo()
        {
            foreach (DriveInfo drive in this.Drives)
            {
                Console.WriteLine($"Drive: {drive.Name}");
                Console.WriteLine($"Drive Type: {drive.DriveType}");

                if (drive.IsReady)
                {
                    Console.WriteLine($"\tVolume Label: {drive.VolumeLabel}");
                    Console.WriteLine($"\tFile System: {drive.DriveFormat}");
                    Console.WriteLine($"\tAvailable Space: {ConversionMethods.ConvertBytes(drive.AvailableFreeSpace)}");
                    Console.WriteLine($"\tTotal Size: {ConversionMethods.ConvertBytes(drive.TotalSize)}");
                }
                else
                {
                    Console.WriteLine("Drive is not ready.");
                }
                Console.WriteLine();
            }
        }

        public string SetCurrentAvailableStorage()
        {
            foreach (DriveInfo drive in this.Drives)
            {
                if (drive.Name == "C:\\")
                {
                    return drive.AvailableFreeSpace.ToString();
                }
            }
            return "0";
        }

        public string SetDirectoryInfo()
        {
            while (true)
            {
                Console.Write("Enter the directory path to scan for files: ");
                string? inputPath = Console.ReadLine();

                if (Directory.Exists(inputPath))
                {
                    Console.WriteLine("Directory found!\n");
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
            Console.WriteLine("Resources disposed!");
        }
    }
}