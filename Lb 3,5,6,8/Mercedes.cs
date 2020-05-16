using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LB3_C_SHARP
{
    class Mercedes : Car,ICar 
    {
        public string mercedesModel { get; private set; }
        public Mercedes(int releaseDate, int weight, int exploitation, double distanceTraveled, Engine engine,
            string carType, int numberOfSeats, string color, int fuelConsumption, string mercedesModel)
           : base(releaseDate, weight, exploitation, distanceTraveled, engine, carType, numberOfSeats, color, fuelConsumption)
        {
            this.mercedesModel = mercedesModel;
        }
        public void ShowCarDashboard()
        {
            Console.WriteLine($"Текущая скорость: {vehicleComponents.engine.GetCurrentSpeed()}" +
                 $"\nОстаток топлива: {vehicleComponents.engine.engineTub.TubFuelRemaining} " +
                 $"\nПолный объём бака: {vehicleComponents.engine.engineTub.TubFuelCapacity}");
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
            return $"Модель: {mercedesModel}" +
                getCarInfo();
        }

        public void Beep()
        {
            Console.Beep(500, 500);
        }
    }
}
