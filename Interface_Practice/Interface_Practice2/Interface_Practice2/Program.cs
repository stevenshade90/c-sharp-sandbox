namespace Interface_Practice2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<IVehicle> fleet = new List<IVehicle>();

            fleet.Add(new Car("Corvette"));
            fleet.Add(new Car("Focus"));
            fleet.Add(new Truck("Semi Rig", 40000));
            fleet.Add(new Motorcycle("Honda"));

            foreach (IVehicle vehicle in fleet)
            {
                vehicle.Accelerate(30);
                Console.WriteLine(vehicle.GetStatus());

                if (vehicle is IBrakeable brakingVehicle)
                {
                    brakingVehicle.Brake(30);
                }
                Console.WriteLine(vehicle.GetStatus());
                Console.WriteLine();
            }

            foreach (IVehicle vehicle in fleet)
            {
                if (vehicle is ITowable towableVehicle)
                {
                    towableVehicle.ToggleTowHitch(true);
                    Console.WriteLine($"This vehicle, {vehicle.Model}, is ready to hitch!"); 
                }
            }

            foreach (IVehicle vehicle in fleet)
            {
                Console.WriteLine(vehicle.GetStatus());
            }
        }

        public class Motorcycle : IVehicle
        {
            public String Model { get; set; }
            public int CurrentSpeed { get; set; }
            public int Wheels { get; set; }

            public Motorcycle(string model)
            {
                this.Model = model;
                this.CurrentSpeed = 0;
                this.Wheels = 2;
            }

            public void Accelerate(int amount)
            {
                this.CurrentSpeed += amount * 2;
            }

            public String GetStatus()
            {
                return $"This {this.Model} motorcycle is moving at {this.CurrentSpeed} MPH and has {this.Wheels} wheels";
            }

        }

        public class Car : IVehicle, IBrakeable
        {
            public String Model { get; set; }
            public int CurrentSpeed { get; set; }
            public int Wheels { get; set; }

            public Car(String model)
            {
                this.Model = model;
                this.CurrentSpeed = 0;
            }

            public void Accelerate(int amount)
            {
                this.CurrentSpeed += amount;
                this.Wheels = 4;
            }

            public void Brake(int amount)
            {
                this.CurrentSpeed -= amount;
                if (CurrentSpeed < 0)
                {
                    this.CurrentSpeed = 0;
                }
            }

            public String GetStatus()
            {
                String Status = $"The {this.Model} is currently moving at {this.CurrentSpeed} MPH and has {this.Wheels} wheels";
                return Status;
            }
        }

        public class Truck : IVehicle, IBrakeable, ITowable
        {
            public String Model { get; set; }
            public int CurrentSpeed { get; set; }
            public int CargoWeightLimit { get; set; }
            public int Wheels { get; set; }
            private bool _isHitchEngage;

            public Truck(String model, int cargoWeightLimit)
            {
                this.Model = model;
                this.CurrentSpeed = 0;
                this.Wheels = 4;
                this.CargoWeightLimit = cargoWeightLimit;
                this._isHitchEngage = false;
            }

            public void Accelerate(int amount)
            {
                this.CurrentSpeed += amount;
                if (this.CurrentSpeed > 80)
                {
                    CurrentSpeed = 80;
                }
            }

            public void Brake(int amount)
            {
                this.CurrentSpeed -= amount / 2;
                if (CurrentSpeed < 0)
                {
                    this.CurrentSpeed = 0;
                }
            }
            public void ToggleTowHitch(bool engaged)
            {
                this._isHitchEngage = engaged;
            }
            public String GetStatus()
            {
                string HitchStatus = this._isHitchEngage ? "Engaged" : "Disengaged";
                return $"This {this.Model} is current moving at {this.CurrentSpeed} MPH with a cargo limit of {this.CargoWeightLimit} lbs and {this.Wheels} wheels, and the hitch is {HitchStatus}";
            }
        }

        public interface IVehicle
        {
            string Model { get; }
            void Accelerate(int amount);
            String GetStatus();
        }

        public interface IBrakeable
        {
            void Brake(int amount);
        }

        public interface ITowable
        {
            void ToggleTowHitch(bool engaged);
        }

    }
}