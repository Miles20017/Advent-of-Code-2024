using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1
{
    internal class Program
    {
        static int Factor(int a, List<int> Right)
        {
            int factor = 0;
            for(int i=0;i< Right.Count(); i++)
            {
                if (Right[i] == a)
                {
                    factor++;
                }
            }
            return factor;
        }


        static void Main(string[] args)
        {

            List<int> Left = new List<int>();
            List<int> Right = new List<int>();

            string path = "day1.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string substring1 = line.Substring(0, 5);
                    Left.Add(int.Parse(substring1.Trim()));
                    string substring2 = line.Substring(8);
                    Right.Add(int.Parse(substring2.Trim()));
                }
            }
            Left.Sort();
            Right.Sort();

            int DistanceTotal = 0;
            for(int i=0;i<Left.Count;i++)
            {
                int toTake = (Left[i]* Factor(Left[i],Right));
                if (toTake < 0)
                {
                    toTake *= -1;
                }
                DistanceTotal += toTake;
            }

            Console.WriteLine(DistanceTotal);
            Console.ReadKey();
        }
    }
}
