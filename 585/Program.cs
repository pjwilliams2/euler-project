using System;
using System.Collections.Generic;
using System.Linq;

namespace Euler585
{
    class Program
    {
        static void Main(string[] args)
        {
            F(20);
            //TestSimplifyRadical();
            //TestGetCompositeFactors();
        }

        static HashSet<string> successes = new HashSet<string>();
        public static void F(int max_x)
        {
            int iterations = 0;
            var attempts = new Dictionary<string, bool>();

            //values will be stored in a matrix in the following manner
            //     0
            //   --------
            // 0 | x |
            // 1 | y |
            // 2 | z |

            // followed by a simplification process
            //     0                 0   1   2   3
            //   --------          ---------------
            // 0 | 8  |     ==>  0 | 8 |   |   |
            // 1 | 15 |          1 | 2 | 3 | 5 |
            // 2 | 15 |          2 |

            //preload some primes
            Euler.Prime.PreloadPrimes(10_000);
            //max_x = 97;
            for(double x = 3; x <= max_x; x++)
            {
                //radicand[0][0] = x;
                var half_x_squared = Math.Round(x*x);

                for(double y = 2; y <= half_x_squared; y++)
                {
                    //skip instances of perfect squares
                    if(Math.Sqrt(y) == (int)Math.Sqrt(y))
                        continue;

                    // TODO add limiting condition to z when y is a large number
                    for(double z = 2; z <= half_x_squared; z++)
                    {
                        //skip instances of perfect squares
                        if(Math.Sqrt(z) == (int)Math.Sqrt(z))
                            continue;

                        //skip instances where we have already attempted the combination
                        var key1 = String.Format("{0}:{1}|{2}", x, y, z);
                        var key2 = String.Format("{0}:{1}|{2}", x, z, y);
                        if(attempts.ContainsKey(key1) || attempts.ContainsKey(key2))
                            continue;
                        else
                        {
                            attempts.Add(key1, true);
                            if(y != z) attempts.Add(key2, true);
                            iterations++;
                            DoesCombinationWork(x, y, z);
                        }
                    }
                }
            }

            Console.WriteLine("Count: " + successes.Count);
            Console.WriteLine("Total Iterations: " + iterations);
        }

