using File_Deduplicator.Methods;
using System.Text.Json.Serialization;

namespace File_Deduplicator.UserClass
{
    internal class User : IDisposable
    {
        [JsonIgnore]
        public DriveInfo[] Drives { get; private set; }
        [JsonIgnore]
        public DirectoryInfo[] Directories { get; set; }
        [JsonIgnore]
        public DirectoryInfo PrimaryDirectory { get; set; }

        public long LifetimeStorageFreed { get; set; }
        [JsonIgnore]
        public long SessionStorageFreed { get; set; }

        public User()
        {
            Drives = DriveInfo.GetDrives();
        }

        public void SetPrimaryDirectoryAndDirectories()
        {
            PrimaryDirectory = null;
            Directories = null;

            PrimaryDirectory = new DirectoryInfo(SetDirectoryInfo());
            Directories = PrimaryDirectory.GetDirectories();
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

        public void ViewCumulativeStorageFreed()
        {
            Console.WriteLine($"Cumulative session storage freed: {ConversionMethods.ConvertBytes(SessionStorageFreed)}");
            Console.WriteLine($"Lifetime storage freed: {ConversionMethods.ConvertBytes(LifetimeStorageFreed)}\n");
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

        public static void DeleteCurrentUser()
        {
            try
            {
                if (File.Exists(SerializationMethods.FileName))
                {
                    File.Delete(SerializationMethods.FileName);
                    Console.WriteLine("User information deleted");
                }
            }
            catch
            {
                Console.WriteLine("Unable to delete user");
            }
        }

        public void Dispose()
        {
            Console.WriteLine("Resources disposed!");
        }
    }
}