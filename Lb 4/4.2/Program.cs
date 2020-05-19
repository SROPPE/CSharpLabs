
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace Lb_4._2_CSHarp
{
    class Program
    {
        [DllImport("Mathdll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void FibonacciInit(int a, int b);
        [DllImport("Mathdll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FibonacciNext();
        [DllImport("Mathdll.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int FibonacciCurrent();
        [DllImport("Mathdll.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int FibonacciIndex();

        static void Main(string[] args)
        {
            FibonacciInit(1, 1);
            do
            {
                Console.WriteLine(FibonacciIndex()+": " + FibonacciCurrent());
         
            } while (FibonacciNext() !=0);
            Console.ReadLine();
        }
       
    }
}
