using System.Text.Json;
using System.Text.Json.Serialization;

namespace Json_Serialization_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                IncludeFields = true,
                WriteIndented = true
            };

            Person p1 = new Person() { FirstName = "Steve", LastName = "Shade", Profession = new string[] { "Chef", "Landscaper" }, Age = 36 };
            p1.personPet.PetName = "Cookie";
            p1.personPet.PetBreed = "Golden Retriever";
            p1.personPet.PetAge = 3;

            SaveAsJson<Person>(p1, options, "");
            Console.WriteLine("Serialized p1!");
            Console.WriteLine($"p1 ToString(): {p1.ToString()}");
            Console.WriteLine();
           

            Console.WriteLine("Deserializing p1!");
            Person savedPerson = ReadFromJson<Person>(options, "");
            Console.WriteLine($"savedPerson ToString(): {savedPerson.ToString()}");
        }

        static void SaveAsJson<T>(T objGraph, JsonSerializerOptions options, string fileName)
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize(objGraph, options));
        }

        static T ReadFromJson<T>(JsonSerializerOptions options, string fileName)
        {
            return JsonSerializer.Deserialize<T>(File.ReadAllText(fileName), options);
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Profession { get; set; }
        public int Age { get; set; }

        [JsonInclude]
        public string hairColor = "Brown";
        [JsonInclude]
        public bool isAlive = true;

        public Pet personPet;

        public Person() 
        {
            personPet = new Pet();
        }

        public string ToString()
        {
            return $"FirstName: {FirstName} -- LastName: {LastName} -- Profession: {Profession[0]} -- Age: {Age} -- HairColor: {hairColor} -- isAlive: {isAlive}";
        }
    }

    public class Pet
    { 
        public string PetName { get; set; }
        public string PetBreed { get; set; }
        public int PetAge { get; set; }
    }
}