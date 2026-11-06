namespace OOP_Practice4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Old OOP practice, testing original accessors and mutators, enums added with pattern matching

            /*
            Car car1 = new Car(135_000, "Chevrolet", "Cruze", 2016, 20_000, FinalPriceCalculator.Standard);
            car1.DisplayCarInfo();
            Console.WriteLine();

            Garage garage1 = new Garage();
            garage1.carValues = car1;
            Console.WriteLine(garage1.numberOfCars);
            Console.WriteLine(garage1.carValues.PricePaid);

            Car car2 = new Car("Ford", "Focus", 2012, 8_000, FinalPriceCalculator.Student);
            car2.DisplayCarInfo();
            Console.WriteLine();
            */

            Car car3 = new Car(9700, "Chevrolet", "Malibu", 2014, 15_000, FinalPriceCalculator.Employee) { buyerName = "Steve", buyerAge = 35 };
            car3.DisplayCarInfo();

            car3.FinalPrice(Car.Tax);
            car3.DisplayCarInfo();

            /*
            Garage garage2 = new Garage(car2, 1);
            Console.WriteLine();
            Console.WriteLine("In the Garage");
            garage2.carValues.DisplayCarInfo();

            Console.WriteLine(garage1.numberOfCars);
            Console.WriteLine(garage2.numberOfCars);

            Garage garage3 = new Garage
            {
                carValues = new Car(196_000, "Chevrolet", "Equinox", 2025, 36000, FinalPriceCalculator.Gift) { buyerName = "Tim", buyerAge = 38  },
            };

            Console.WriteLine();
            Console.WriteLine();
            garage3.carValues.DisplayCarInfo();
            */
        }
    }

    public class Car
    {
        private static readonly String _manufacturer;
        private static readonly double _tax;

        private String _make;
        private String _model;
        private int _year;
        private int _mileage;

        private double _currentPrice;
        private double _pricePaid;

        private FinalPriceCalculator _priceCalculator;
        private Addons _addons;

        public String buyerName { get; init; } = "No Name";
        public int buyerAge { get; init; }

        public String Manufacturer
        {
            get => _manufacturer;
        }

        public static double Tax
        {
            get => _tax;
        }

        public double CurrentPrice
        {
            get => _currentPrice;
            private set => _currentPrice = value;
        }

        public int? Mileage
        {
            get => _mileage;
            set => _mileage = value ?? 0;
        }

        public String Make
        {
            get => _make;
            set => _make = value;
        }

        public String Model
        {
            get => _model;
            set => _model = value;
        }

        public int Year
        {
            get => _year;
            set => _year = value;
        }

        public double PricePaid
        {
            get => _pricePaid;
            set => _pricePaid = value;
        }

        public int Age
        {
            get => buyerAge;
        }

        public FinalPriceCalculator PriceCalculator
        {
            get => _priceCalculator;
            set => _priceCalculator = value;
        }

        public Addons CarAddons
        {
            get => _addons;
            set => _addons = value;
        }

        static Car()
        {
            _manufacturer = "GM";
            _tax = 1000;
        }

        public Car() { }

        public Car(String make, String model, double currentPrice, FinalPriceCalculator priceCalculator)
            : this(0, make, model, 0, currentPrice, priceCalculator) { }

        public Car(String make, String model, int year, double currentPrice, FinalPriceCalculator priceCalculator)
            : this(0, make, model, year, currentPrice, priceCalculator) { }

        public Car(int? mileage, String make, String model, int year, double currentPrice, FinalPriceCalculator priceCalculator)
        {
            Mileage = mileage;
            Make = make;
            Model = model;
            Year = year;

            CurrentPrice = currentPrice;
            PricePaid = currentPrice;
            PriceCalculator = priceCalculator;
        }

        public void DisplayCarInfo()
        {
            Console.WriteLine("Manufacturer: {0}", Manufacturer);
            Console.WriteLine("Make: {0}", Make);
            Console.WriteLine("Model: {0}", Model);
            Console.WriteLine("Year: {0: ####}", Year);
            Console.WriteLine("Mileage: {0:#,##0}", Mileage);
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Price Paid: {0:C}", CurrentPrice);
            Console.WriteLine("Price Type: {0}", PriceCalculator);
            Console.WriteLine("Final Price: {0:C}", PricePaid);
        }

        public void FinalPrice(double taxAmount)
        {
            PricePaid = this switch
            {
                { PriceCalculator: FinalPriceCalculator.Standard, Age: >= 18 }
                    => PricePaid = CurrentPrice + (taxAmount * 1.00),
                { PriceCalculator: FinalPriceCalculator.Student, Age: >= 18 }
                    => PricePaid = CurrentPrice + (taxAmount * 0.75),
                { PriceCalculator: FinalPriceCalculator.Employee, Age: >= 18 }
                    => PricePaid = CurrentPrice + (taxAmount * 0.50),
                { PriceCalculator: FinalPriceCalculator.Gift, Age: >= 18 }
                    => PricePaid = 0,
                _ => CurrentPrice
            }; 
        }

        public String AddonsForCar()
        {
            String additions = this switch
            {
                { CarAddons: Addons.HeatedSeats }
                    => "Adding heated seats",
                { CarAddons: Addons.ExtendedWarranty }
                    => "Adding extended warranty",
                { CarAddons: Addons.SelfDriving }
                    => "Adding self driving capabilities",
                { CarAddons: Addons.Tint }
                    => "Tinting all windows",
                _ => "No additions"

            };
            return additions;
        }
    }

    public class Garage
    {
        public int numberOfCars { get; set; } = 0;
        public Car carValues {  get; set; }

        public Garage()
        {
            carValues = new Car();
            numberOfCars = 1;
        }
        
        public Garage (Car carValues, int number)
        {
            this.carValues = carValues;
            numberOfCars = number;
        }
    }

    public enum FinalPriceCalculator
    {
        Standard,
        Student,
        Employee,
        Gift,
    }

    public enum Addons
    {
        HeatedSeats,
        SelfDriving,
        ExtendedWarranty,
        Tint,
    }
}