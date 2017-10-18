using System;

namespace Euler
{
    public static class MathExt
    {
        public static bool IsPrime(double number)
        {
            for(long i = 2; i <= Math.Sqrt(number); i++)
                if(number % i == 0)
                    return false;
            
            return true;
        }
    }
}