        static int DoesCombinationWork(double x, double y, double z)
        {
            int count = 0;
            int iterations = 0;
            double[][] radicand = new double[3][];
            radicand[0] = new double[1]{ x };

            radicand[1] = SimplifyRadical(y);
            radicand[2] = SimplifyRadical(z);
            var combined = CombineSimplifiedRadicals(radicand[1], radicand[2]);

            //where the two radicals able to be combined
            var isCombined = combined[1][0] == 0;

            if(isCombined)
            {
                Array.Copy(combined[0], radicand[1], combined[0].Length);
                Array.Copy(combined[1], radicand[2], combined[1].Length);
            }
            
            //coefficient must be even
            if(radicand[1][0] % 2 != 0 || radicand[2][0] % 2 != 0) 
                return 0;
            

            //if combined, the factors should add up to x
            //and the middle product should solve 2AB = radicand[1][0] * radicand[1][1..Length-1]
            if(isCombined)
            {
                double a = 0;
                double b = 0;
                double multiplier = 1;

                if(radicand[1].Length > 3) return 0;
                else if(radicand[1][0] > 2)
                {
                    multiplier = radicand[1][0] * radicand[1][0] / 4;
                    //a = radicand[1][0] * radicand[1][0] / 4;
                    //b =  radicand[1][1];
                }
                
                if(radicand[1].Length == 2)
                {
                    a = multiplier;
                    b = radicand[1][1];
                }
                else
                {
                    a = multiplier * radicand[1][1];
                    b = radicand[1][2];

                    if(x != (a+b))
                    {
                        a /= multiplier;
                        b *= multiplier;
                    }

                    if(x != (a+b))
                    {
                        a = multiplier;
                        b = radicand[1][1] * radicand[1][2];
                    }
                }

                if(x == (a+b))
                {
                    count++;
                    Console.WriteLine("x: {0} y: {1} z: {2} = Rad({3}) + Rad({4})", x, y, z, a, b);
                    successes.Add(String.Format("{0}:{1}|{2}|", x, a, b));
                    //Console.WriteLine("x: {0} y: {1} z: {2} = -Rad({3}) + -Rad({4})", x, y, z, a, b);
                    //successes.Add(String.Format("{0}:{1}|{2}|", x, -a, -b));
                }
            }
            else
            {
                var composite = GetCompositeFactors(radicand[1], radicand[2]);

                //all of the composites must sum up to be x
                var sum = composite.Sum();
                if(sum != x) return 0;

                var signArrays = CreateSignArrays(composite.Length);
                var rad1_divisor = CreateRadicandDivisor(radicand[1]);
                var rad2_divisor = CreateRadicandDivisor(radicand[2]);
                //var products = new double[composite.Length * (composite.Length - 1) / 2];
                var products1 = new double[composite.Length, composite.Length];
                var products2 = new double[composite.Length, composite.Length];
                for(int i = 0; i < composite.Length; i++)
                    for(int j = 0; j < composite.Length; j++)
                    {
                        if(i == j) continue;
                        products1[i, j] = 2 * Math.Sqrt(composite[i] * composite[j] / rad1_divisor);
                        products2[i, j] = 2 * Math.Sqrt(composite[i] * composite[j] / rad2_divisor);
                    }
                
                bool comp_1_works = false, comp_2_works = false, works = false;
                int[] signs = null;
                for(int s = 0; s < signArrays.Length; s++)
                {    
                    signs = signArrays[s];
                    comp_1_works = false; comp_2_works = false; works = false;
                    for(int i = 0; i < composite.Length && !works; i++)
                        for(int j = composite.Length - 1; j >= 0 && i != j && !works; j--)
                            //if(i == j) break;
                            //else
                            for(int k = composite.Length - 1; k >= 0 && !works; k--)
                                if(i == k) continue;
                                else
                                for(int l = 0; l < composite.Length && k != l && !works; l++)
                                {
                                    if(j == l) continue;
                                    //if(k == l) break;
                                    comp_1_works = comp_1_works || (signs[i] * signs[j] * products1[i, j] + signs[l] * signs[k] * products1[k, l]) == radicand[1][0];
                                    comp_2_works = comp_2_works || (signs[i] * signs[j] * products2[i, j] + signs[l] * signs[k] * products2[k, l]) == radicand[2][0];
                                    works = comp_1_works && comp_2_works;
                                }
                    
                    if(works)
                    {
                        count++;
                        Console.Write("x: {0} y: {1} z: {2} = ", x, y, z);
                        var str = String.Format("{0}:", x);
                        for(int i = 0; i < composite.Length; i++)
                        {
                            Console.Write(signs[i] == -1 ? "-" : "");
                            Console.Write("Rad({0}) + ", composite[i]);
                            str = str + String.Format("{0}|", signs[i] * composite[i]);
                        }
                        Console.WriteLine();
                        successes.Add(str);
                    }
                }
            }

            //Console.WriteLine("x: {0}, y: {1}, z: {2}", x, y, z);
            if(++iterations % 10_000 == 0)
                Console.WriteLine("Iteration: " + iterations);

            return count;
        }

        static double[][] CombineSimplifiedRadicals(double[] a, double[] b)
        {
            var combined = new double[2][];
            combined[0] = new double[a.Length]; Array.Copy(a, combined[0], a.Length);
            combined[1] = new double[b.Length]; Array.Copy(b, combined[1], b.Length);

            if(a.Length == b.Length)
            {
                var combinable = true;
                for(int i = 1; i < a.Length && combinable; i++)
                    if(a[i] != b[i])
                        combinable = false;
                
                if(combinable)
                {
                    combined[0][0] += combined[1][0]; 
                    combined[1] = new double[]{ 0, 1 };
                }
            }

            return combined;
        }

        static Dictionary<int, int[][]> signArrayCache = new Dictionary<int, int[][]>();
        static int[][] CreateSignArrays(int length)
        {
            if(signArrayCache.ContainsKey(length))
                return signArrayCache[length];

            int[][] array = new int[(int)Math.Pow(2, length)][];
            for(int i = 0; i < array.Length; i++)
            {
                array[i] = new int[length];
                for(int j = 0; j < length; j++)
                {
                    array[i][j] = (int)Math.Pow(-1, i / (int)Math.Pow(2, j) % 2);
                }
            }
            
            signArrayCache[length] = array;

            return array;
        }

