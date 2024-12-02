using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Day_2
{
    internal class Program
    {
        static bool ifSolvable(List<int> report)
        {

            for (int i = 0; i < report.Count(); i++)
            {
                List<int> temp = new List<int>();
                temp.AddRange(report);
                temp.RemoveAt(i);
                if (isSafe(temp)) return true;
            }
            return false;
        }

        static bool IncOrDec(List<int> report)
        {
            for (int i = 0; i < report.Count - 1; i++)
            {
                if (report[1] - report[0] < 0)
                {
                    if (!(report[i + 1] - report[i] < 0))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!(report[i + 1] - report[i] > 0))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static bool JumpsOkay(List<int> report)
        {
            for(int i=0;i<report.Count - 1;i++)
            {
                int jump = report[i + 1] - report[i];
                if(!(Math.Abs(jump)>0 && Math.Abs(jump) < 4))
                {
                    return false;
                }
            }

            return true;
        }

        static bool isSafe(List<int> report)
        {
            return (JumpsOkay(report) && IncOrDec(report));
        }

        static void Main(string[] args)
        {
            int counter = 0;
            List<int> report = new List<int>();
            string currentInt = "";

            string path = "day2.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    for(int i=0;i<line.Length; i++)
                    {
                        if (line[i]!=' ')
                        {
                            currentInt += line[i];
                        }
                        if (line[i]==' ' || i==line.Length-1)
                        {
                            report.Add(int.Parse(currentInt));
                            currentInt = "";
                        }
                    }

                    if (isSafe(report))
                    {
                        counter++;
                    }
                    else if (ifSolvable(report))
                    {
                        counter++;
                    }
                    report.Clear();
                }
            }

            Console.WriteLine(counter);
            Console.ReadKey();
        }       
    }
}
