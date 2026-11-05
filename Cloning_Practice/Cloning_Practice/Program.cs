namespace Cloning_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("First Point Set Test");
            Point p1 = new Point(50, 50);
            Point p2 = (Point)p1.Clone();

            Console.WriteLine("Before mod");
            Console.WriteLine("p1: {0}", p1);
            Console.WriteLine("p2: {0}", p2);

            p2.X = 25;
            p2.Y = 99;

            Console.WriteLine("After mod");
            Console.WriteLine("p1: {0}", p1);
            Console.WriteLine("p2: {0}", p2);
            Console.WriteLine();

            Console.WriteLine("Second Point Set Test");
            Point p3 = new Point(100, 100, "Jorge");
            Point p4 = (Point)p3.Clone();

            Console.WriteLine("Before mod");
            Console.WriteLine("p3: {0}", p3);
            Console.WriteLine("p4: {0}", p4);

            p4.desc.PetName = "My New point";
            p4.X = 9;

            Console.WriteLine("After mod");
            Console.WriteLine("p3: {0}", p3);
            Console.WriteLine("p4: {0}", p4);
        }
    }

    public class PointDescription
    {
        public string PetName { get; set; }
        public Guid PointID { get; set; }

        public PointDescription()
        {
            PetName = "No name";
            PointID = Guid.NewGuid();
        }
    }

    public class Point : ICloneable
    {
        public PointDescription desc = new PointDescription();
        public int X { get; set; }
        public int Y { get; set; }

        public Point() { }
        public Point (int xPos, int yPos, string petName) 
        { 
            X = xPos; 
            Y = yPos;
            desc.PetName = petName;
        }
        public Point (int xPos, int yPos)
        {
            X = xPos; Y = yPos;
        }

        public override string ToString()
        {
            return $"X = {X}; Y = {Y}; Name = {desc.PetName}; Hash = {this.GetHashCode()}; PointDescription Object Hash = {desc.GetHashCode()}";
        }

        // Shallow copy -- each cloned object will have a reference to the same PointDescription object, but value types will be unique
        public object Clone() => this.MemberwiseClone();
    }
}