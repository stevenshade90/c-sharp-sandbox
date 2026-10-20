using File_Deduplicator.Methods;
using File_Deduplicator.UserClass;
using System.Collections.Concurrent;

namespace File_Deduplicator
{
    internal class Program
    {
        static bool continueOrExit = true;

        static async Task Main(string[] args)
        {
            DisplayMethods.DisplayMessageGeneric("Loading User");

            using (User user1 = SerializationMethods.FileExistCheck())
            {
                while (continueOrExit)
                {
                    user1.SetPrimaryDirectoryAndDirectories();
                    DisplayMethods.DisplayMessageGeneric("Gathering All Files from Directories");
                    List<FileInfo> allFilesContainer = await ReadMethods.ReadDirectoriesAsync(user1.Directories); // Contains every file from all directories

                    DisplayMethods.DisplayMessageGeneric("Converting Files to Hash Strings");
                    Dictionary<string, ConcurrentBag<FileInfo>> convertedData = await ConversionMethods.ConvertFileToHashStringAsync(allFilesContainer); // Contains all files with a HashString associated to it

                    DisplayMethods.DisplayMessageGeneric("Displaying Duplicate Files");
                    List<FileInfo> duplicateFiles = DeleteMethods.RemoveDuplicatesFromData(convertedData);
                    DeleteMethods.DeleteCheck(duplicateFiles, user1);

                    DisplayMethods.DisplayMessageGeneric("Continue Request");
                    continueOrExit = DisplayMethods.DisplayUserConfirmation("Enter Y to search another directory: ");
                }
                user1.ViewCumulativeStorageFreed();
                SerializationMethods.SerializeUserDataAsJson<User>(user1);
            }
        }
    }
}