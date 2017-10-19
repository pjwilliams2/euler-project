using System;
using System.IO;

namespace Euler102
{
    class Program
    {
        static void Main(string[] args)
        {
            var rdr = new StreamReader("p102_triangles.txt");
            var count = 0;

            while(!rdr.EndOfStream)
            {
                var coords = ParseTriangleCoords(rdr.ReadLine());
                double[] pt_a = coords[0], pt_b = coords[1], pt_c = coords[2];

                //make sure the x coordinate of the angle bisector and the opposite line segment are between the x's of the line segment
                // A bisect BC x coord
                // Solving for: Ax / Ay * X = (Cy - By) / (Cx - Bx) * (X - Bx) + By
                double x1 = ((pt_b[1] - pt_c[1]) / (pt_c[0] - pt_b[0]) * pt_b[0] + pt_b[1]) / (pt_a[1] / pt_a[0] + (pt_c[1] - pt_b[1]) / (pt_b[0] - pt_c[0]));
                // B bisect AC x coord
                double x2 = ((pt_a[1] - pt_c[1]) / (pt_c[0] - pt_a[0]) * pt_a[0] + pt_a[1]) / (pt_b[1] / pt_b[0] + (pt_c[1] - pt_a[1]) / (pt_a[0] - pt_c[0]));
                // C bisect AB x coord
                double x3 = ((pt_a[1] - pt_b[1]) / (pt_b[0] - pt_a[0]) * pt_a[0] + pt_a[1]) / (pt_c[1] / pt_c[0] + (pt_b[1] - pt_a[1]) / (pt_a[0] - pt_b[0]));

                if((x1 < pt_b[0] && x1 < pt_c[0]) || (x1 > pt_b[0] && x1 > pt_c[0]) ||
                   (x2 < pt_a[0] && x2 < pt_c[0]) || (x2 > pt_a[0] && x2 > pt_c[0]) ||
                   (x3 < pt_a[0] && x3 < pt_b[0]) || (x3 > pt_a[0] && x3 > pt_b[0]))
                {
                    continue;
                }

                count++;
            }

            Console.WriteLine("Number of triangles with origin: " + count);
        }

        static double[][] ParseTriangleCoords(string str)
        {
            //input should be in the format: x1,y1,x2,y2,x3,y3
            double[][] arr = new double[3][];

            var strs = str.Split(',', StringSplitOptions.None);

            for(int i = 0; i < 3; i++)
                arr[i] = new double[2]{ Int32.Parse(strs[2*i]), Int32.Parse(strs[2*i+1]) };

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
