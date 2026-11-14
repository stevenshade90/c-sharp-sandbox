namespace BitwiseOperations_Practice2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DogBreed dog = DogBreed.Golden;
            Console.WriteLine(Enum.GetUnderlyingType(dog.GetType()));
            Console.WriteLine(dog.GetType().Name);
            Console.WriteLine(dog.ToString());
            Console.WriteLine((byte)dog);

            Console.WriteLine();
            Array DogBreedValues = Enum.GetValues(dog.GetType());
            for (int i = 0; i < DogBreedValues.Length; i++)
            {
                Console.WriteLine(DogBreedValues.GetValue(i) + " : " + "{0, -10:d}", DogBreedValues.GetValue(i));
            }
            Console.WriteLine();

            DogBreed combinedDog = DogBreed.Golden | DogBreed.Boxer;

            Console.WriteLine("{0}", (combinedDog | DogBreed.Golden) == combinedDog); // (0101 | 0001 = 0101 ) == 0101
            Console.WriteLine("{0}", (combinedDog | DogBreed.Jack_Russel) == combinedDog); // (0101 | 0010 = 0111) != 0101
            Console.WriteLine("{0}", (combinedDog | DogBreed.Boxer) == combinedDog); // (0101 | 0100 = 0101) == 0101
            Console.WriteLine("{0}", (combinedDog | DogBreed.Cocker) == combinedDog); // (0101 | 1000 = 1101) != 0101
            Console.WriteLine("{0}", (combinedDog | DogBreed.Golden | DogBreed.Boxer) == combinedDog); // (0101 | 0001 | 0100 = 0101) == 0101

            DogBreedCheck(combinedDog);
        }

        static void DogBreedCheck (DogBreed e)
        {
            switch (e)
            {
                case DogBreed.Golden:
                    Console.WriteLine("This dog is golden");
                    break;

                case DogBreed.Jack_Russel:
                    Console.WriteLine("This dog is JR");
                    break;

                case DogBreed.Boxer:
                    Console.WriteLine("This dog is boxer");
                    break;

                case DogBreed.Golden | DogBreed.Boxer:
                    Console.WriteLine("This dog is a golden and boxer");
                    break;

                case DogBreed.Cocker:
                    Console.WriteLine("This dog is cocker");
                    break;

                default:
                    break;
            }
        }
        public enum DogBreed : byte
        { 
            Golden = 1,
            Jack_Russel = 2,
            Boxer = 4,
            Cocker = 8
        }
    }
}
