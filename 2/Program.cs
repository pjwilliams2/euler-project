using System;
using System.IO;

namespace Euler2
{
    class Program
    {
        static void Main(string[] args)
        {
            var fib1 = 0;
            var fib2 = 1;
            var fibNext = 0;
            var max_fib = 4000000; //4M
            var sum = 0;

            while((fib1 < max_fib) && (fib2 < max_fib))
            {
                fibNext = fib1 + fib2;
                
                if(fibNext % 2 == 0)
                    sum += fibNext;

                fib1 = fib2;
                fib2 = fibNext;
            }

            Console.WriteLine("Sum of even fibs: " + sum);
        }
    }
}
