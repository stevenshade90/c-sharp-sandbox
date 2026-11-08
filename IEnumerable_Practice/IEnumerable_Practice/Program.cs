using System.Collections;

namespace IEnumerable_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Garage g = new Garage();

            foreach (Car c in g)
            {
                Console.WriteLine(c.GetName);
            }
        }
    }

    public class Garage 
    {
        private Car[] carArray = new Car[4];
        public Garage()
        {
            carArray[0] = new Car("Rusty", 50);
            carArray[1] = new Car("Speedy", 25);
            carArray[2] = new Car("Zippy", 35);
            carArray[3] = new Car("Hunker", 200);
        }
        public IEnumerator GetEnumerator()
        {
            foreach (Car c in carArray)
            {
                yield return c;
            }
        }
    }

    public class  Car
    {
        private string _name;
        private int _speed;

        public Car() { }
        public Car(string name, int speed)
        { 
            _name = name;
            _speed = speed;
        }

        public string GetName => _name;
        public int GetSpeed => _speed;
    }
}