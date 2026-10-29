using File_Deduplicator.Methods;
using File_Deduplicator.UserClass;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace File_Deduplicator
{
    public class ProgramOrchestrator
    {
        private readonly ILogger<ProgramOrchestrator> _logger;

        private readonly DisplayMethods _display;
        private readonly ReadMethods _reader;
        private readonly ConversionMethods _converter;
        private readonly DeleteMethods _deleter;
        public readonly SerializationMethods _serializer;

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

        public async Task ExecuteProgram()
        {
            DisplayMethods.DisplayMessageGeneric("Loading User");
            User user = _serializer.FileExistCheck();

            while (continueOrExit)
            {
                user.SetPrimaryDirectoryAndDirectories();
                DisplayMethods.DisplayMessageGeneric("Gathering All Files from Directories");
                List<FileInfo> allFilesContainer = await _reader.ReadDirectoriesAsync(user.Directories); // Contains every file from all directories

                DisplayMethods.DisplayMessageGeneric("Converting Files to Hash Strings");
                Dictionary<string, ConcurrentBag<FileInfo>> convertedData = await _converter.ConvertFileToHashStringAsync(allFilesContainer); // Contains all files with a HashString associated to it

                DisplayMethods.DisplayMessageGeneric("Displaying Duplicate Files");
                List<FileInfo> duplicateFiles = _deleter.RemoveDuplicatesFromData(convertedData);
                _deleter.DeleteCheck(duplicateFiles, user);

                DisplayMethods.DisplayMessageGeneric("Continue Request");
                continueOrExit = DisplayMethods.DisplayUserConfirmation("Enter Y to search another directory: ");
            }
            user.ViewCumulativeStorageFreed();
            _serializer.SerializeUserDataAsJson<User>(user);
        }
    }
}