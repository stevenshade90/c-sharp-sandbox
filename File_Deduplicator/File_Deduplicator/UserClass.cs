namespace File_Deduplicator.UserClass
{
    internal class User : IDisposable
    {
        public DriveInfo[] drives { get; private set; }

        public DirectoryInfo dir { get; private set; } // Method to set this later
        public DirectoryInfo[] directories { get; private set; } 

        public User()
        {
            drives = DriveInfo.GetDrives();
            dir = new DirectoryInfo(@"C:\Users\steve\OneDrive\Desktop\Dorico Files");
            directories = dir.GetDirectories();
        }

        public void Dispose()
        {
            // Not needed for DriveInfo and DirectoryInfo
            // May use for FileStream later
            Console.WriteLine("Resources disposed!");
        }
    }
}