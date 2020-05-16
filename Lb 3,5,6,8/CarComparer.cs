using System.Collections;
using System.Collections.Generic;

namespace LB3_C_SHARP
{
    static public class Helper
    {
        public static string ConvertToString(this IEnumerable collection)
        {
            var res = "";
            foreach (var item in collection)
                res += item;
            return res;
        }
    }

    class CarComparer : IComparer<Car>
    {
        public int Compare(Car car1, Car car2)
        {
            if (car1.GetEngine() == car2.GetEngine())
                return 0;
            else if (car1.GetEngine() > car2.GetEngine())
                return 1;
            else
                return 0;
        }
    }
}
