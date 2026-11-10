using System.Collections;

namespace CustomException_Practice2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car car_1 = new Car("Suze", 20);
            car_1.DisplayCarInfo();
            AccelerationMethod(car_1, -10);
            Console.WriteLine();

            Sedan sedan_1 = new Sedan("Rusty", 10);
            sedan_1.DisplayCarInfo();
            AccelerationMethod(sedan_1, 500);

        }
        public static void AccelerationMethod(Car currentCar, int accelerationAmount)
        {
            try
            {
                currentCar.Accelerate(accelerationAmount);
            }
            catch (TooFastException e) when (Car.MaxSpeed == 100)
            {
                Console.WriteLine("This car is donezo!");
                Console.WriteLine($"Error type: {e.Message}");
                Console.WriteLine($"Cause: {e.ErrorCause}");
                Console.WriteLine($"Time of Error: {e.ErrorTime}");
            }
            catch (CarIsDeadException e)
            {
                Console.WriteLine(e.InnerException);
            }
            catch (MyException e)
            {
                try
                {
                    String word = "Stuff";
                    int x = int.Parse(word);
                }
                catch (Exception e2)
                {
                    Console.WriteLine(e2.InnerException);
                }

            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"\tHelp link: {e.HelpLink}");
                foreach (DictionaryEntry d in e.Data)
                {
                    Console.WriteLine($"\t{d.Key} : {d.Value}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                currentCar.RadioState(false);
            }

        }
    }



    public class Car
    {
        public String PetName { get; set; } = String.Empty;
        public const int MaxSpeed = 100;
        public int CurrentSpeed { get; set; } = 0;
        private bool _carIsDead = false;
        private bool _engineExploded = false;
        public int EngineDamage { get; set; } = 0;


        Radio carRadio = new Radio();

        public virtual void NumberOfSeats() 
        {
            Console.WriteLine("No seat information to display");
        }

        public bool EngineState
        {
            get => _carIsDead;
        }
        public void RadioState(bool onOrOff)
        {
            carRadio.TunesState(onOrOff);
        }

        public Car() { }
        public Car(string name)
            : this(name, 0) { }
        public Car(String name, int speed)
        {
            PetName = name;
            CurrentSpeed = speed;
        }

        public void DisplayCarInfo()
        {
            Console.WriteLine("Pet name: {0}", PetName);
            Console.WriteLine("Max speed: {0}", MaxSpeed);
        }

        public void GetSpeed()
        {
            Console.WriteLine("Current speed: {0}", CurrentSpeed);
        }

        public void Accelerate(int delta)
        {
            if (delta > MaxSpeed)
            {
                throw new TooFastException("Acceleration error", "You accelerated too fast", DateTime.Now);
            }

            if (delta < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(delta), "Speed must be greater than 0")
                {
                    HelpLink = "www.aol.com",
                    Data =
                    {
                        { "Car name", PetName },
                        { "Max Speed", MaxSpeed },
                        { "Current Speed", CurrentSpeed },
                        { "Engine state", EngineState },
                        { "Engine damage", EngineDamage },
                    }
                };
            }
            if (_carIsDead)
            {
                Console.WriteLine("{0} is out of order and cannot accelerate...", PetName);
                EngineDamage += 1;

                if (EngineDamage >= 3)
                {
                    _engineExploded = true;
                }
                if (_engineExploded)
                {
                    Console.WriteLine("The engine has exploded");
                }
            }
            else
            {
                CurrentSpeed += delta;
                if (CurrentSpeed > MaxSpeed)
                {
                    CurrentSpeed = 0;
                    _carIsDead=true;
                    EngineDamage += 1;
                    throw new MyException($"{PetName} is now overheated!", "Too fast!")
                    {
                        HelpLink = "http://www.google.com",
                        Data =
                        {
                            { "Car name", PetName },
                            { "Current engine damage", EngineDamage }
                        }
                    };
                }
                Console.WriteLine("Car is now accelerated to: {0}", CurrentSpeed);
            }
        }
    }

    public class Sedan : Car
    {
        public const int Seats = 6;
        public int SeatCount => Seats;

        public Sedan() { }
        public Sedan(string name)
            : this(name, 0) { }
        public Sedan(String name, int speed)
            : base(name, speed) { }

        public override void NumberOfSeats()
        {
            Console.WriteLine($"Current seating: {SeatCount}");
        }
    }

    public class Radio
    {
        public void TunesState(bool onOrOff)
        {
            Console.WriteLine(onOrOff ? "Tunes On!" : "Quiet time...");
        }
    }

    public class CarIsDeadException : ApplicationException
    {
        public string CauseOfError {  get; set; }
        public DateTime ErrorTimeStamp { get; set; }

        public CarIsDeadException() { }
        public CarIsDeadException(string cause, DateTime time)
            : this(cause, time, String.Empty) { }

        public CarIsDeadException(string cause, DateTime time, string message)
            : this(cause, time, message, null) { }

        public CarIsDeadException(string cause, DateTime time, string message, System.Exception inner)
            : base(message, inner)
        { 
            CauseOfError = cause;
            ErrorTimeStamp = time;
        }
    }

    public class TooFastException : Exception
    {
        public string ErrorCause { get; set; }
        public DateTime ErrorTime;

        public TooFastException() { }
        public TooFastException(string message, DateTime time)
            : this(message, null, time) { }

        public TooFastException(string message, string cause, DateTime time)
            : base(message)
        {
            ErrorTime = time;
            ErrorCause = cause;
        }
    }
   
    public class MyException : Exception
    {

        public DateTime ErrorTimeStamp2 = DateTime.Now;
        public String ErrorCause { get; set; } = "Unknown cause";

        public MyException() { }
        public MyException(string message, string errorCause) 
            : this (message, errorCause, null) { }
        public MyException(string message, string errorCause, System.Exception inner) 
            : base(message, inner) 
        {
            ErrorCause = errorCause;
        }
    }
}