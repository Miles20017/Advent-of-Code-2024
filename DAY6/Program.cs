using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

public class Point
{
    public int x, y;
    public char c;
    public string Direction;

    public Point(int x, int y, char c, string Direction)
    {
        this.x = x;
        this.y = y;
        this.c = c;
        this.Direction = Direction;
    }
}

class Program
{
    static string Direction = "UP";
    
    static void Main(string[] args)
    {
        List<List<Point>> map = new List<List<Point>>();
        int GuardX = 0, GuardY = 0;

        string path = "test6.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                if (line.Contains('^'))
                {
                    GuardX = line.IndexOf('^');
                    GuardY = map.Count();
                }
                List<Point> myList = new List<Point>();
                for (int i = 0; i < line.Length; i++)
                {
                    myList.Add(new Point(i, map.Count(), line[i], Direction));
                }
                map.Add(myList);
            }
        }


        Console.ReadKey();
    }
}
