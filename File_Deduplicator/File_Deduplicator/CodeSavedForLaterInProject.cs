// CONSIDERATION - What if it is a cloud file that is being opened? Will it mass download files? Confirm underlying type before adding the file
// from the directory? Or, maybe stream the file?


//List<FileInfo> distinctFiles = new List<FileInfo>(); // Contains only the distinct files from allFiles

//List<FileInfo> duplicatesToRemove = new List<FileInfo>(allFilesBag);

//int i = 1;
//foreach (FileInfo file in allFilesBag.DistinctBy(f => f.Name).OrderBy(f => f.Length))
//{
//    distinctFiles.Add(file);
//Console.WriteLine($"File {i}: {file.FullName} (Size: {BytesConversion(file.Length)})");
//i++;
//}

//Console.WriteLine($"Total size: {Methods.ConversionMethods.BytesConversion(allFilesBag.Sum(f => f.Length))}");
//Console.WriteLine($"Distinct Files ({distinctFiles.Count()})");
//distinctFiles.ForEach(f => Console.WriteLine(f.Name));

//distinctFiles.ForEach(f =>
//{
//    if (duplicatesToRemove.Contains(f))
//    {
//        duplicatesToRemove.Remove(f);
//    }
//});

//Console.WriteLine("Viewing remaining files...");
//Console.WriteLine($"Amount: {duplicatesToRemove.Count}");
//Console.WriteLine($"Storage Saved: {Methods.ConversionMethods.BytesConversion(duplicatesToRemove.Sum(f => f.Length))}");
//duplicatesToRemove.ForEach(f => Console.WriteLine($"\tDuplicate File: {f.FullName} (Size: {Methods.ConversionMethods.BytesConversion(f.Length)})"));

//static void SelectDuplicates(ConcurrentBag<FileInfo> allFiles)
//{
//    IEnumerable<FileInfo> duplicates = from file in allFiles
//                                       select file;
//}

//static void DuplicatesToRemove(List<FileInfo> duplicates)
//{
//    Console.Write("Enter Y to delete duplicate files: ");
//    string userResponse = Console.ReadLine();

//    if (userResponse.Equals("Y", StringComparison.OrdinalIgnoreCase))
//    {
//        duplicates.ForEach(file => File.Delete(file.ToString()));
//    }
//    else
//    {
//        Environment.Exit(0);
//    }
//}