using System;
using System.IO;

namespace Euler102
{
    class Program
    {
        static void Main(string[] args)
        {
            var rdr = new StreamReader("p102_triangles.txt");
            
            TestParseTriangleCoords();



            Console.WriteLine("Hello World!");
        }

        static int[][] ParseTriangleCoords(string str)
        {
            //input should be in the format: x1,y1,x2,y2,x3,y3
            int[][] arr = new int[3][];

            var strs = str.Split(',', StringSplitOptions.None);

            for(int i = 0; i < 3; i++)
                arr[i] = new int[2]{ Int32.Parse(strs[2*i]), Int32.Parse(strs[2*i+1]) };

            return arr;
        }

        static void TestParseTriangleCoords()
        {
            var str = "4,3,-9,0,6,-1";
            var arr = ParseTriangleCoords(str);

            foreach(var pair in arr)
                Console.WriteLine("x: {0}, y: {1}", pair[0], pair[1]);
        }
    }
}
