using File_Deduplicator.Methods;
using File_Deduplicator.UserClass;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace File_Deduplicator
{
    internal class ProgramOrchestrator
    {
        private readonly ILogger<ProgramOrchestrator> _logger;

        private readonly DisplayMethods _display;
        private readonly ReadMethods _reader;
        private readonly ConversionMethods _converter;
        private readonly DeleteMethods _deleter;
        private readonly SerializationMethods _serializer;

        private static bool continueOrExit = true;

        public ProgramOrchestrator(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ProgramOrchestrator>();

            _display = new DisplayMethods(loggerFactory.CreateLogger<DisplayMethods>());
            _reader = new ReadMethods(loggerFactory.CreateLogger<ReadMethods>());
            _converter = new ConversionMethods(loggerFactory.CreateLogger<ConversionMethods>());
            _deleter = new DeleteMethods(loggerFactory.CreateLogger<DeleteMethods>());
            _serializer = new SerializationMethods(loggerFactory.CreateLogger<SerializationMethods>());
        }

        public async Task ExecuteProgram(User user)
        {
            while (continueOrExit)
            {
                user.SetPrimaryDirectoryAndDirectories();
                DisplayMethods.DisplayMessageGeneric("Gathering All Files from Directories");
                List<FileInfo> allFilesContainer = await ReadMethods.ReadDirectoriesAsync(user.Directories); // Contains every file from all directories

                DisplayMethods.DisplayMessageGeneric("Converting Files to Hash Strings");
                Dictionary<string, ConcurrentBag<FileInfo>> convertedData = await ConversionMethods.ConvertFileToHashStringAsync(allFilesContainer); // Contains all files with a HashString associated to it

                DisplayMethods.DisplayMessageGeneric("Displaying Duplicate Files");
                List<FileInfo> duplicateFiles = DeleteMethods.RemoveDuplicatesFromData(convertedData);
                DeleteMethods.DeleteCheck(duplicateFiles, user);

                DisplayMethods.DisplayMessageGeneric("Continue Request");
                continueOrExit = DisplayMethods.DisplayUserConfirmation("Enter Y to search another directory: ");
            }
            user.ViewCumulativeStorageFreed();
            SerializationMethods.SerializeUserDataAsJson<User>(user);
        }
    }
}