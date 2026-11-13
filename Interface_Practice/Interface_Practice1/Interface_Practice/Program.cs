using System.Collections;

namespace Interface_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hexagon hex = new Hexagon("Hexi1");
            Circle c = new Circle("Lisa");
            Hexagon hex2 = new Hexagon("Hexi2");

            //IPointy i = null;
            IPointy i2Conv = hex2 as IPointy;

            if (i2Conv != null)
            {
                Console.WriteLine(i2Conv.Points);
            }

            if (hex2 is IPointy i)
            {
                Console.WriteLine("Worked");
            }

            PointyCheck(hex);
            PointyCheck(c);

            static void PointyCheck (object o)
            {
                IPointy i = o as IPointy;
                if (i != null)
                {
                    Console.WriteLine("Sides: {0}", i.Points);
                }
                else
                {
                    Console.WriteLine("No sides");
                }
            }

            Square sq1 = new Square("Box1") 
                { NumberOfSides = 4, SideLength = 4 };

            Console.WriteLine(((IRegularPointy)sq1).Perimeter);

            /*
            if (i2Conv != null)
            {
                Console.WriteLine("Converted!");
                Console.WriteLine(i2Conv.Points);
            }
            else
            {
                Console.WriteLine("No points");
            }
        
                
            try
            {
                i = (IPointy)c;
                Console.WriteLine(i.Points);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }

        
            Console.WriteLine($"IRegularPointy: {IRegularPointy.ExampleProperty}");
            IRegularPointy.ExampleProperty = "Updated";
            Console.WriteLine($"IRegularPointy: {IRegularPointy.ExampleProperty}");

            Shape[] myShapes = 
            { 
                new Hexagon(), 
                new Circle(), 
                new Triangle(), 
                new Hexagon(),
            };

            Shape[] newShapes =
            {
                new Hexagon(),
                new Circle(),
                new Triangle(),
                new Hexagon(),
                new Hexagon(),
                new Circle(),
            };

            for (int i = 0; i < myShapes.Length; i++)
            {
                if (myShapes[i] is IDraw3D s)
                {
                    DrawIn3D(s);
                }
            }

            for (int i = 0; i < newShapes.Length; i++) 
            {
                if (newShapes[i] is IPointy ii)
                {
                    IsExtraPointy(ii);
                }
                else
                {
                    Console.WriteLine("Not pointy");
                }
            }

            IPointy check = FindFirstPointyShape(myShapes);
            Console.WriteLine(check?.Points);

            foreach (Shape shape in newShapes)
            {
                Console.WriteLine(shape);
            }
            */

            Shape s = new Shape("Frank");

            IEnumerator shapeEnumerator = s.GetEnumerator();
            Console.WriteLine(shapeEnumerator);

        }
        public static void DrawIn3D(IDraw3D id3t)
        {
            Console.WriteLine("Drawing in 3D, if compatible...");
            id3t.Draw3D();
        }

        public static void IsExtraPointy(IPointy ipt)
        {
            Console.WriteLine("This is extra pointy...");
            ipt.ExtraPointy();
        }

        public static IPointy FindFirstPointyShape(Shape[] shapes)
        {
            foreach (Shape s in shapes)
            {
                if (s is IPointy iss)
                {
                    return iss;
                }
            }
            return null;
        }
    }

    public class Shape : IEnumerable
    {
        public Shape[] randomShapes = { new Shape("Shape1"), new Shape("Shape2") };
        private string _name;

        public Shape() { }
        public Shape(string name) 
        { 
            _name = name;

        }
        public virtual void Draw() { }

        public string GetName => _name;

        public IEnumerator GetEnumerator() => randomShapes.GetEnumerator();
    }

    public class Pitchfork : IPointy
    {
        public byte Points => 3;
    }

    public class Square : Shape, IRegularPointy
    {
        public int SideLength { get; set; }
        public int NumberOfSides { get; set; }
        public byte Points => 4;

        public Square() { }
        public Square(string name) 
            : base(name) { }
        public override void Draw()
        {
            Console.WriteLine("Drawing a square"); 

        }
    }

    public class Circle : Shape
    {
        public Circle() { }
        public Circle(string name) 
            : base(name) { }

        public override void Draw() 
        {
            Console.WriteLine("Drawing the circle");
        }
    }

    public class Triangle : Shape, IPointy
    {
        public Triangle() { }
        public Triangle(string name) 
            : base(name) { }

        public override void Draw()
        {
            Console.WriteLine("Drawing the triangle, {0}", GetName);
        }

        public byte Points => 3;
        public void ExtraPointy()
        {
            Console.WriteLine("This triangle is extra pointy");
        }
    }

    public class Hexagon : Shape, IPointy, IDraw3D
    {
        public Hexagon() { }
        public Hexagon(string name)
            : base(name) { }

        public override void Draw()
        {
            Console.WriteLine("Drawing the hexagon, {0}", GetName);
        }

        public byte Points => 6;

        public void Draw3D()
        {
            Console.WriteLine("Drawing 3D Hexagon!");
        }

        public void ExtraPointy()
        {
            Console.WriteLine("This hexagon is extra pointy");
        }
    }

    public interface IPointy
    {
        //byte GetNumberOfPoints();
        //
        //string PropName { get; set; } 
        byte Points { get; }
        static IPointy() { }

        void ExtraPointy() { }
    }

    interface IRegularPointy : IPointy
    {
        int SideLength { get; set; }
        int NumberOfSides { get; set; }
        int Perimeter => SideLength * NumberOfSides;
        static string ExampleProperty { get; set; }

        static IRegularPointy() => ExampleProperty = "Foo";
    }

    interface IDraw3D
    {
        public void Draw3D();
    }
}