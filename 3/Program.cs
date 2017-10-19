using System;
using System.IO;
using System.Collections.Generic;

using Euler;

namespace Euler3
{
    class Program
    {
        static void Main(string[] args)
        {
            const long value = 600851475143;
            var primeFactors = new List<long>();
            var done = false;
            var intermediate = value;

            while(!done)
            {
                for(long i = 2; i <= intermediate; i++)
                {
                    if(Euler.MathExt.IsPrime(i) && (intermediate % i == 0))
                    {
                        done = i == intermediate;
                        intermediate /= i;
                        primeFactors.Add(i);
                        break;
                    }
                }
            }

            foreach(var num in primeFactors)
                Console.WriteLine("Prime factor: " + num);
        }
    }
}
