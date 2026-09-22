using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Delegates_Practice
{
    internal class Program
    {
        public delegate int MathOps(int x, int y);
        public delegate void Warning(string s);
        public delegate void GenericDelegate<T>(T arg);
        public delegate void GenericTupleDelegate<T1, T2>((T1, T2) tuple);
        public delegate void WarningWithEventArg(object o, CustomEventArgs e);

        static event Warning MajorWarningEvent;
        static event Warning MinorWarningEvent;
        static event WarningWithEventArg WarningWithArgs;

        static event EventHandler<CustomEventArgs> WarnWithArgs;


        static void Main(string[] args)
        {
            //// Testing a basic math operation delegate
            //MathOps mo = new MathOps(Add);
            //mo += Subtract;
            //mo += Multiply;
            //mo += Divide;

            //foreach (MathOps delType in mo.GetInvocationList())
            //{
            //    int result = delType.Invoke(11, 7);

            //    Console.WriteLine($"Method Name: {delType.Method.Name}");
            //    Console.WriteLine($"Method Attributes: {delType.Method.Attributes}");
            //    Console.WriteLine($"Method Target: {delType.Target ?? "None"}");
            //    Console.WriteLine($"Result: {result}\n");
            //}


            //// Testing a basic delegate with strings, and method group conversion syntax
            //Warning w = new Warning(WarningMessage);
            //WarningInvocation(w);
            //WarningInvocation(mo);
            //WarningInvocation(new Warning(WarningMessage)); // Method Group Conversion 


            //// Testing generic delegates with different inputs
            //GenericDelegate<string> genericStringDelegate = new GenericDelegate<string>(GenericStringTarget);
            //GenericDelegate<int> genericIntDelegate = new GenericDelegate<int>(GenericIntTarget);
            //genericStringDelegate.Invoke("Goodbye, World!");
            //genericIntDelegate.Invoke(42);

            //GenericTupleDelegate<string, int> genericTupleDelegate = new GenericTupleDelegate<string, int>(GenericTupleTarget);
            //genericTupleDelegate.Invoke(("Goodbye, World!", 100));

            //GenericTupleDelegate<int, string> genericTupleDelegate2 = new GenericTupleDelegate<int, string>(GenericTupleTarget);
            //genericTupleDelegate2.Invoke((001, "Goodbye, World! Tuple flipped!"));

            //GenericTupleDelegate<double, double> genericTupleDelegate3 = new GenericTupleDelegate<double, double>(GenericTupleTarget);
            //genericTupleDelegate3.Invoke((3.14, 2.71));

            //GenericDelegate<string> genDelStr = new GenericDelegate<string>(GenericTarget<string>);
            //genDelStr.Invoke("Hello!");

            //GenericDelegate<int> genDelInt = new GenericDelegate<int>(GenericTarget<int>);
            //genDelInt.Invoke(2);


            //// Action/Func practice
            //Action<string, string, string> a1 = ActionTarget;
            //a1.Invoke("Hello", "Goodbye", "World");

            //Action<int, int, bool> a2 = ActionTarget;
            //a2.Invoke(1, 2, true);

            //Action<object, List<string>, Dictionary<string, int>> a3 = ActionTarget;
            //a3.Invoke(null, new List<string> { "Item 1", "Item 2" }, new Dictionary<string, int> { { "Key 1", 1 }, { "Key 2", 2 } });


            //// Events practice
            //Console.WriteLine("Beginning Events practice");
            //MajorWarningEvent += MajorWarningMessage;
            //MinorWarningEvent += MinorWarningMessage;

            //Console.Write("Enter a number: ");
            //string num = Console.ReadLine();

            //if (num == "1")
            //{
            //    MajorWarningEvent?.Invoke("This is a major warning event");
            //}
            //else
            //{
            //    MinorWarningEvent?.Invoke("This is just a minor warning event");
            //}

            //Console.WriteLine("Testing EventArgs");
            //WarningWithArgs += WarningWithArgsMethod;

            //WarningWithArgs.Invoke(new { Name = "Steve" }, new CustomEventArgs("In the custom event args"));


            //Console.WriteLine("\nTesting generic EventHandler<T>");
            //EventHandler<CustomEventArgs> e = WarningWithArgsMethod;
            //e += AlternateWarningWithArgs;

            //e.Invoke(new { Name = "Steve" }, new CustomEventArgs("In the custom event args"));

            //Program.WarnWithArgs += WarningWithArgsMethod;
            //Program.WarnWithArgs.Invoke("Testing static", new CustomEventArgs("Test"));

            Console.WriteLine("Anonymous Methods");
            Program.MajorWarningEvent += delegate (string s)
            {
                Console.WriteLine("Oh no!");
                Console.WriteLine(s);
            };

            Program.MajorWarningEvent.Invoke("Yikes");

            Program.WarnWithArgs += static delegate (object sender, CustomEventArgs e)
            {
                Console.WriteLine("In the WarnWithArgs anonymous method");
                Console.WriteLine($"Sender: {sender}");
                Console.WriteLine($"Arg message: {e.msg}");
                Console.WriteLine($"ID Num: {e.idNum}");
            };

            Program.WarnWithArgs.Invoke(new { Name = "Steve" }, new CustomEventArgs("New message"));

        }


        static int Add(int x, int y) => x + y;
        static int Subtract(int x, int y) => x - y;
        static int Multiply(int x, int y) => x * y;
        static int Divide(int x, int y) => x / y;

        static void WarningInvocation(Delegate d)
        {
            if (d is Warning warningDelegate)
            {
                for (int i = 0; i <= 10; i++)
                {
                    if (i <= 3)
                    {
                        warningDelegate.Invoke($"This is an EARLY warning message {i + 1}");
                    }
                    else if (i <= 6)
                    {
                        warningDelegate.Invoke($"This is a MID warning message {i + 1}");
                    }
                    else if (i < 10)
                    {
                        warningDelegate.Invoke($"This is a LATE warning message {i + 1}");
                    }
                    else
                    {
                        warningDelegate.Invoke("");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("The delegate is not of type Warning.");
            }
        }
        static void WarningMessage(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                Console.WriteLine("Warning message is empty or null.");
                return;
            }
            Console.WriteLine("A Warning Message");
            Console.WriteLine($"\t => {s}");
        }
        static void MajorWarningMessage(string s)
        {
            Console.WriteLine("A Major Warning Message");
            Console.WriteLine($"\t => {s}");
        }
        static void MinorWarningMessage(string s)
        {
            Console.WriteLine("A Minor Warning Message");
            Console.WriteLine($"\t => {s}");
        }
        static void WarningWithArgsMethod(object o, CustomEventArgs e)
        {
            Console.WriteLine($"Sent from {o}, message: {e.msg}");
            Console.WriteLine($"Identification: {e.idNum}");
        }
        static void AlternateWarningWithArgs(object o, CustomEventArgs e)
        {
            Console.WriteLine("In the alternate warning method");
        }

        static void GenericStringTarget(string s)
        {
            Console.WriteLine($"Generic string target, displaying string message: {s}");
        }
        static void GenericIntTarget(int x)
        {
            Console.WriteLine($"Generic int target, displaying int message: {x}");
        }
        static void GenericTarget<T>(T arg)
        {
            Console.WriteLine($"Generic target, displaying message of type {typeof(T)}: {arg}");
        }
        static void GenericTupleTarget<T1, T2>((T1, T2) tuple)
        {
            var (val1, val2) = tuple; // Deconstruction

            Console.WriteLine($"Generic tuple target!");
            Console.WriteLine($"\t => Value 1: {val1}");
            Console.WriteLine($"\t => Value 2: {val2}");
            Console.WriteLine();
        }

        static void ActionTarget<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
        {
            Console.WriteLine($"Action target with 3 parameters of types {typeof(T1)}, {typeof(T2)}, and {typeof(T3)}");
            Console.WriteLine($"\t => Arg 1: {arg1}");
            Console.WriteLine($"\t => Arg 2: {arg2}");
            Console.WriteLine($"\t => Arg 3: {arg3}");
            Console.WriteLine();
        }
        static Type FuncTarget<T1, T2, TResult>(T1 arg1, T2 arg2)
        {
            Console.WriteLine($"Func target with 2 parameters of types {typeof(T1)} and {typeof(T2)}, returning type {typeof(TResult)}");
            Console.WriteLine($"\t => Arg 1: {arg1}");
            Console.WriteLine($"\t => Arg 2: {arg2}");
            Console.WriteLine();

            return typeof(TResult);
        }   
    }

    public class CustomEventArgs : EventArgs
    {
        public readonly string msg;
        public Guid idNum { get; set; }

        public CustomEventArgs(string s)
        {
            msg = s;
            idNum = Guid.NewGuid();
        }
    }
}