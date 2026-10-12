using File_Deduplicator.Methods;
using File_Deduplicator.UserClass;

namespace File_Deduplicator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (User user1 = new User())
            {
                //DisplayMethods.DisplaySectionHeader("Drive Information");
                //await DisplayMethods.GetDriveInfo(user1.drives);

                DisplayMethods.DisplaySectionHeader("Gathering All Files from Directories");
                IEnumerable<FileInfo> allFilesContainer = await ReadMethods.ReadDirectoriesAsync(user1.directories); // Contains every file from all directories 

                DisplayMethods.DisplaySectionHeader("Converting Files to Hash Strings");
                Task<IDictionary<FileInfo, string>> convertedData = ConversionMethods.ConvertFileToHashStringAsync(allFilesContainer);

                ReadMethods.ReadConvertedData(convertedData);
            }

            Environment.Exit(0);
        }
    }
}