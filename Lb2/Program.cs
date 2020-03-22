using System;
using System.Collections.Generic;
using System.Text;

namespace LB2_C_SHARP
{
    class Program
    {
        static void Main(string[] args)
        {
            char [] str = new char [257];
            List<DateTime> dateTime = new List<DateTime>();
            List<string> month = new List<string>();
            Random random = new Random();

 //30 Рандомных символов
            for (int i = 0; i < 257; i++)
            {
                str[i] = (char)random.Next(97,123);
            }
            for (int i = 0; i < 31; i++)
            {
                Console.Write($"{str[random.Next(0,257)]} ");
            }
            Console.WriteLine();

//Месяца на французском
            for (int i = 1; i <= 12; i++)
            {
                dateTime.Add(new DateTime(1, i, 1));
            }
            for (int i = 0; i < dateTime.Count; i++)
            {
            month.Add(dateTime[i].ToString("F", new System.Globalization.CultureInfo("fr-FR"))
                .Substring(dateTime[i].ToString("F", new System.Globalization.CultureInfo("fr-FR"))
                .IndexOf("1")+2, dateTime[i].ToString("F", new System.Globalization.CultureInfo("fr-FR")).IndexOf('0')));
            month[i] = month[i].Substring(0,month[i].IndexOf('0'));
            Console.WriteLine(month[i]);
            }

//Обратный порядок слов
            Console.WriteLine("Введите строку: ");
            string a = Console.ReadLine().Trim();
            StringBuilder stringBuilder = new StringBuilder();
            while (true)
            {
                if (a.Contains(" "))
                {
                    stringBuilder.Append(a.Substring(a.LastIndexOf(' ')+1)+' ');
                    a = a.Remove(a.LastIndexOf(' '));
                }
                else
                {
                    stringBuilder.Append(a);
                    break;
                }
            }    
                Console.WriteLine(stringBuilder);
            Console.ReadLine();
        }
    }
}
