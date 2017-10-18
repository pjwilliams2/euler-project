using System;

namespace Euler6
{
    //Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
    class Program
    {
        static void Main(string[] args)
        {
            double sumOfSquares = 0;
            double squareOfSums = 0;
            for(int i = 1; i <= 100; i++)
            {
                sumOfSquares += i * i;
                squareOfSums += i;
            }

            //square the sums
            squareOfSums *= squareOfSums;

            Console.WriteLine("{0} - {1} = {2}", squareOfSums, sumOfSquares, squareOfSums - sumOfSquares);
        }
    }
}
