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
                DisplayMethods.DisplayMessage("Gathering All Files from Directories");
                IEnumerable<FileInfo> allFilesContainer = await ReadMethods.ReadDirectoriesAsync(user1.Directories); // Contains every file from all directories 

                DisplayMethods.DisplayMessage("Converting Files to Hash Strings");
                IDictionary<string, ConcurrentBag<FileInfo>> convertedData = await ConversionMethods.ConvertFileToHashStringAsync(allFilesContainer);

                ReadMethods.ReadConvertedData(convertedData);

                // Next, find duplicates in the convertData and create a method to delete them
                // Could also have it calculate the .Length of the file object removed to show how much storage was freed up
            }
        }
    }
}