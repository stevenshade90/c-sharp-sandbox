namespace File_IO_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo di = new DirectoryInfo("");
            Console.WriteLine(di.CreationTime);
            Console.WriteLine(di.FullName);
            Console.WriteLine(di.Name);
            var fi = di.GetFiles();

            foreach (FileInfo s in fi)
            {
                Console.WriteLine(s.Name);
            }

            foreach (DirectoryInfo d in di.GetDirectories())
            {
                Console.WriteLine(d);
            }


            TestIfDirectoryExists(@"");
            TestIfDirectoryExists(@"");

            try
            {
                DirectoryInfo di2 = new DirectoryInfo(@"");
                //di2.Create();

                string targetDirectory = @"";

                di2.MoveTo(targetDirectory);
                Console.WriteLine("File moved!");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"Couldnt find the primary directory: {e.Message}");
            }


            foreach (string line in File.ReadAllLines(@""))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine(line);
                }
            }

            using (StreamReader sr = new StreamReader(@""))
            {
                string input = null;

                while ((input = sr.ReadLine()) != null)
                {
                    Console.WriteLine(input);
                }
            }
        }

        static void TestIfDirectoryExists(string directory)
        {
            string s = (new DirectoryInfo($@"{directory}").Exists) 
                ? $"Your directory exists! ({directory})" 
                : $"Your directory doesn't exist ({directory})";

            Console.WriteLine(s);   
        }
    }
}