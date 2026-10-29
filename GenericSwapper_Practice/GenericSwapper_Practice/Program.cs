namespace GenericSwapper_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string beforeTxt = "Before swap", afterTxt = "After swap";

            dynamic value1 = 1;
            dynamic value2 = 2;

            InputValues(beforeTxt, value1, value2);
            Swapper(ref value1, ref value2);
            InputValues(afterTxt, value1, value2);
            Console.WriteLine();

            value1 = "Steve";
            value2= "Shade";
            InputValues(beforeTxt, value1, value2);
            Swapper(ref value1, ref value2);
            InputValues(afterTxt, value1, value2);
            Console.WriteLine();

            value1 = 'c';
            value2 = 'd';
            InputValues(beforeTxt, value1, value2);
            Swapper(ref value1, ref value2);
            InputValues(afterTxt, value1, value2);
            Console.WriteLine();

            value1 = new List<int>() { 1, 2, 3, 4, 5 };
            value2 = new List<int>() { 6, 7, 8, 9, 10 };
            InputValues(beforeTxt, value1, value2);
            Swapper(ref value1, ref value2);
            InputValues(afterTxt, value1, value2);
        }

        static void Swapper<T>(ref T a, ref T b)
        {
            T temp = a;

            a = b;
            b = temp;

            // (b, a) = (a, b);
        }

        static void InputValues<T>(string text, T a, T b)
        {
            if ((a is List<int> list1) && (b is List<int> list2))
            {
                DisplayListValues(text, list1, list2);
                return;
            }
            Console.WriteLine($"{text, -11} => Value1: {a, -10} Value2: {b, -10}");
        }

        static void DisplayListValues(string text, List<int> list1, List<int> list2)
        {
            Console.Write("List1 values: ");
            list1.ForEach(x =>
            {
                Console.Write($"{x} ");
            });
            Console.WriteLine();

            Console.Write("List2 values: ");
            list2.ForEach(x =>
            {
                Console.Write($"{x} ");
            });
            Console.WriteLine();

            Console.WriteLine($"{text,-11} => Value1: {list1[0],-10} Value2: {list2[0],-10}");
        }
    }
}