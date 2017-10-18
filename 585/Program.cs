using System;
using System.Collections.Generic;

namespace Euler585
{
    class Program
    {
        readonly HashSet<double> primes = new HashSet<double>();
        readonly HashSet<double> nonprimes = new HashSet<double>();

        static void Main(string[] args)
        {
            int max = 100;
            int count = 0;

            for(double x = 2; x <= max; x++)
            {
                var half_x_squared = x*x/2;

                for(double y = 2; y <= half_x_squared; y++)
                {
                    //skip instances of perfect squares
                    if(Math.Sqrt(y) == (int)Math.Sqrt(y))
                        continue;

                    // TODO add limiting condition to z when y is a large number
                    for(double z = half_x_squared - y + 2; z <= half_x_squared; z++)
                    {
                        //skip instances of perfect squares
                        if(Math.Sqrt(z) == (int)Math.Sqrt(z))
                            continue;

                        //Console.WriteLine("x: {0}, y: {1}, z: {2}", x, y, z);
                        if(++count % 1_000 == 0)
                            Console.WriteLine("Count: " + count);
                        //count++;
                    }
                }
            }

            Console.WriteLine("Count: " + count);
        }

        double[] GetPrimeFactors(double number)
        {
            var factors = new List<double>();



            return factors.ToArray();
        }


    }
}
