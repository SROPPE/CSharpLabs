using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LB3_C_SHARP
{
    class Program
    {

        public struct Cars
        {
            public List<Lada> ladas;
            public List<Tesla> teslas;
            public List<Mercedes> mercedes;
        };
        static public void CreateCar(string model, string company,ref Cars cars)
        {
            #region(Input)
            Console.WriteLine("Дата выхода: ");
            int release_date =int.Parse(Console.ReadLine());
            Console.WriteLine("Вес: ");
            int weight =int.Parse(Console.ReadLine());
            Console.WriteLine("Время эксплуатации: ");
            int exploitation = int.Parse(Console.ReadLine());   
            Console.WriteLine("Пройденная дистанция: ");
            int distance_traveled = int.Parse(Console.ReadLine());
            Console.WriteLine("Тип машины: ");
            string car_type = Console.ReadLine();
            Console.WriteLine("Количество посадочных мест: ");
            int number_of_seats = int.Parse(Console.ReadLine());
            Console.WriteLine("Цвет машины: ");
            string color = Console.ReadLine();
            Console.WriteLine("Затрата топлива на 100км: ");
            int fuel_consumption = int.Parse(Console.ReadLine());
            Console.WriteLine("Название двигателя");
            string engine_name;
            while (true)
            {
                engine_name = Console.ReadLine();
                if (int.TryParse(engine_name, out int res))
                    break;
            }
            Console.WriteLine("Мощность двигателя (л.с.): ");
            int engine_power = int.Parse(Console.ReadLine());
            Console.WriteLine("Тип двигателя: ");
            string energy_source = Console.ReadLine();
            Console.WriteLine("Объём бака: ");
            string tubFuelCapacity;
            while (true)
            {
                tubFuelCapacity = Console.ReadLine();
                if (int.TryParse(tubFuelCapacity, out int res))
                    break;
            }
            #endregion
            #region(Creation)
            Engine engine = new Engine(engine_name,engine_power,energy_source,tubFuelCapacity,LocationFrom.God);
            Car car = new Car(release_date, weight, exploitation, distance_traveled, engine, car_type, number_of_seats, color, fuel_consumption);
            switch (company)
            {   case "Tesla":cars.teslas.Add(new Tesla(release_date, weight, exploitation, distance_traveled, engine, car_type, number_of_seats, color, fuel_consumption, model)); break;
                case "Lada": cars.ladas.Add(new Lada(release_date, weight, exploitation, distance_traveled, engine, car_type, number_of_seats, color, fuel_consumption, model)); break;
                case "Mercedes": cars.mercedes.Add(new Mercedes(release_date, weight, exploitation, distance_traveled, engine, car_type, number_of_seats, color, fuel_consumption, model)); break;
            }
            #endregion
        }
        static void Main(string[] args)
        {
            bool exit = false;

            Cars cars = new Cars();
            cars.ladas = new List<Lada>();
            cars.mercedes = new List<Mercedes>();
            cars.teslas = new List<Tesla>();
            Engine EngineLada = new Engine("0", 900, "Бензиновый", "45",LocationFrom.GPS);
            Engine EngineTesla = new Engine("0", 450, "Электрический", "60",LocationFrom.God);
            Engine EngineMers = new Engine("0", 600, "Бензиновый", "60",LocationFrom.None);
            cars.ladas.Add(new Lada(2000, 1000, 15, 300000, EngineLada, "r", 4, "белый", 15, "7"));
            cars.teslas.Add(new Tesla(2015, 2000, 2, 30000, EngineTesla, "d", 4, "белый", 10, "Model S"));
            cars.mercedes.Add(new Mercedes(2018, 1500, 1, 45065, EngineMers, "Седан", 4, "Чёрный", 12, "С"));
          
            while (!exit)
            {
        
                Console.WriteLine("1)Информация об автомобилях" +
                    "\n2)Добавление новой машины" +
                    "\n3)Сравнение машин" +
                    "\n4)Методы машин"+
                    "\n5)Выход");
                switch (Console.ReadKey(true).KeyChar)
                {
                    #region(Show Info)
                    case '1':   
                        Console.Clear();
                        Console.WriteLine("1)Информацию о всех ТС" +
                            "\n2)Информация об указанном транспортном средстве");
                        switch (Console.ReadKey(true).KeyChar)
                        {
                            case '1': //Информация о всех ТС
                                Console.WriteLine(cars.ladas.ConvertToString());
                                Console.WriteLine(cars.mercedes.ConvertToString());
                                Console.WriteLine(cars.teslas.ConvertToString());
                                break;
                            case '2': //Информация об указанном ТС
                                Console.Clear();
                                Console.WriteLine("Введите уникальный номер автомобиля: ");
                                Console.WriteLine(Vehicle.vehicleList[Console.ReadLine()]);
                                Console.WriteLine();
                                break;
                        }
                        break;
                    #endregion;
                    #region(Add new car)
                    case '2': //добавление новой машины
                        Console.Clear();
            
                        Console.WriteLine("Укажите марку автомобиля:" +
                            "\n1)Tesla" +
                            "\n2)Mercedes" +
                            "\n3)Lada");
                        switch (Console.ReadKey(true).KeyChar)
                        {
                            case '1': //Добавление Tesla
                                Console.Clear();
                                Console.WriteLine("Введите модель Tesla");
                                string model = Console.ReadLine();
                                CreateCar(model,"Tesla",ref cars);
                                break;
                            case '2': //Добавление Mersa
                                Console.Clear();
                                Console.WriteLine("Введите модель Tesla");
                                model = Console.ReadLine();
                                CreateCar(model, "Mercedes", ref cars);
                                break;
                            case '3':
                                Console.Clear();
                                Console.WriteLine("Введите модель Tesla");
                                model = Console.ReadLine();
                                CreateCar(model, "Lada", ref cars);
                                break;
                        }
                        break;
                    #endregion;
                    #region(Comparer)
                    case '3':
                        Console.Clear();
                        Console.WriteLine("1)Сравнение машин Tesla" +
                            "\n2)Сравнение машин Lada" +
                            "\n3)Сравнение машин Mercedes" +
                            "\n4)Сравнение двух выбранных машин");
                        switch (Console.ReadKey(true).KeyChar)
                        {
                            case '1':
                                Console.Clear();
                                cars.teslas.Sort(new CarComparer());
                                Console.WriteLine(cars.teslas.ConvertToString());
                                break;
                            case '2':
                                Console.Clear();
                                cars.ladas.Sort(new CarComparer());
                                Console.WriteLine(cars.ladas.ConvertToString());
                                break;
                            case '3':
                                Console.Clear();
                                cars.mercedes.Sort(new CarComparer());
                                Console.WriteLine(cars.mercedes.ConvertToString());
                                break;
                            case '4':
                                Console.Clear();
                                List<Car> compCar = new List<Car>();
                                Console.WriteLine("Введите номер машины 1: ");
                                compCar.Add((Car)Vehicle.vehicleList[Console.ReadLine()]);
                                Console.WriteLine("Введите номер машины 2: ");
                                compCar.Add((Car)Vehicle.vehicleList[Console.ReadLine()]);
                                compCar.Sort(new CarComparer());
                                Console.WriteLine(compCar.ConvertToString());
                                break;
                        }
            
                        break;
                    #endregion;
                    #region(carMethods)
                    case '4':
                        Console.WriteLine("Уникальный номер автомобиля: ");
                        string chooseCar = Console.ReadLine();
                        var inputCar = (Car)Vehicle.vehicleList[chooseCar];
                        if (inputCar is Tesla)
                        {
                            Tesla TestCar = (Tesla)inputCar;
                      
                            bool ex = false;
                            while (!ex)
                            {
                             
                                Console.WriteLine("1)Открыть окно" +
                          "\n2)Замена батареи" +
                          "\n3)Поездка" +
                          "\n4)Зарядка" +
                          "\n5)Вывод информации о машине" +
                          "\n6)Exit");
                                switch (Console.ReadKey(true).KeyChar)
                                {
                                    case '1':
                                        {
                                            Console.Clear();
                                            Console.WriteLine(TestCar.UseWipers());
                                            Console.WriteLine(TestCar.OpenWindow(2));
                                        break;
                                        }
                                    case '2':
                                        {
                                            Console.Clear();
                                            Console.WriteLine(TestCar.teslaBattery);
                                            TestCar.BatteryChange(new TeslaBattery(TestCar.tesla_model));
                                            Console.WriteLine(TestCar.teslaBattery);
                                        break;
                                        }
                                    case '3':
                                        {        
                                            while (Console.ReadKey(true).KeyChar == 'w' && TestCar.CarTravel(TestCar.GetEngine().GetCurrentSpeed()))
                                            {          
                                              
                                                Console.WriteLine("Машина едет");
                                                TestCar.ShowCarDashboard();
                                            }    
                                            
                                            
                                        break;
                                        }
                                    case '4':
                                        {
                                            while (Console.ReadKey(true).KeyChar == '+')
                                            {
                                                Thread.Sleep(1000);
                                                Console.WriteLine(TestCar.teslaBattery.BatteryCharging(1));
                                            }
                                        break;
                                        }
                                    case '5':
                                        {
                                            Console.WriteLine(TestCar);
                                            break;
                                        }
                                    case '6':
                                        {
                                            ex = true;
                                            break;
                                        }
                                }
                            }
                        }
                        else if(inputCar is Lada)
                        {
                            Lada TestCar = (Lada)inputCar;
                            TestCar.Beep();
                            TestCar.UseWipers();
                            TestCar.ShowCarDashboard();
                            while (Console.ReadKey(true).KeyChar == 'w' && TestCar.CarTravel(TestCar.GetEngine().GetCurrentSpeed()))
                            {
                                Console.WriteLine("Машина едет");
                                TestCar.ShowCarDashboard();
                            }
                       
                        }
                        else if(inputCar is Mercedes)
                        {
                            Mercedes TestCar = (Mercedes)inputCar;
                            TestCar.Beep();
                            TestCar.UseWipers();
                            TestCar.ShowCarDashboard();
                            while (Console.ReadKey(true).KeyChar == 'w' && TestCar.CarTravel(TestCar.GetEngine().GetCurrentSpeed()))
                            {
                                Thread.Sleep(1000);
                                Console.WriteLine("Машина едет");
                                TestCar.ShowCarDashboard();
                            }
                            while (Console.ReadKey(true).KeyChar =='+')
                            {
                                Console.WriteLine(TestCar.GetTub().TubRefill(0.5));
                            }
                            
                            Console.WriteLine(TestCar.GetEngine());
                            Console.WriteLine(TestCar.GetTub());
                        }
                        break;
                    case '5': exit = true; break;
                    #endregion;
                    default: Console.Clear(); break;
                }

            }
                Console.ReadKey();
    
        }
      
    }
  
}
