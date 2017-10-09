using System;
using System.Collections.Generic;

namespace Euler4
{
    //Find the largest palindrome made from the product of two 3-digit numbers.
    class Program
    {
        static void Main(string[] args)
        {
            //PalindromeTest();
            var largest = 0;
            for(int i = 999; i >= 100; i--)
            {
                for(int j = i; j >= 100; j--)
                {
                    if(IsPalindrome(i * j))
                    {
                        Console.WriteLine("Found palindrome: {0} * {1} = {2}", i, j, (i * j));
                        if(largest < (i*j)) 
                            largest = i * j;
                    }
                }
            }

            Console.WriteLine("Largest: " + largest);
        }

        static void PalindromeTest()
        {
            var testNums = new List<int>();
            testNums.Add(9009);
            testNums.Add(9445);
            testNums.Add(1234321);

            foreach(int num in testNums)
            {
                Console.WriteLine(String.Format("isPal? {0} - {1}", num, IsPalindrome(num)));
            }
        }

        static bool IsPalindrome(int number)
        {
            var str = number.ToString();
            for(int i = 0, j = str.Length - 1; i <= j; i++, j--)
            {
                if(str[i] != str[j])
                    return false;
            }

            return true;
        }
    }
}
