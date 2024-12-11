using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_11
{
    internal class Program
    {
        static List<long> ConvertToListInt(string path)
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

        static bool SplitEvenDigits(List<long> list, int index)
        {
            long CurrentNum = list[index];
            string NumAsString = CurrentNum.ToString();
            int NumberOfDigits = NumAsString.Length;

            if(NumberOfDigits % 2 == 0) //Even
            {
                string Sub1 = NumAsString.Substring(0, NumberOfDigits / 2);
                string Sub2 = NumAsString.Substring(NumberOfDigits / 2, NumberOfDigits / 2);

                list[index] = long.Parse(Sub1);
                list.Insert(index + 1, long.Parse(Sub2));
                return true;
            }

            return false;
        }

        static void Multiply2024(List<long> list, int index)
        {
            long x = list[index];
            list[index] = (x<< 11) - (x<<4)-(x << 3);
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
                        Multiply2024(Stones, i);
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
            List<long> Stones = ConvertToListInt("day11.txt");

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
