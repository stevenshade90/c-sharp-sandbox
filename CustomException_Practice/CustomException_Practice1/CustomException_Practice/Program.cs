using System.Collections;

namespace CustomException_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userChoice1 = NumberSelection("Enter a number: ");
            string userChoice2 = NumberSelection("Enter a number: ");

            try
            {
                if (Convert.ToInt32(userChoice1) == 2 && Convert.ToInt32(userChoice2) == 2)
                {
                    Console.WriteLine($"Its an int and it's {userChoice1}!");
                }
                else

                {
                    throw new MyExceptions("An error occurred", "The value isnt an int", DateTime.Now)
                    {
                        Data =
                        {
                            { "Warning Level", "1" },
                            { "Repair Price", "50000" }
                        }

                    };
                }
            }
            catch (MyExceptions e)
            {
                const int padding = -35;

                Console.WriteLine("\n=== An Exception Has Been Thrown ===");
                Console.WriteLine($"{"Custom Exception Error Message", padding} => {e.ErrorMessage}");
                Console.WriteLine($"{"Error Cause", padding} => {e.ErrorCause}");
                Console.WriteLine($"{"Error Time Stamp", padding} => {e.ErrorTimeStamp}");
                Console.WriteLine($"{"Base Exception Message", padding} => {e.Message}");
                Console.WriteLine("Displaying Dictionary Entry Data");
                foreach (DictionaryEntry de in e.Data)
                {
                    Console.WriteLine("\t{0} : {1}", de.Key, de.Value);
                }
            }

        }

        static string NumberSelection(string text)
        {
            Console.Write(text);
            return Console.ReadLine();
        }
    }

    public class MyExceptions : Exception
    {
        public String ErrorMessage { get; set; }
        public String ErrorCause { get; set; }
        public DateTime ErrorTimeStamp { get; set; }

        public MyExceptions() { }
        public MyExceptions(String message, String cause, DateTime timestamp)
            : base(message) 
        {
            ErrorMessage = message;
            ErrorCause = cause;
            ErrorTimeStamp = timestamp;
        }
    }
}