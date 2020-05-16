using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB3_C_SHARP
{
   
    public class Car : Vehicle, IComparer<Car>
    {
  
        protected string carType;
        protected string carColor;
        protected double fuelConsumption;
        protected int numberOfSeats;
        private int carId;
        static Random rand = new Random(3);
        static List<int> inidentificationNumberList = new List<int>();
       
        
        public Car(int releaseDate, int weight, int exploitation, double distanceTraveled,Engine engine,
             string carType, int numberOfSeats, string color, int fuelConsumption) 
            : base(releaseDate,weight,exploitation,distanceTraveled,engine)
        {     
            SetCarType(carType);
            SetColor(color);
            SetFuelConsumption(fuelConsumption);
            SetnumberOfseats(numberOfSeats);
            int k = rand.Next();
            while (inidentificationNumberList.Contains(k))
                k = rand.Next();          
            carId = rand.Next();    
        }
        
        private void SetCarType(string carType)
        {

            if (string.IsNullOrWhiteSpace(carType))
            {
                throw new ArgumentNullException(nameof(carType), "Cannot be empty");
            }
            this.carType = carType;
        }
        public string GetCarType()
        {
            return this.carType;
        }
        private void SetColor(string color)
        {

            if (string.IsNullOrWhiteSpace(color))
            {
                throw new ArgumentNullException(nameof(color), "Cannot be empty");
            }
            this.carColor = color;
        }
        public virtual bool CarTravel(double speed)
        {
            if (travel.Invoke(speed, fuelConsumption))
                return true;
            else return false;
        }
        public string GetColor()
        {
            return this.carColor;
        }
        private void SetnumberOfseats(int numberOfSeats)
        {
            if (numberOfSeats <= 0)
                {
                    throw new ArgumentException(nameof(numberOfSeats), "Cannot be negative");
                }
            this.numberOfSeats = numberOfSeats;
        }

        public int GetNumberOfSeats()
        {
            return this.numberOfSeats;
        }
        private void SetFuelConsumption(int fuelConsumption)
        {
            if (fuelConsumption <= 0)
            {
                throw new ArgumentException(nameof(fuelConsumption), "Cannot be negative");
            }
            this.fuelConsumption = fuelConsumption;
        }
        protected string getCarInfo()
        {
            return $"\nДата выпуска: {releaseDate}" +
                $"\nId: {vehicleCurrentNumber}"+
                $"\nВремя эксплуатации: {exploitationTime} года" +
                $"\nВес: {weight}" +
                $"\n{ vehicleComponents.engine}" +
                $"\nПройденная дистанция: {distanceTraveled}" +
                $"\nКласс: {carType}" +
                $"\nКоличество пассажиров: {numberOfSeats}" +
                $"\nЦвет: {carColor}" +
                $"\nЗатрата батареи: {fuelConsumption}" +
                $"\nБак: \n\n{vehicleComponents.tub}\n";
               
        }
        public double GetFuelConsumption()
        {
            return this.fuelConsumption;
        }
      
        public override string ToString()
        {
            return
                $"Тип: {carType}" +
                $"\nЦвет: {carColor}" +
                $"\nКоличество колёс: {numberOfSeats}" +
                $"\nРасход топлива на 100км: {fuelConsumption}" +
                $"\nУникальный номер: {carId}";
        }
        public int Compare(Car car1, Car car2)
        {
            if (car1.vehicleComponents.engine == car2.vehicleComponents.engine)
                return 0;
            else if (car1.vehicleComponents.engine > car2.vehicleComponents.engine)
                return 1;
            else
                return 0;
        }

    }
}
