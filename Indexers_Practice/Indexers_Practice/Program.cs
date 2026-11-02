using System.Collections;

namespace ConsoleApp115
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<Person> collectionOfPeople = new List<Person>
            {
                new Person("Steve", 35),
                new Person("Tim", 38),
                new Person("Jeff", 41), 
            };

            PersonCollection peoplesCollections = new PersonCollection();
            for (int i = 0; i <  collectionOfPeople.Count; i++) 
            {
                peoplesCollections[i] = collectionOfPeople[i];
            }

            for (int i = 0; i < peoplesCollections.peopleCollection.Count; i++)
            {
                Person? p = peoplesCollections[i] as Person;
                if (p != null)
                {
                    Console.WriteLine("Casting complete!");
                    Console.WriteLine(peoplesCollections[i]);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Null!");
                }
            }

            /*
             * Separating from List Indexers
             * to
             * Dictionary Indexers 
             */

            peoplesCollections["Josh"] = new Person("Josh", 34);
            peoplesCollections["Kelsey"] = new Person("Kelsey", 33);

            Console.WriteLine(peoplesCollections["Josh"]);
            Console.WriteLine(peoplesCollections["Kelsey"]);
        }
    }

    public class Person
    {
        private String _name;
        private int _age;

        public String Name => _name;
        public int Age => _age; 
        public Person(string name, int age)
        {
            _name = name;
            _age = age;
        }

        public override string ToString() => $"Name: {Name} - Age: {Age}";    
    }


    public class PersonCollection 
    {
        public ArrayList peopleCollection = new ArrayList();
        public Dictionary<String, Person> dictionaryCollection = new Dictionary<String, Person>();

        public PersonCollection() { }
        public PersonCollection (Person p)
        {
            peopleCollection.Add(p);
        }

        public Person this[int index]
        {
            get => (Person)peopleCollection[index];
            set => peopleCollection.Insert(index, value);
        }

        public Person this[String name]
        {
            get => (Person)dictionaryCollection[name];
            set => dictionaryCollection[name] = value;
        }
    }
}