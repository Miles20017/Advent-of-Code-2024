using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Day_11___Pt_2
{
    internal class Program
    {
        static List<long> ConvertToListLong(string path)
        {
            List<long> list = new List<long>();

            using (StreamReader reader = new StreamReader(path))
            {
                string[] myArray = reader.ReadLine().Split(' ');

                foreach (string str in myArray)
                {
                    list.Add(long.Parse(str));
                }
            }

            return list;
        }

        static bool ChangeToZero(List<long> list, int index)
        {
            if (list[index] == 0)
            {
                list[index] = 1;
                return true;
            }

            return false;
        }

        static Dictionary<string, (long, long)> Splits = new Dictionary<string, (long, long)>();
        static bool SplitEvenDigits(List<long> list, int index)
        {
            long CurrentNum = list[index];
            string NumAsString = CurrentNum.ToString();
            int NumberOfDigits = NumAsString.Length;

            if (NumberOfDigits % 2 == 0) //Even
            {
                if (Splits.Keys.Contains(NumAsString))
                {
                    list[index] = Splits[NumAsString].Item1;
                    list.Insert(index + 1, Splits[NumAsString].Item2);
                }
                else
                {
                    string Sub1 = NumAsString.Substring(0, NumberOfDigits / 2);
                    string Sub2 = NumAsString.Substring(NumberOfDigits / 2, NumberOfDigits / 2);

                    list[index] = long.Parse(Sub1);
                    list.Insert(index + 1, long.Parse(Sub2));
                    Splits.Add(NumAsString, (list[index], list[index + 1]));
                }
                return true;
            }

            return false;
        }

        static Dictionary<long, long> PreCalc = new Dictionary<long, long>();
        static void Multiply(List<long> Stones,int i)
        {
            long start = Stones[i];

            if (PreCalc.Keys.Contains(start))
            {
                Stones[i] = PreCalc[start];
            }
            else
            {
                long x = Stones[i];
                Stones[i] = (x << 11) - (x << 4) - (x << 3);
                PreCalc.Add(start, Stones[i]);
            }
        }

        static void Blink(List<long> Stones)
        {
            for (int i = 0; i < Stones.Count; i++)
            {
                bool ChangedAZero = ChangeToZero(Stones, i);
                bool SplitDigits = SplitEvenDigits(Stones, i);

                if (!ChangedAZero)
                {
                    if (!SplitDigits)
                    {
                        Multiply(Stones, i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            List<long> Stones = ConvertToListLong("day11.txt");

            for (int i = 0; i < 75; i++)
            {
                Blink(Stones);
                Console.WriteLine($"Blinking {i}");
            }

            Console.WriteLine($"Stones Count: {Stones.Count()}");
            Console.ReadKey();
        }
    }
}
