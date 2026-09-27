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

            Action<string> warn = (text) => Console.WriteLine($"WARNING: {text}");
            warn.Invoke("Watch out");

            Func<int, int, bool> random = (static (int a, int b) =>
            {
                return a % b == 0;
            });

            int a = 3, b = 4;
            Console.WriteLine($"{a} % {b} = 0? => {random.Invoke(a, b)}");

            bool doesThisNumberExist = numbers.Exists(x => x == 1001);
            Console.WriteLine(doesThisNumberExist);
            Console.WriteLine(oddNums.Exists(x => x == 3));

            //Console.WriteLine($"Is every number > 0? => {numbers.TrueForAll(x => x > 0)}");
            bool bb = numbers.TrueForAll(x =>
            {
                Console.WriteLine($"Is {x} > 0? => {x > 50}");

                return x > 50;
            });
            Console.WriteLine(bb);

            var evenNumsDividedTen = evenNums.FindAll(x => x % 10 == 0);
            evenNumsDividedTen.ForEach(x => Console.WriteLine(x));
        }
    }
}