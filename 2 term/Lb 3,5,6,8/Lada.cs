using System;
using System.Linq;
using System.Threading;

namespace LB3_C_SHARP
{
    public class Lada : Car, ICar
    {

        Random rand = new Random(Guid.NewGuid().ToByteArray().Sum(x => x));
        public string ladaModel { get; private set; }
        public Lada(int releaseDate, int weight, int exploitation, int distanceTraveled, Engine engine,
        string carType, int numberOfSeats, string color, int fuelConsumption, string ladaModel)
           : base(releaseDate, weight, exploitation, distanceTraveled, engine, carType, numberOfSeats, color, fuelConsumption)
        {
            this.ladaModel = ladaModel;
        }
        public void ShowCarDashboard()
        {
            Console.WriteLine($"Текущая скорость:{vehicleComponents.engine.GetCurrentSpeed()}" +
                 $"\nОстаток топлива: {vehicleComponents.engine.engineTub.TubFuelRemaining}" +
                 $"\nПолный объём бака: {vehicleComponents.engine.engineTub.TubFuelCapacity}");
        }

        public string OpenWindow(int num)
        {
            return "Мощно так покрутил ручку";
        }

        public string PlayMusic(string nameOfSong)
        {
            return "Тык";
        }

        public string UseWipers()
        {
            return $"Дворники начали работать";
        }
        public override bool CarTravel(double speed)
        {
            if (rand.Next(0, 100) > 90)
                Thread.Sleep(200);
            return base.CarTravel(speed);
        }
        public override string ToString()
        {
            return $"Модель: {ladaModel}" +
                   getCarInfo();
        }

        public void Beep()
        {
            Console.Beep(500, 500);
        }
    }
}
