using System.Reflection;

namespace Reflection_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyHiddenClass mhc = new MyHiddenClass();
            Type type = typeof(MyHiddenClass);
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            DisplayInfo(mhc, "Type of object mhc");
            DisplayInfo(fields, "Fields of MyHiddenClass");


            foreach (FieldInfo field in fields)
            {
                try
                {
                    Console.WriteLine($"Current Field: {field.Name}");
                    field.SetValue(mhc, "Changing values in mhc");
                }
                catch { }
            }

            foreach (FieldInfo field in fields)
            {
                Console.WriteLine(field.GetValue(mhc));
            }
        }

        static void DisplayInfo<T>(T data, string inputText)
        {
            if (data is MyHiddenClass mhc)
            {
                Console.WriteLine($"{inputText}: {mhc.GetType().Name}");
            }
            else if (data is FieldInfo[] fields)
            {
                Console.WriteLine(inputText);
                foreach (var item in fields)
                {
                    Console.WriteLine($"\t{item}");   
                }
            }

        }
    }

    public class MyHiddenClass
    {
        private string _hiddenInfo = "Hidden value 1";
        private string _MoreInfo = "Hidden value 2";
        private int _count = 5;
    }
}