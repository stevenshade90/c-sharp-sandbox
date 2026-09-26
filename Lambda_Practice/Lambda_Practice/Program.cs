namespace Lambda_Practice
{
    internal class Program
    {
        public delegate string MyNewDelegate();
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            numbers.AddRange(Enumerable.Range(1, 1000));

            List<int> evenNums = numbers.FindAll((int n) => (n % 2 == 0));
            List<int> oddNums = numbers.FindAll(n =>
            {
                Console.WriteLine($"Current Num being evaluated: {n}");
                bool isOdd = n % 2 != 0;
                return isOdd;
            });


            //Console.WriteLine("EVEN NUMBERS");
            //evenNums.ForEach(n => Console.Write(n + " "));

            Console.WriteLine();
            Console.WriteLine("ODD NUMBERS");
            Console.Write(string.Join(", ", oddNums));

            Console.WriteLine("\n");
            MyNewDelegate d = new MyNewDelegate(() => "Here's the string return");
            Console.WriteLine(d.Invoke());
        }
    }
}