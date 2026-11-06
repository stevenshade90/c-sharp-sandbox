namespace Switch_Practice2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Language preference:");
            Console.WriteLine("\t1: C#");
            Console.WriteLine("\t2: C");
            Console.WriteLine("\t3: Python"); 

            object userChoice = Console.ReadLine();
            var switchChoice = int.TryParse(userChoice.ToString(), out int x) ? x : userChoice;

            switch (switchChoice)
            {
                case int i when i == 1:
                case String s when s.Equals("C#", StringComparison.OrdinalIgnoreCase):
                    Console.WriteLine("I see your favorite language is C#");
                    break;

                case int i when i == 2:
                case String s when s.Equals("C", StringComparison.OrdinalIgnoreCase):
                    Console.WriteLine("I see your favorite language is C");
                    break;

                case int i when i == 3:
                case String s when s.Equals("Python", StringComparison.OrdinalIgnoreCase):
                    Console.WriteLine("I see your favorite language is Python");
                    break;

                default:
                    Console.WriteLine("No choice");
                    break;
            }

            object answer = Console.ReadLine();

            var abc = int.TryParse(answer.ToString(), out int yy) ? yy : answer;

            switch (abc)
            {
                case int i when i == 5:
                case string s when s.Equals("h"):
                    Console.WriteLine("Ok");
                    break;
            }
        }
    }
}