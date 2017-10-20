using System;

namespace Euler7
{
    class Program
    {
        static void Main(string[] args)
        {
            var maxOrdinalIndex = 10_001;
            var ordinalIndex = 0;
            var primeNum = 0D;

            for(double num = 2; ordinalIndex < maxOrdinalIndex; num++)
                if(Euler.Prime.IsPrime(num))
                {
                    ordinalIndex++;
                    primeNum = num;
                }

            Console.WriteLine("The {0} prime is: {1}", ordinalIndex, primeNum);
        }
    }
}
