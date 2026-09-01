using System.Runtime.Intrinsics.Arm;

namespace GenericsPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Point<int> point1 = new Point<int>(1, 2);
            Console.WriteLine($"Before swapping: {point1}");
            point1.Swap<int>(ref point1);
            Console.WriteLine($"Swap in memory confirmation: {point1}");
            Point<int>.PatternMatching(point1);
            point1.ResetPopint();
            Console.WriteLine(point1);
            Console.WriteLine();

            Point<double> point2 = new Point<double>(1.5, 1.6);
            Console.WriteLine($"Before swapping: {point2}");
            point2.Swap<double>(ref point2);
            Console.WriteLine($"Swap in memory confirmation: {point2}");
            Point<double>.PatternMatching(point2);
            Console.WriteLine();

            Point<String> point3 = new Point<String>("1", "2");
            Console.WriteLine($"Before swapping: {point3}");
            point3.Swap<String>(ref point3);
            Console.WriteLine($"Swap in memory confirmation: {point3}");
            Point<String>.PatternMatching(point3);
            Console.WriteLine();
        }

        public struct Point<T> 
        {
            private T _xPos;
            private T _yPos;

            public Point()
            {
                this._xPos = default(T);
                this._yPos = default(T);
            }
            public Point(T xVal, T yVal)
            {
                _xPos = xVal;
                _yPos = yVal;
            }

            public T X
            {
                get => _xPos;
                set => _xPos = value;
            }

            public T Y
            {
                get => _yPos;
                set => _yPos = value;
            }

            public override string ToString() => $"[{_xPos}, {_yPos}]";

            public void Swap<T>(ref Point<T> a)
            {
                T temp = a._xPos;
                a._xPos = a._yPos;
                a._yPos = temp;

                Console.WriteLine($"After swapping: {a}");
            }

            public void ResetPopint()
            {
                _xPos = default(T);
                _yPos = default(T);
            }

            public static void PatternMatching(object p)
            {
                switch (p)
                {
                    case Point<String> pString:
                        Console.WriteLine("String type: {0}", pString);
                        break;
                    case Point<int> pInt:
                        Console.WriteLine("Int type: {0}", pInt);
                        break;
                    case Point<double> pDouble:
                        Console.WriteLine("Double type: {0}", pDouble);
                        break;
                    default:
                        Console.WriteLine("Unaccepted value");
                        break;
                }
            }
        }
    }
}