using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB3_C_SHARP
{

    class TeslaBattery
    {
        public Action BatteryLow = new Action(() =>
        {
            Console.WriteLine("Внимание низкий уровень заряда батареи!");
         
        } );
        private Func<bool> BatteryChargingСheck = new Func<bool>(delegate ()
        {
           return true;
        }
        );
        private double batteryСapacity;
        private double batteryDistance;
        private double batteryCurrent;
        private bool lowBatteryMode = false;

        private double batteryChargingTime;
        static Random rand = new Random();
        //Событие разряда батареи
        public TeslaBattery(string model)
        {
            switch (model)
            {
                case "Model X":
                    batteryСapacity = 63;
                    batteryDistance = 400;
                    batteryChargingTime = 8;
                    break;
                case "Model 3":
                    batteryСapacity = 66;
                    batteryDistance = 480;
                    batteryChargingTime = 8;
                    break;
                case "Model S":
                    batteryСapacity = 85;
                    batteryDistance = 450;
                    batteryChargingTime = 8;
                    break;
                default: throw new ArgumentException(nameof(model), "The specified Tesla model was not found"); 
            }
            BatteryLow += BatteryLowModOn;
            setBatteryRemaining();
        }
        public double BatteryTravelBehavior(double percentPerSec)//Изменение заряда при движении.
        {
            double batteryDecrease =  percentPerSec;

            if (batteryCurrent >= 10)
            {
                batteryCurrent -= batteryDecrease;
            }
            else if(batteryCurrent < 20 && lowBatteryMode == false)
            {
                BatteryLow?.Invoke();
            }
            else
            {
                batteryCurrent -= batteryDecrease;
            }
            if (batteryCurrent <= 0)
                throw new Exception("The battery has run out");
            return batteryCurrent;
        }
        private void BatteryLowModOn()
        {
            lowBatteryMode = true;
        }
        private void BatteryLowModOf()
        {
            lowBatteryMode = false;
        }
        public double BatteryCharging(double timeInSec)
        {
            if(BatteryChargingСheck())
                batteryCurrent += batteryChargingTime / 3600 * timeInSec;
            return batteryCurrent;
        }

        private void setBatteryRemaining()
        {
            batteryCurrent = rand.Next(0,101);
        }
        public double GetBatteryRemainingDistance()
        {
            return (GetBatteryFuelRemaining() / 100) * GetFullBatteryDistance();
        }
        public double GetBatteryFuelRemaining()
        {
            return batteryCurrent;
        }
        public double GetAvarageBatteryPercentPerKilometer()
        {
            return batteryСapacity / batteryDistance;
        }
        public double GetFullBatteryDistance()
        {
            return batteryDistance;
        }
        public override string ToString()
        {
            return $"Текущий остаток батареи: {batteryCurrent}" +
                $"\nЁмкость батареи: {batteryСapacity}" +
                $"\nЗапас хода: {batteryDistance}" +
                $"\nСкорость зарядки: {batteryChargingTime}";

        }
    }
    class Tesla : Car,ICar
    {
        public TeslaBattery teslaBattery { get; private set; }
        protected new Func<double, double, bool> travel;
        public string tesla_model { get; private set; }
       
        public Tesla(int releaseDate, int weight, int exploitation, double distanceTraveled, Engine engine,
             string carType, int numberOfSeats, string color, int fuelConsumption,string teslaModel) 
            : base(releaseDate,weight,exploitation,distanceTraveled,engine,carType,numberOfSeats,color,fuelConsumption)
        {
            engine.SetEnergySource("Электрический");
            this.tesla_model = teslaModel;
            teslaBattery = new TeslaBattery(teslaModel);

            travel = null;
            travel = (speed, batteryPerSecCons) =>
            {
                try
                {
                    teslaBattery.BatteryTravelBehavior(batteryPerSecCons);
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
        }
        public void ShowCarDashboard()//Отражает пожилую панель приборов
        {
            Console.WriteLine($"Текущая скорость:{ vehicleComponents.engine.EngineCurrentSpeed}" +
                $"\nОстаток батареи: {teslaBattery.GetBatteryFuelRemaining()}" +
                $"\nОстаток расстояния: {teslaBattery.GetBatteryRemainingDistance()}" +
                $"\nСкорость затраты батареи: {100/teslaBattery.GetFullBatteryDistance()*vehicleComponents.engine.EngineCurrentSpeed}%/ч");
        }
        public TeslaBattery BatteryChange(TeslaBattery newBattery)//Смена батареи 
        {
            teslaBattery = newBattery;
            return teslaBattery;
        }
        public override bool CarTravel(double speed)
        {
            return travel.Invoke(speed,BatteryConsumptionSecond(speed));
        }
  
        public double BatteryConsumptionSecond(double speed)
        {
            return (100 / teslaBattery.GetFullBatteryDistance())*(speed/3600);
        }

        public string OpenWindow(int numOfWindow)//Адскими ритуалами открывает окно.
        {
            return $"Окно {numOfWindow} открыто";
        }

        public string PlayMusic(string nameOfSong)//Включает музыку.
        {
            return $"Сейчас играет: {nameOfSong}";
        }
   
        public string UseWipers()//Использует дворники.
        {
            return $"Дворники начали работать";
        }
        public override string ToString()
        {
            return $"Модель: {tesla_model}" +
                $"\nID: {vehicleCurrentNumber}" +
                $"\nДата выпуска: {releaseDate}" +
                $"\nВремя эксплуатации: {exploitationTime} года" +
                $"\nВес: {weight}" +
                $"\n{ vehicleComponents.engine}" +
                $"\nПройденная дистанция: {distanceTraveled}" +
                $"\nКласс: {carType}" +
                $"\nКоличество пассажиров: {numberOfSeats}" +
                $"\nЦвет: {carColor}" +
                $"\nЗатрата батареи: {fuelConsumption}" +
                $"\nБатарея: {teslaBattery}\n";
        }

        public void Beep()
        {
            Console.Beep(500, 500);
        }
    }
}
