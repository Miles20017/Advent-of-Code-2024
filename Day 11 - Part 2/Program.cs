using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        List<long> Stones = ConvertToListlong("day11.txt");

        for (long i = 0; i < 75; i++)
        {
            Blink(Stones);
            Console.WriteLine($"Blinking {i}");
        }

        Console.WriteLine($"Stones Count: {Stones.Count}");
        Console.ReadKey();
    }

    static bool ChangeToZero(List<long> list, int index, Dictionary<long, bool> zeroMap)
    {
        if (list[index] == 0)
        {
            list[index] = 1;
            zeroMap[index] = false; // Update the hashmap
            return true;
        }
        return false;
    }

    static bool SplitEvenDigits(List<long> list, int index, Dictionary<long, bool> evenDigitMap)
    {
        long CurrentNum = list[index];
        string NumAsString = CurrentNum.ToString();
        int NumberOfDigits = NumAsString.Length;

        if (NumberOfDigits % 2 == 0) // Even
        {
            string Sub1 = NumAsString.Substring(0, NumberOfDigits / 2);
            string Sub2 = NumAsString.Substring(NumberOfDigits / 2, NumberOfDigits / 2);

            list[index] = long.Parse(Sub1);
            list.Insert(index + 1, long.Parse(Sub2));
            evenDigitMap[index] = false; // Update the hashmap
            evenDigitMap[index + 1] = (Sub2.Length % 2 == 0); // New number might also be even
            return true;
        }
        return false;
    }

    static void Multiply2024(List<long> list, int index)
    {
        list[index] *= 2024;
    }

    static void Blink(List<long> Stones)
    {
        Dictionary<long, bool> zeroMap = new Dictionary<long, bool>();
        Dictionary<long, bool> evenDigitMap = new Dictionary<long, bool>();

        for (int i = 0; i < Stones.Count; i++)
        {
            if (Stones[i] == 0)
                zeroMap[i] = true;
            string numStr = Stones[i].ToString();
            if (numStr.Length % 2 == 0)
                evenDigitMap[i] = true;
        }

        int index = 0;
        while (index < Stones.Count)
        {
            bool ChangedAZero = zeroMap.ContainsKey(index) && ChangeToZero(Stones, index, zeroMap);
            bool SplitDigits = evenDigitMap.ContainsKey(index) && SplitEvenDigits(Stones, index, evenDigitMap);

            if (!ChangedAZero)
            {
                if (!SplitDigits)
                {
                    Multiply2024(Stones, index);
                }
                else
                {
                    index++; // Skip the next element after splitting
                }
            }
            index++;
        }
    }

    static List<long> ConvertToListlong(string path)
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
}
