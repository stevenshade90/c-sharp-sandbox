namespace Switch_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Choose your favorite hobby:");
            Console.WriteLine("1. Biking");
            Console.WriteLine("2. Cycling");
            Console.WriteLine("3. Painting");

            object userChoice = Console.ReadLine();
            var switchVariable = ((int.TryParse(userChoice.ToString(), out int num)) ? num.ToString() : userChoice.ToString());

            Console.WriteLine(SwitchStarter(switchVariable.ToLower()));


            static String SwitchStarter(String variable)
            {
                return variable switch
                {
                    "1" => "Biking",
                    "biking" => "Biking",

                    "2" => "Cycling",
                    "cycling" => "Cycling",

                    "3" => "Painting",
                    "painting" => "Painting",

                    _ => "Not an option"
                };
            }
            
        }
    }
}