        //radical is given in the form [4,2,5] where 4 is the coefficient
        //should return 10
        static double CreateRadicandDivisor(double[] radical)
        {
            var div = 1D;
            for(int i = 1; i < radical.Length; i++)
                div *= radical[i];

            return div;
        }

        public static double[] GetCompositeFactors(double[] a, double[] b)
        {
            var factors = new List<double>();
            //var aList = new List<double>();
            //var bList = new List<double>();


            for(int i = 1; i < a.Length; i++)
                for(int j = 1; j < b.Length; j++)
                {
                    factors.Add(a[i] * b[j]);
                    if(a.Length > b.Length || b.Length == 2) factors.Add(a[i]);
                    else if(b.Length > a.Length || a.Length == 2) factors.Add(b[j]);
                }

            // foreach(var aItem in aList)
            //     foreach(var bItem in bList)
            //         factors.Add(aItem * bItem);

            factors.Sort();

            return factors.Distinct().ToArray();
        }

        //given a radical, the method returns the simplified version
        //for example, given 160, the method will return [4, 2, 5] where 4 is the coefficient
        static Dictionary<double, double[]> simpliedRadicals = new Dictionary<double, double[]>();
        static double[] SimplifyRadical(double radicand)
        {
            double[] tmp;
            double[] cached;
            if(simpliedRadicals.ContainsKey(radicand))
            {
                cached = simpliedRadicals[radicand];
                tmp = new double[cached.Length];
                Array.Copy(cached, tmp, tmp.Length);
                return tmp;
            }

            var factors = Euler.Prime.GetPrimeFactors(radicand);
            var coeff = 1D;
            var list = new List<double>();

            for(int i = 0; i < factors.Length; i++)
            {
                if(i == factors.Length - 1)
                {
                    list.Add(factors[i]);
                }
                else if(factors[i] == factors[i+1])
                {
                    coeff *= factors[i];
                    factors[i] = factors[i+1] = 1;
                    i++;
                }
                else
                {
                    list.Add(factors[i]);
                }
            }

            tmp = new double[list.Count + 1]; cached = new double[tmp.Length];
            tmp[0] = coeff;
            list.CopyTo(tmp, 1);
            Array.Copy(tmp, cached, cached.Length);
            simpliedRadicals.Add(radicand, cached);
            
            return tmp;
        }

        public static void TestSimplifyRadical()
        {
            //should produce 4 2 5
            var result = SimplifyRadical(160);

            Console.Write("*** 160 = ");
            foreach(var num in result)
                Console.Write("{0} ", num);
            Console.Write("\n");

            //should produce 6 3
            result = SimplifyRadical(108);

            Console.Write("*** 108 = ");
            foreach(var num in result)
                Console.Write("{0} ", num);
            Console.Write("\n");

            //should produce 9
            result = SimplifyRadical(81);

            Console.Write("*** 81 = ");
            foreach(var num in result)
                Console.Write("{0} ", num);
            Console.Write("\n");
        }

        static void TestGetCompositeFactors()
        {
            //should return 5, 13
            var composites = GetCompositeFactors(new double[]{5, 13}, new double[]{ 1 });
            Console.WriteLine("Expecting 5 and 13");
            foreach(var f in composites)
                Console.Write("{0} ", f);
            Console.WriteLine();

            //should return 2, 5, 6, 15
            composites = GetCompositeFactors(new double[]{5, 2}, new double[]{ 1, 3 });
            Console.WriteLine("Expecting 2, 5, 6, 15");
            foreach(var f in composites)
                Console.Write("{0} ", f);
            Console.WriteLine();

            //should return 6, 14, 15, 35
            composites = GetCompositeFactors(new double[]{5, 2}, new double[]{ 7, 3 });
            Console.WriteLine("Expecting 6, 14, 15, 35");
            foreach(var f in composites)
                Console.Write("{0} ", f);
            Console.WriteLine();
        }
    }
}
