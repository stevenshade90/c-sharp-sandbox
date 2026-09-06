using System.Collections.ObjectModel;


namespace ObservableCollection
{
    using System.Collections.Specialized;
    internal class Program
    {
        static void Main(string[] args)
        {

            ObservableCollection<String> peopleToObserve = new ObservableCollection<String>()
            {
                "Steve",
                "Tim",
                "Jeff",
            };

            peopleToObserve.CollectionChanged += PeopleToObserve_CollectionChanged;

            Console.WriteLine("Armed...");
            Thread.Sleep(2000);

            peopleToObserve.Add("Mark");
            peopleToObserve.Remove("Steve");
            peopleToObserve[0] = "New Person";
            peopleToObserve.Clear();

            peopleToObserve.Add("Jim");
            peopleToObserve.Add("John");
            peopleToObserve.Move(0, 1);
            peopleToObserve.Move(0, 1);
        }

        public static void PeopleToObserve_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Action for the event: {0}", e.Action);

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Console.WriteLine("New items");
                foreach (String s in e.NewItems)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                Console.WriteLine("Old items");
                foreach (string s in e.OldItems)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                Console.WriteLine("The collection was cleared.");
                Console.WriteLine();
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                Console.WriteLine("Replaced items");
                foreach (string s in e.OldItems)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine("With new items:");
                foreach (string s in e.NewItems)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Move)
            {
                Console.WriteLine("Moved item:");
                foreach (string s in e.NewItems)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
            }

        }  
    }
    public enum NotifyCollectionChangedAction
    {
        Add = 0,
        Remove = 1,
        Replace = 2,
        Move = 3,
        Reset = 4
    }
}