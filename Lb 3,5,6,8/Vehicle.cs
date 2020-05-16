using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LB3_C_SHARP
{
    public struct VehicleComponents
    {
       public Engine engine;
       public Tub tub; 
    };
    public class VehicleList
    {
        static private List<Vehicle> vehicleNumbers = new List<Vehicle>();

        public Vehicle this[string UniqueVehicleNumber]
        {
            get
            {
                var vehicle = vehicleNumbers.FirstOrDefault(x => x.vehicleCurrentNumber ==UniqueVehicleNumber);
                return vehicle;
            }

        }
        public IEnumerable GetVehicleList()
        {
            return vehicleNumbers;
        }
        public void Add(Vehicle vehicle)
        {
            if (vehicle != null)
                vehicleNumbers.Add(vehicle);
        }

        public void Add(List<Vehicle> vehicles)
        {
            vehicleNumbers.AddRange(vehicles);
        }
    }
    public class Vehicle
    {
        protected VehicleComponents vehicleComponents = new VehicleComponents();
        protected SortedList<int, Engine> Engines = new SortedList<int, Engine>();

        static public VehicleList vehicleList = new VehicleList();
        protected Func<double, double, bool> travel;
        protected int releaseDate;
        protected int weight;
        protected int exploitationTime;
        protected double distanceTraveled;
        protected string vehicleType;
        public string vehicleCurrentNumber { get; private set; }     
        public Vehicle(int releaseDate, int weight, int exploitation, double distanceTraveled, string type, Engine engine)
        {
            SetReleaseDate(releaseDate);
            SetWeight(weight);
            SetExploitation(exploitation);
            SetVehicleType(type);
            vehicleComponents.engine = engine;
            vehicleComponents.tub = engine.engineTub;
            Engines.Add(int.Parse(vehicleComponents.engine.GetEngineName()), vehicleComponents.engine);
            SetDistanceTraveled(distanceTraveled);
            travel = (speed, fuel_consum) =>
            {
                try
                {
                    vehicleComponents.engine.EngineSpentFuel(speed, fuel_consum);
                    vehicleComponents.engine.ChangeEngineMileageReserve(speed);
                    DistanceMeterIncrease(speed);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.White;

                    return false;
                }
                return true;

            };
            engine.ChangeEngineMileageReserve(distanceTraveled);
            vehicleCurrentNumber = Guid.NewGuid().ToString();
            using (StreamWriter streamWriter = new StreamWriter(@"C:\Users\User\source\repos\LB3 C SHARP\VehicleList.txt", true, Encoding.UTF8))
            {
                streamWriter.WriteLine(vehicleCurrentNumber);
            }
            vehicleList.Add(this);
         
        }

        public Vehicle(int realeaseDate, int weight, int exploitation, double distanceTraveled, Engine engine)
            : this(realeaseDate, weight, exploitation, distanceTraveled, "Наземный", engine) { }

        public Vehicle(int realeaseDate, int weight, double distanceTraveled)
            : this(realeaseDate, weight, 0, distanceTraveled, "Наземный", new Engine()) { }

        public Vehicle(int releaseDate, int weight, double distanceTraveled, string type, Engine engine)
           : this(releaseDate, weight, 0, distanceTraveled, type, engine) { }

        public Engine this[int Engine_Num]
        {
            get
            {
                return Engines[Engine_Num];
            }
            set
            {
                if (value != null)
                    Engines[Engine_Num] = value;
            }
        }
        public Engine GetEngine()
        {
            return vehicleComponents.engine;
        }
        public Tub GetTub()
        {
            return vehicleComponents.tub;
        }
        private void SetDistanceTraveled(double distanceTraveled)
        {
            if (distanceTraveled < 0 || (this.exploitationTime > 0 && distanceTraveled == 0))
            {
                throw new ArgumentException(nameof(distanceTraveled), "Cannot be negative");
            }
            this.distanceTraveled = distanceTraveled;
        }

        private void SetReleaseDate(int date)
        {
            this.releaseDate = date;
        }

        public int GetRealeaseDate()
        {
            return this.releaseDate;
        }

        private void SetWeight(int weight)
        {

            if (weight < 0)
                throw new ArgumentNullException(nameof(weight), "Cannot be negative");
            this.weight = weight;
        }

        public int GetWeight()
        {
            return this.weight;
        }

        private void SetExploitation(int exploitation)
        {

            if (exploitation > DateTime.Now.Year - releaseDate)
                throw new ArgumentException(nameof(exploitationTime), "Incorrect exploitation time");
            this.exploitationTime = exploitation;
        }

        public int GetExploitation()
        {
            return this.exploitationTime;
        }

        private void SetVehicleType(string type)
        {

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "Cannot be empty");
            switch (type)
            {
                case "Наземный": this.vehicleType = type; break;
                case "Воздушный": this.vehicleType = type; break;
                case "Водный": this.vehicleType = type; break;
                case "Космический": this.vehicleType = type; break;
                default: throw new ArgumentException(nameof(type), "Unknown vehicle type");
            }

        }

        public string GetVehicleType()
        {
            return this.vehicleType;
        }
        
        protected virtual double DistanceMeterIncrease(double speed)
        {
            double distance_traveled = speed / 3600;
            this.distanceTraveled += distance_traveled;
            return this.distanceTraveled;
        }
        public virtual double DistanceMeterIncrease(string distance_traveled)
        {
            for (int i = 0; i < distance_traveled.Length; i++)
            {
                if (distance_traveled[i] < 48 || distance_traveled[i] > 57)
                    throw new ArgumentException(nameof(distance_traveled), "Data is incorrect");
            }
            if(Convert.ToDouble(distance_traveled) <0)
            {
                throw new ArgumentException(nameof(distance_traveled), "Cannot be negative");
            }
            this.distanceTraveled += Convert.ToInt32(distance_traveled);
            return this.distanceTraveled;
        }
        
        public Engine VehicleEngineChange(Engine NewEngine,int name)
        {
            if (Engines.ContainsKey(name))
            {
                Engines[name] = NewEngine;
                vehicleComponents.engine = NewEngine;
                return Engines[name];
            }
            return null;
        }
 
        public override string ToString()
        {
            string x = "";
            foreach (var eng in Engines.Keys)
            {
                x += Engines[eng];
            }
           
            return $"Дата выпуска: {releaseDate}" +
                $"\nВремя эксплуатации: {exploitationTime}" +
                $"\nТип траноспортного средства: {vehicleType}" +
                $"\nМасса транспортного средства: {weight}" +
                $"\n{x.Trim()}" +
                $"\nПройдено километров: {distanceTraveled}" +
                $"\nId: {vehicleCurrentNumber}";
        }
    }
}
