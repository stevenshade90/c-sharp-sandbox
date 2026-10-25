using File_Deduplicator.UserClass;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace File_Deduplicator.Methods
{
    internal class DisplayMethods
    {
        private readonly ILogger<DisplayMethods> _logger;

        public DisplayMethods(ILogger<DisplayMethods> logger)
        {
            _logger = logger;
        }

        public static void DisplayMessageGeneric<T>(T value1, string text2 = "")
        {
            if (value1 is IEnumerable<FileInfo> duplicateFiles)
            {
                if (!duplicateFiles.Any())
                {
                    Console.WriteLine("\tNo duplicate files found \n");
                }
                else
                {
                    foreach (var file in duplicateFiles)
                    {
                        Console.WriteLine($"\t{file.FullName}");
                    }
                }
            }
            else if (value1 is IEnumerable<KeyValuePair<string, ConcurrentBag<FileInfo>>> convertedData)
            {
                foreach (KeyValuePair<string, ConcurrentBag<FileInfo>> kvp in convertedData)
                {
                    string hashString = kvp.Key;
                    ConcurrentBag<FileInfo> files = kvp.Value;

                    if (files.Count <= 1) continue;

                    Console.WriteLine($"Hash: {hashString}");
                    foreach (FileInfo file in files)
                    {
                        Console.WriteLine($"\tFile: {file.FullName}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine(convertedData.Count());
            }
            else if (value1 is string text1)
            {
                Console.WriteLine(string.IsNullOrEmpty(text2)
                    ? $"********** {text1} **********"
                    : $"\t{text1}\n\t{text2}");
            }
        }

        public static bool DisplayUserConfirmation(string text)
        {
            Console.Write(text);
            string? userResponse = Console.ReadLine();
            Console.WriteLine();

            return userResponse.Equals("Y", StringComparison.OrdinalIgnoreCase)
                ? true
                : false;
        }

        public static string DisplayProcessedException(Exception ex, string additionalInfo)
        {
            return ex switch
            {
                UnauthorizedAccessException => ConversionMethods.ConvertTextToSubstring($"Access denied: {additionalInfo}"),
                IOException => ConversionMethods.ConvertTextToSubstring($"I/O Error: {additionalInfo}"),
                _ => ConversionMethods.ConvertTextToSubstring($"Unexpected error: {additionalInfo}")
            };
        }
    }

    internal class ReadMethods
    {
        private readonly ILogger<ReadMethods> _logger;

        public ReadMethods(ILogger<ReadMethods> logger)
        {
            _logger = logger;
        }

        public async Task<List<FileInfo>> ReadDirectoriesAsync(IEnumerable<DirectoryInfo> directories)
        {
            ConcurrentBag<FileInfo> allFiles = new ConcurrentBag<FileInfo>();
            ParallelOptions parOpts = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };

            _logger.LogInformation("Beginning to process all directories and pull out files");
            await Parallel.ForEachAsync(directories, parOpts, async (directory, _) =>
            {
                try
                {
                    FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories).ToArray();
                    await Parallel.ForEachAsync(files, parOpts, async (file, _) =>
                    {
                        allFiles.Add(file);
                    });
                }
                catch (Exception ex)
                {
                    string message = $"{ConversionMethods.ConvertTextToSubstring(directory)} -- Exception: {ex.Message}";
                    Console.WriteLine(DisplayMethods.DisplayProcessedException(ex, message));
                    _logger.LogError(ex, message);
                }
            });
            DisplayMethods.DisplayMessageGeneric("Done processing all files!", $"Total Files: {allFiles.Count(): #,##0}\n");
            _logger.LogInformation("All files processed and logged");
            await Task.Delay(3000);

            return allFiles.ToList();
        }
    }

    internal class ConversionMethods
    {
        private readonly ILogger<ConversionMethods> _logger;

        public ConversionMethods(ILogger<ConversionMethods> logger)
        {
            _logger = logger;
        }

        private const long BYTES_IN_MB = 1024 * 1024;
        private const long BYTES_IN_GB = 1024 * 1024 * 1024;

        public async Task<Dictionary<string, ConcurrentBag<FileInfo>>> ConvertFileToHashStringAsync(IEnumerable<FileInfo> filesToConvert)
        {
            ConcurrentDictionary<string, ConcurrentBag<FileInfo>> fileAndHashString = new ConcurrentDictionary<string, ConcurrentBag<FileInfo>>();
            ParallelOptions parOpts = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };

            _logger.LogInformation("Beginning to convert all files into HashStrings");
            await Parallel.ForEachAsync(filesToConvert, parOpts, async (file, _) =>
            {
                try
                {
                    if (file.Attributes.HasFlag(FileAttributes.Offline)) return;
                    if (file.GetType() != typeof(FileInfo)) return;

                    using FileStream fs = File.OpenRead(file.FullName);             
                    byte[] hashBytes = SHA256.HashData(fs);
                    string hashString = Convert.ToHexString(hashBytes);

                    ConcurrentBag<FileInfo> filebag = fileAndHashString.GetOrAdd(hashString, _ => new ConcurrentBag<FileInfo>());
                    filebag.Add(file);
                    
                }
                catch (Exception ex)
                {
                    string message = $"{ConversionMethods.ConvertTextToSubstring(file)} -- Exception: {ex.Message}";
                    Console.WriteLine(DisplayMethods.DisplayProcessedException(ex, message));
                    _logger.LogError(ex, message);
                }
            });
            Console.WriteLine("\tFinished converting files to Hash Strings\n");
            _logger.LogInformation("All files attempted to be converted to HashString");
            return fileAndHashString.ToDictionary();
        }

        public static string ConvertTextToSubstring<T>(T parameterValue)
        {
            StringBuilder sb = new StringBuilder();
            if (parameterValue is FileSystemInfo file)
            {
                sb.Append(file.FullName.Length >= 30
                    ? file.FullName.Substring(0, 30) + "..."
                    : file.FullName);
            }
            else if (parameterValue is string text)
            {
                sb.Append(text.Length >= Console.WindowWidth
                    ? text.Substring(0, Console.WindowWidth - 3) + "..."
                    : text);
            }
            return sb.ToString();
        }

        public static string ConvertBytes(long bytes)
        {
            return bytes switch
            {
                >= BYTES_IN_GB => $"{bytes / (double)BYTES_IN_GB:F4} GB",
                >= BYTES_IN_MB => $"{bytes / (double)BYTES_IN_MB:F4} MB",
                _ => $"{bytes:#,##0} bytes"
            };
        }
    }

    internal class DeleteMethods
    {
        private readonly ILogger<DeleteMethods> _logger;

        public DeleteMethods(ILogger<DeleteMethods> logger)
        {
            _logger = logger;
        }

        public List<FileInfo> RemoveDuplicatesFromData(IEnumerable<KeyValuePair<string, ConcurrentBag<FileInfo>>> convertedData)
        {
            List<FileInfo> duplicateFiles = new List<FileInfo>();
            _logger.LogInformation("Ordering all files and retrieving duplicates");
            foreach (KeyValuePair<string, ConcurrentBag<FileInfo>> kvp in convertedData)
            {
                var duplicates = kvp.Value.OrderBy(f => f.CreationTime).Skip(1).ToList();
                duplicates.ForEach(f => duplicateFiles.Add(f));
            }
            DisplayMethods.DisplayMessageGeneric<List<FileInfo>>(duplicateFiles);

            return duplicateFiles;
        }

        public void DeleteCheck(IEnumerable<FileInfo> allFiles, User user)
        {
            if (allFiles.Any())
            {
                _logger.LogInformation("Duplicates found -- requesting use confirmation to delete");
                long storageFreed = allFiles.Sum(file => file.Length);
                bool userConfirmation = DisplayMethods.DisplayUserConfirmation("Enter Y to delete duplicate files: ");
                DeleteDuplicates(allFiles, userConfirmation);

                if (userConfirmation)
                {
                    user.SessionStorageFreed += storageFreed;
                    user.LifetimeStorageFreed += storageFreed;

                    DisplayMethods.DisplayMessageGeneric("Duplicate Files Deleted");
                    Console.WriteLine($"Storage freed from current directory: {ConversionMethods.ConvertBytes(storageFreed)}\n");
                }
            }
        }

        public void DeleteDuplicates(IEnumerable<FileInfo> duplicateFiles, bool confirmation)
        {
            _logger.LogInformation("Attempting to delete duplicate files");
            if (confirmation)
            {
                _logger.LogInformation("User confirmed deletion -- beginning to delete files");
                foreach (FileInfo file in duplicateFiles)
                {
                    try
                    { 
                        file.Delete();
                        Console.WriteLine($"\tDeleted: {file.FullName}");
                    }
                    catch (Exception ex)
                    {
                        string message = $"{ConversionMethods.ConvertTextToSubstring(file)} -- Exception: {ex.Message}";
                        Console.WriteLine(DisplayMethods.DisplayProcessedException(ex, message));
                        _logger.LogError(ex, message);
                    }
                }
                Console.WriteLine();
            }
            else
            {
                _logger.LogInformation("User denied deletion request");
                Console.WriteLine("\tDeletion canceled by user");
            }
        }
    }

    public class SerializationMethods
    {
        private readonly ILogger<SerializationMethods> _logger;

        public SerializationMethods(ILogger<SerializationMethods> logger)
        {
            _logger = logger;
        }

        public static string FileName { get; } = "user_data.json";
        public static JsonSerializerOptions Options { get; } = new JsonSerializerOptions() 
        { 
            IncludeFields = true, WriteIndented = true, PropertyNamingPolicy = null, PropertyNameCaseInsensitive = true 
        };

        public void SerializeUserDataAsJson<T>(T userData)
        {
            Console.WriteLine($"Saving file as: {FileName}");
            _logger.LogInformation("Attempting to serialize user information");
            try
            {
                File.WriteAllText(FileName, System.Text.Json.JsonSerializer.Serialize(userData, Options));
                Console.WriteLine("Saved user info!");
                _logger.LogInformation("User successfully serialized");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File not saved: {ex.Message}");
                _logger.LogError(ex, "User not serialized");
            }

        }

        public T DeserializeUserDataAsJson<T>()
        {
            _logger.LogInformation("Attempting to deserialize user info");
            return System.Text.Json.JsonSerializer.Deserialize<T>(File.ReadAllText(FileName), Options);
        }

        public User FileExistCheck()
        {
            User userInfo;
            _logger.LogInformation("Attempting to find user information");
            if (File.Exists(FileName))
            {
                _logger.LogInformation("User information found");
                Console.WriteLine("Found user data!\n");
                userInfo = DeserializeUserDataAsJson<User>();
            }
            else
            {
                _logger.LogInformation("User information not found");
                Console.WriteLine("No user data found\n");
                userInfo = new User();
            }
            return userInfo;
        }
    }
}