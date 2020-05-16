using System;
using System.Linq;

namespace LB3_C_SHARP
{
    public class Tub
    {
        public Action TubRefilling = () => Console.ForegroundColor = ConsoleColor.Red;
        public Action TubFuelSpending = () => Console.ForegroundColor = ConsoleColor.Red;
        public double TubFuelRemaining { get; private set; }
        public double TubFuelCapacity { get; private set; }
        static Random rand = new Random(Guid.NewGuid().ToByteArray().Sum(x => x));
        public Tub(string TubFuelCapacity)
        {
            if (double.TryParse(TubFuelCapacity, out double result))
            {
                this.TubFuelCapacity = result;
            }
            else
            {
                throw new ArgumentException(nameof(TubFuelCapacity), "Incorrect data entered");
            }
           TubFuelRemaining = rand.Next(0, (int)this.TubFuelCapacity);
        }

        public double TubRefill(double RefillVolume)
        {
            TubRefilling?.Invoke();
            if (TubFuelRemaining + RefillVolume >= TubFuelCapacity)
            {
                TubFuelRemaining = TubFuelCapacity;
            }
            else
            {
                TubFuelRemaining += RefillVolume;
            }
            return TubFuelRemaining;
        }
        public void TubFuelSpend(double fuelSpend)
        {
            TubFuelSpending?.Invoke();
            if (TubFuelRemaining - fuelSpend >= 0)
            {
                TubFuelRemaining -= fuelSpend;
            }
            else
                TubFuelRemaining = 0;
            
        }
        public override string ToString()
        {
            return $"Остаток топлива в баке: {TubFuelRemaining}" +
                $"\nОбъём бака: {TubFuelCapacity}";
        }
    }
}
