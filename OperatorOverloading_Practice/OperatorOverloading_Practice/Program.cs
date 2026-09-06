namespace OperatorOverloading_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Square s1 = new Square(10);
            Square s2 = new Square(20);

            Console.WriteLine($"Square 1: {s1}");
            Console.WriteLine($"Square 2: {s2}");


            Console.WriteLine($"Square 1 + Square 2: {s1 + s2}");
            Square s3 = s1 + s2;
            Console.WriteLine($"Result: {s3}");

            
            Square s4 = s1 + 5;
            Console.WriteLine($"Square 1 + integer (5): {s4}");

            Console.WriteLine($"Increasing size of Square 1 by +1: {++s1}");

            Console.WriteLine("\n\n\n");
            Console.WriteLine($"Square 1 minus itself: {s1 - s1}");
            Console.WriteLine($"Square 1 > Square 2?: {s1 > s2}");
            Console.WriteLine($"Square 1 < Square 2?: {s1 < s2}");

            Console.WriteLine("\n\n\n");
            Console.WriteLine("Rectangle to Square conversion");
            Rectangle r1 = new Rectangle(10, 20);
            Console.WriteLine($"Rectangle: {r1}");
            Square s5 = (Square)r1;
            Console.WriteLine($"Converted to Square: {s5}");
        }
    }

    public class Square : IComparable<Square> 
    {
        int width { get; set; }
        int height { get; set; }

        public Square(int width)
        {
            this.width = width;
            this.height = width;
        }

        public int Area() =>  width * height;

        // + operator overloading
        public static Square operator +(Square s1, Square s2) => new Square(s1.width + s2.width);
        public static Square operator +(Square s1, int difference) => new Square(s1.width + difference);    
        public static Square operator ++(Square sq) => new Square(sq.width + 1);

        // - operator overloading
        public static Square operator -(Square s1, Square s2) => new Square(s1.width - s2.width);
        public static Square operator -(Square s1, int difference) => new Square(s1.width - difference);
        public static Square operator --(Square sq) => new Square(sq.width - 1);

        // IComparable implementation
        public int CompareTo(Square? other)
        {
            int x = (other == null) ? 1 : this.Area().CompareTo(other.Area());
            return x;
        }

        public static bool operator >(Square s1, Square s2) => s1.Area().CompareTo(s2.Area()) > 0;
        public static bool operator <(Square s1, Square s2) => s1.Area().CompareTo(s2.Area()) < 0;

        public static explicit operator Square(Rectangle r)
        {
            return new Square(r.width);
        }

        public override string ToString() => $"[Width = {width}, Height = {height}, Area = {Area()}]";
    }

    public class Rectangle
    {
        public int width { get; set; }
        public int height { get; set; }
        public Rectangle(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public int Area() => width * height;
        public override string ToString() => $"[Width = {width}, Height = {height}, Area = {Area()}]";
    }
}