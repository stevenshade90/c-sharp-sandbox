namespace TupleDeconstruction_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            (int X, int Y) myTuple = (5, 6);
            Console.WriteLine("Original values of myTuple");
            Console.WriteLine($"\t{myTuple.X}");
            Console.WriteLine($"\t{myTuple.Y}");
            Console.WriteLine();

            (int x, int y) = myTuple;
            Console.WriteLine("New tuple assigned from myTuple");
            Console.WriteLine($"\t{x}");
            Console.WriteLine($"\t{y}");
            Console.WriteLine();

            Point p1 = new Point(5, 6);
            (int xPos, int yPos) desconstructedTuple = p1.Deconstruct();
            Console.WriteLine("Deconstructed Tuple from Point object");
            Console.WriteLine($"\t{desconstructedTuple.xPos}");
            Console.WriteLine($"\t{desconstructedTuple.yPos}");
        }
         
        struct Point
        {
            public int X;
            public int Y;

            public Point() { }
            public Point(int xPos, int yPos)
            {
                X = xPos; Y = yPos;
            }

            public (int, int) Deconstruct() => (X, Y);
        }
    }
}