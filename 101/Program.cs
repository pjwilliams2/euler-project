using System;

namespace Euler101
{
    //https://projecteuler.net/problem=101
    //un = 1 − n + n2 − n3 + n4 − n5 + n6 − n7 + n8 − n9 + n10

    struct Point
    {
        public long x, y;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var coeffs = new int[]{1, -1, 1, -1, 1, -1, 1, -1, 1, -1, 1};
            var generated = new Point[coeffs.Length];

            //generate points with generator func
            for(int i = 0; i < coeffs.Length; i++)
                generated[i] = new Point(){ x = i+1, y = Evaluate(i+1, coeffs)};

            // foreach(var pt in generated)
            //     Console.WriteLine("Generated: " + pt.y);

            //EvaluateTest();
            //InterpolateValueTest();

            //progressively evaluate more points and sum up fits
            Point[] tempPts = null;
            Point tempPt;
            var fits = 0.0;
            for(int i = 1; i < generated.Length; i++)
            {
                tempPt = generated[i];
                tempPts = new Point[i];
                Array.Copy(generated, tempPts, i);
                var value = InterpolateValue(tempPt.x, tempPts);
                var expected = tempPt.y;

                fits += value;
            }
            
            Console.WriteLine("Sum of fits: {0}", fits);
        }

        static double InterpolateValue(double input, Point[] points)
        {
            var output = 0.0;
            var temp = 0.0;

            for(int i = 0; i < points.Length; i++)
            {
                temp = points[i].y;
                for(int j = 0; j < points.Length; j++)
                {
                    if(i != j)
                        temp *= (input - points[j].x) / (points[i].x - points[j].x);
                }
                output += temp;
            }

            return output;
        }

        static void InterpolateValueTest()
        {
            //points for x^2 - 2x + 1
            var test = new Point[3];
            test[0] = new Point(){x = 1, y = 0};
            test[1] = new Point(){x = 2, y = 1};
            test[2] = new Point(){x = -1, y = 4};
            
            //should output 9
            var a = InterpolateValue(4, test); 
            Console.WriteLine("Output: " + a);
        }

        static long Evaluate(int input, int[] coeffs, int degree = -1)
        {
            var output = 0L;

            if(degree < 0) degree = coeffs.Length - 1;

            for(int i = 0; i <= degree && i < coeffs.Length; i++)
            {
                output += (long)(coeffs[i] * Math.Pow(input, i));
            }

            return output;
        }

        static void EvaluateTest()
        {
            //x^2 - 1
            var output = Evaluate(3, new int[]{-1, 0, 1}, 2);
            Console.WriteLine("x^2 - 1 of 3 = {0} : Expected {1}", output, 8);
            
            output = Evaluate(5, new int[]{-1, 0, 1}, 2);
            Console.WriteLine("x^2 - 1 of 5 = {0} : Expected {1}", output, 24);

            //x^3 + 5x^2 - x + 2
            output = Evaluate(2, new int[]{2, -1, 5, 1});
            Console.WriteLine("x^3 + 5x^2 - x + 2 of 2 = {0} : Expected {1}", output, 28);

            output = Evaluate(5, new int[]{2, -1, 5, 1});
            Console.WriteLine("x^3 + 5x^2 - x + 2 of 5 = {0} : Expected {1}", output, 247);

            //x^3 + 5x^2 - x + 2 up to 2nd degree
            output = Evaluate(2, new int[]{2, -1, 5, 1}, 2);
            Console.WriteLine("x^3 + 5x^2 - x + 2 of 2 (2nd degree) = {0} : Expected {1}", output, 20);

            output = Evaluate(5, new int[]{2, -1, 5, 1}, 2);
            Console.WriteLine("x^3 + 5x^2 - x + 2 of 5 (2nd degree) = {0} : Expected {1}", output, 122);
        }
    }
}
