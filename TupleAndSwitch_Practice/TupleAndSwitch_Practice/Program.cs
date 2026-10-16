namespace TupleAndSwitch_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //1. Computer selection
                Random r = new Random();
                int i = r.Next(1, 3);
                String computerChoice = ComputerChoice(i);

                //2. User selection sequence
                Console.WriteLine("Choose:");
                Console.WriteLine("\t1. Rock");
                Console.WriteLine("\t2. Scissor");
                Console.WriteLine("\t3. Paper");

                object userChoice = Console.ReadLine();
                var o = int.TryParse(userChoice.ToString(), out int userNum) ? userNum : userChoice;
                String userSelection = PlayerChoice(o);

                Console.WriteLine("User selected: {0} -- Computer selected: {1}", userSelection, computerChoice);
                //3. Convert game values to tuple
                (String, String) gameTuple = (computerChoice.ToLower(), userSelection.ToLower());

                //4. Display results utilizing gameTuple
                Console.WriteLine(RockPaperScissorsTuple(gameTuple));
                Console.WriteLine();
            }
        }
        static String ComputerChoice(int i)
        {
            return (i) switch
            {
                1 => "rock",
                2 => "paper",
                3 => "scissor",
                _ => "rock"
            };
        }

        static String PlayerChoice(object o)
        {
            switch (o)
            {
                case int i when i == 1:
                case String s when s.Equals("rock", StringComparison.OrdinalIgnoreCase):
                    return "rock";

                case int i when i == 2:
                case String s when s.Equals("paper", StringComparison.OrdinalIgnoreCase):
                    return "paper";

                case int i when i == 3:
                case String s when s.Equals("scissor", StringComparison.OrdinalIgnoreCase):
                    return "scissor";

                default:
                    Console.WriteLine("No selection, assigning \"Rock\"");
                    return "rock";
            }
        }

        public static String RockPaperScissorsTuple((String, String) a)
        {
            return (a) switch
            {
                ("rock", "paper") => "Paper covers rock!",
                ("rock", "scissor") => "Rock crushes scissors!",
                ("paper", "rock") => "Paper covers rock!",
                ("paper", "scissor") => "Scissors cut paper!",
                ("scissor", "paper") => "Scissors cut paper!",
                ("scissor", "rock") => "Rock crushes scissors!",
                (_, _) => "Tie!"
            };
        }
    }
}