namespace ConsoleApp48
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> Fruits = new Dictionary<int, string>
            {
                {1, "Apple" },
                {2, "Banana" },
                {3, "Coconut" }
            };

            Console.WriteLine(Fruits[1]);
            Console.WriteLine(Fruits[3]);

            Console.WriteLine(Fruits[2]);
            Fruits.Add(4, "Tomato");
            Console.WriteLine(Fruits[4]);

            foreach (var fruit in Fruits)
            {
                Console.WriteLine($"{fruit.Key}. {fruit.Value}");
            }


            bool TF = Fruits.ContainsValue("Tomato");
            Console.WriteLine("Result: " + TF);

            Fruits.Remove(4);
            TF = Fruits.ContainsValue("Tomato");
            Console.WriteLine("Result: " + TF);
            Console.WriteLine(Fruits.Count);

            Console.WriteLine(Fruits[2]);

            //Fruits.Clear();
            foreach (var key in Fruits.Keys)
            {
                Console.WriteLine(key);   
            }
            foreach (var value in Fruits.Values)
            {
                Console.WriteLine(value);
            }
        }
    }
}