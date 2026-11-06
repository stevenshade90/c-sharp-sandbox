using System.Collections;

namespace OOP_Practice2
{
    internal class Program
    {
        static void Main()
        {
            Dog dog1 = new ("Butch", "Doberman", 3);
            Console.WriteLine("To string info: {0}", dog1.ToString());
            Console.WriteLine("Hashcode value: {0}", dog1.GetHashCode());
            dog1.DisplayStats();
            dog1.GetVetInfo();
            dog1.GetHashCode();
            Console.WriteLine();


            try
            {
                dog1.ExceptionThrower();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.TargetSite);
                Console.WriteLine(e.HelpLink);

                foreach (DictionaryEntry de in e.Data)
                {
                    Console.WriteLine("{0} : {1}", de.Key, de.Value);
                }
            }

            Dog dog2 = dog1;
            Console.WriteLine("To string info: {0}", dog2.ToString());
            Console.WriteLine("Hashcode value: {0}", dog2.GetHashCode());
            dog2.ToString();
            dog2.DisplayStats();
            dog2.GetVetInfo();
            dog2.GetHashCode();
            Console.WriteLine();

            Console.WriteLine("Same hash?: {0}", dog1.GetHashCode() == dog2.GetHashCode());
            Console.WriteLine("Dog 1 == dog 2?: {0}", dog1.Equals(dog2));

            if (dog1 is Animal ee && dog1 is Dog)
            {
                Console.WriteLine("It's an animal!");
                Console.WriteLine(ee.Name);
            }
        }
    }

    public abstract class Animal : object
    {
        public VetUnit animalVet = new VetUnit();

        public string Name { get; set; } = "";
        public string Breed { get; set; } = "";
        public int Age { get; set; } = 0;

        public Animal() { }
        public Animal(string name, string breed, int age)
        {
            Name = name;
            Breed = breed;
            Age = age;
        }

        public abstract void DisplayStats();
        public void VetInfo()
        {
            animalVet.VetInfo();
        }
    }

    public class Dog : Animal 
    {
        public Dog() { }
        public Dog(string name)
            : base(name, "", 0) { }
        public Dog(string name, string breed)
            : base(name, breed, 0) { }
        public Dog(string name, string breed, int age)
            : base(name, breed, age) { }

        public override bool Equals(object? obj) => obj?.GetHashCode() == GetHashCode();
        public override string ToString() => $"[Name: {base.Name}; Breed: {base.Breed}; Age: {base.Age}]";  
        public override int GetHashCode() => Name.GetHashCode();

        public override void DisplayStats()
        {
            Console.WriteLine("\tName: {0}", base.Name);
            Console.WriteLine("\tBreed: {0}", base.Breed);
            Console.WriteLine("\tAge: {0}", base.Age);
        }

        public void GetVetInfo()
        {
            base.VetInfo();
        }

        public void ExceptionThrower()
        {
            throw new Exception("Error thrown")
            {
                HelpLink = "www.Google.com",
                Data =
                {
                    { "Timestamp", DateTime.Now }
                }
            };
        }
    }

    public class VetUnit
    {
        private const string _unitName = "Williamsport Vet Co.";
        private const int _unitId = 12345678;
        private const string _specialty = "Emergency";

        public static string UnitName => _unitName;
        public static int UnitId => _unitId;
        public static string Specialty => _specialty;

        public void VetInfo()
        {
            Console.WriteLine("\t\t=>Vet Name: {0}", UnitName);
            Console.WriteLine("\t\t=>ID: {0}", UnitId);
            Console.WriteLine("\t\t=>Specialty: {0}", Specialty);
        }
    }
}