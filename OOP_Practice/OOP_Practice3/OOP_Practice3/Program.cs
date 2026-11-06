namespace OOP_Practice3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person("Homer", "Simpson", 50, "111-11-1111");
            Person p2 = new Person("Homer", "Simpson", 50, "111-11-1111");

            Console.WriteLine("p1.ToString() = {0}", p1.ToString());
            Console.WriteLine("p2.ToString() = {0}", p2.ToString());

            Console.WriteLine("p1 = p2?: {0}", p1.Equals(p2));
            Console.WriteLine("Same hash codes? {0}", p1.GetHashCode() == p2.GetHashCode());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(object.Equals(p1,p2));

            Person c1 = new Child("Steve", "Shade", 35, "111-11-2222");
            Child d = c1 as Child;
            Console.WriteLine(c1.ToString());
            Console.WriteLine(d.ToString());
            Console.WriteLine(object.Equals(c1,d));

            if (c1.GetHashCode() == d.GetHashCode())
            {
                Console.WriteLine("Same hash codes");
            }

            Console.WriteLine("c1 = d?: {0}", c1.Equals(d));

            if (c1 is Person c)
            {
                Console.WriteLine($"Object is Person type - {c.FirstName}");
            }
        }
    }

    public class Person
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int PersonAge { get; set; }
        public String Ssn { get; } = "";

        public Person(String fName, String lName, int personAge, String ssn)
        {
            FirstName = fName;
            LastName = lName;
            PersonAge = personAge;
            Ssn = ssn;
        }

        public override string ToString() => $"[First Name: {FirstName}; Last Name: {LastName}; Age: {PersonAge}]";
        public override bool Equals(object obj) => obj?.ToString() == ToString();
        public override int GetHashCode() => PersonAge.GetHashCode();
    }

    public class Child : Person
    {
        public Child(string fName, string lName, int personAge, string ssn) 
            : base(fName, lName, personAge, ssn) { }

        public override string ToString() => $"[First Name: {FirstName}; Last Name: {LastName}; Age: {PersonAge}]";
        public override int GetHashCode() => FirstName.GetHashCode();
        public override bool Equals(object obj) => obj.ToString() == ToString();
    }
}