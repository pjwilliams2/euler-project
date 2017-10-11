using System;
using System.Collections.Generic;

namespace Euler5
{
    //What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
    class Program
    {
        static void Main(string[] args)
        {
            var max = 20;
            var nums = new int[max];
            
            //build out the list of numbers
            for(int i = 1; i <= max; i++)
                nums[i-1] = i;

            nums = RemoveCommonFactors(nums);

            var smallestMultiple = 1;
            foreach(int num in nums)
                smallestMultiple *= num;


            Console.WriteLine("Smallest multiple: " + smallestMultiple);
        }

        static int[] RemoveCommonFactors(int[] nums)
        {
            //remove common factors by dividing by smaller numbers
            var temp = new int[nums.Length];
            Array.Copy(nums, temp, nums.Length);

            for(int i = 0; i < temp.Length; i++)
                for(int j = i + 1; j < temp.Length && temp[i] != 1; j++)
                {
                    if(temp[j] % temp[i] == 0) temp[j] /= temp[i];
                }

            return temp;
        }
    }
}
