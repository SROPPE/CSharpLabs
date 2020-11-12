using System;
using System.Linq;
using System.Threading;
namespace LB3_C_SHARP
{

    public enum LocationFrom
    { Server, GPS, God, None }
    public class Engine
    {
        public event EventHandler<FuelOverEventArgs> FuelIsOver;
        public delegate Action travel();
        protected string engineName;
        protected int enginePower;
        public double EngineCurrentSpeed { get; private set; }
        protected string engineEnergySource;
        protected double engineMileageReserve;
        public Tub engineTub { get; private set; }
        static Random rand = new Random(Guid.NewGuid().ToByteArray().Sum(x => x));

        private LocationFrom from;
        private Func<string> GetLocation;

        public Engine(string engineName, int enginePower, string energySource, string TubFuelCapacity,LocationFrom from)
        {
            SetEngineName(engineName);
            SetEnginePower(enginePower);
            SetEnergySource(energySource);
            engineTub = new Tub(TubFuelCapacity);
            this.from = from;
            SetLocation();
            engineMileageReserve = rand.Next(500000,1500000);
            
        }
        private void SetLocation()
        {
            if (from == LocationFrom.God)
            {
                GetLocation += () => { return "Бог помог"; };
            }
            else if(from == LocationFrom.GPS)
            {
                GetLocation += () => { return "Где-то1"; };
            }
            else if(from == LocationFrom.Server)
            {
                GetLocation += () => { return "Где-то2"; };
            }
            else
            {
                GetLocation += () => { return ""; };
            }
        }
        public Engine()
        {
            engineName = "Незвестно";
            enginePower = 40;
            engineEnergySource = "Незивестно";
        }

        private void SetEngineName(string engineName)
        {

            if (string.IsNullOrWhiteSpace(engineName))
            {
                throw new ArgumentNullException(nameof(engineName), "Cannot be empty");
            }
            if (int.TryParse(engineName, out int result))
            {
                this.engineName = engineName;
            }
            else throw new ArgumentException(nameof(engineName), "Incorrect data");
        }
        public string GetEngineName()
        {
            return this.engineName;
        }
        private void SetEnginePower(int engine_power)
        {
            this.enginePower = engine_power;
        }
        public int GetEnginePower()
        {
            return this.enginePower;
        }
        public double GetCurrentSpeed()//Возврашает текущую скорость.
        {
            EngineCurrentSpeed = rand.Next(0,250);
            return EngineCurrentSpeed;
        }
        public void ChangeEngineMileageReserve(double speed)
        {
            double KmSecond = speed / 3600;
            if (engineMileageReserve - KmSecond > 0)
                engineMileageReserve -= KmSecond;
            else 
            {
                engineMileageReserve = 0;
            }
                
        }
        public void SetEnergySource(string energySource)
        {
            switch (energySource)
            {
                case "Бензиновый": this.engineEnergySource = energySource; break;
                case "Электрический": this.engineEnergySource = energySource; break;
                case "Дизельный": this.engineEnergySource = energySource; break;
                default: throw new ArgumentException(nameof(energySource), "Unknown engine source");
            }

        }
        public string GetEnergySource()
        {
            return this.engineEnergySource;
        }
        public void EngineSpentFuel(double speed, double fuelConsumption)
        {
            double fuelPerSec = speed * fuelConsumption / (100 * 3600);
            if (engineTub.TubFuelRemaining - fuelPerSec >= 0)
                engineTub.TubFuelSpend(fuelPerSec);
            else
                OnFuelIsOver(new FuelOverEventArgs(GetLocation,true));
            
        }
        protected void OnFuelIsOver(FuelOverEventArgs e)
        {
            FuelIsOver?.Invoke(this, e);
        }
        public override string ToString()
        {

            return
               $"Название двигателя: {engineName}" +
               $"\nТип двигателя: {engineEnergySource}" +
               $"\nМощность двигателя(л.с): {enginePower}" +
               $"\nОстаток запаса хода: {engineMileageReserve}";
        }
        static public bool operator ==(Engine engine1, Engine engine2)
        {
            if (engine1.enginePower == engine2.enginePower)
                return true;
            else
                return false; 
        }
        static public bool operator !=(Engine engine1, Engine engine2)
        {
           
            if (engine1.enginePower != engine2.enginePower)
                return true;
            else
                return false;
        }
        static public bool operator >(Engine engine1, Engine engine2)
        {
            if (engine1.enginePower > engine2.enginePower)
                return true;
            else return false;
        }
        static public bool operator <(Engine engine1, Engine engine2)
        {
            if (engine1.enginePower < engine2.enginePower)
                return true;
            else return false;
        }
    }
}
