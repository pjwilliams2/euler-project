using System;
using System.Collections.Generic;

namespace Euler
{
    public static class Prime
    {
        static readonly HashSet<double> primes = new HashSet<double>();
        static readonly HashSet<double> nonprimes = new HashSet<double>();

        public static void PreloadPrimes(double index)
        {
            for(double i = 1, num = 2; i <= index; num++)
                if(IsPrime(num))
                {
                    i++;
                    primes.Add(num);
                }
                else
                {
                    nonprimes.Add(num);
                }
        }

        public static bool IsPrime(double number)
        {
            //check cache for number
            if(primes.Contains(number)) return true;
            if(nonprimes.Contains(number)) return false;

            var result = true;
            for(long i = 2; i <= Math.Sqrt(number); i++)
                if(number % i == 0)
                {
                    result = false;
                    break;
                }
            
            //store the number in cache
            if(result) primes.Add(number);
            else       nonprimes.Add(number);

            return result;
        }

        public static double[] GetPrimeFactors(double number)
        {
            var primeFactors = new List<double>();
            var done = false;
            var intermediate = number;

            while(!done)
            {
                for(double i = 2; i <= intermediate; i++)
                {
                    if(IsPrime(i) && (intermediate % i == 0))
                    {
                        done = i == intermediate;
                        intermediate /= i;
                        primeFactors.Add(i);
                        break;
                    }
                }
            }

            return primeFactors.ToArray();
        }
    }
}
