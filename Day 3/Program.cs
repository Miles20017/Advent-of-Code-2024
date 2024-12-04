using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Day_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> toMultiply = new List<int>();
            List<string> Possible = new List<string>();
            
            string pattern = @"mul\(\d{1,3},\d{1,3}\)";
            Regex regex = new Regex(pattern);

            string Enabler = @"do\(\)|don't\(\)"; 
            Regex regex2 = new Regex(Enabler);

            bool Enabled = true;

            string path = "day3.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string input;
                input = reader.ReadToEnd();

                MatchCollection mul = regex.Matches(input);
                MatchCollection DoDontMatch = regex2.Matches(input);

                for(int i = 0; i < input.Length; i++)
                {
                    foreach(Match match in DoDontMatch)
                    {
                        if (i == match.Index)
                        {
                            if (match.Value == "don't()")
                            {
                                Enabled = false;
                            }
                            else
                            {
                                Enabled = true;
                            }
                        }
                    }

                    foreach(Match match in mul)
                    {
                        if (Enabled)
                        {
                            if (i == match.Index)
                            {
                                Possible.Add(match.Value);
                            }
                        }
                    }
                }
            }

            foreach(string command in Possible)
            {
                string ToConvert = "";
                for(int i = 4; i < command.Length; i++)
                {
                    while (command[i] != ',')
                    {
                        ToConvert+= command[i];
                        i++;
                    }
                    i++;
                    toMultiply.Add(int.Parse(ToConvert));
                    ToConvert = "";
                    while (command[i] != ')')
                    {
                        ToConvert += command[i];
                        i++;
                    }
                    toMultiply.Add(int.Parse(ToConvert));
                }
            }

            int sum = 0;
            for(int i = 0; i < toMultiply.Count()-1; i+=2)
            {
                sum+=(toMultiply[i] * toMultiply[i + 1]);
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }
    }
}
