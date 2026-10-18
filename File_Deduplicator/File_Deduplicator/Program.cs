using File_Deduplicator.Methods;
using File_Deduplicator.UserClass;
using System.Collections.Concurrent;

namespace File_Deduplicator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (User user1 = new User())
            {
                DisplayMethods.DisplayMessageGeneric("Gathering All Files from Directories");
                IList<FileInfo> allFilesContainer = await ReadMethods.ReadDirectoriesAsync(user1.Directories); // Contains every file from all directories

                DisplayMethods.DisplayMessageGeneric("Converting Files to Hash Strings");
                IDictionary<string, ConcurrentBag<FileInfo>> convertedData =  await ConversionMethods.ConvertFileToHashStringAsync(allFilesContainer); // Contains all files with a HashString associated to it

                DisplayMethods.DisplayMessageGeneric("Displaying Duplicate Files");
                List<FileInfo> duplicateFiles = DeleteMethods.RemoveDuplicatesFromData(convertedData);
                user1.StorageFreedUp = duplicateFiles.Sum(file => file.Length).ToString();  
                DeleteMethods.DeleteDuplicates(duplicateFiles, DeleteMethods.DeleteConfirmation());

                DisplayMethods.DisplayMessageGeneric("Duplicate Files Deleted");
                Console.WriteLine($"Storage Freed Up: {ConversionMethods.ConvertBytes(long.Parse(user1.StorageFreedUp))}");
            }
        }
    }
}