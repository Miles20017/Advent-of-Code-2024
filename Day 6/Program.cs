using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Day_6
{
    public class Point
    {
        public int x;
        public int y;
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

    internal class Program
    {
        static string Direction = "UP";
        static int LoopCount = 0;

        static bool WithinBounds(List<List<Point>> map, int GuardX, int GuardY)
        {
            return (GuardX < map[0].Count() && GuardX > -1) && (GuardY < map.Count() && GuardY > -1);
        }

        static int MoveUp(ref List<List<Point>> map, ref int GuardX, ref int GuardY)
        {
            int counter = 0;

            if (map[GuardY][GuardX].c == 'X' && map[GuardY][GuardX].Direction == Direction)
            {
                LoopCount++;
                return 0;
            }
            else
            {
                while (WithinBounds(map, GuardX, GuardY))
                {
                    if (map[GuardY][GuardX].c == '#')
                    {
                        Direction = "RIGHT";
                        GuardY++;
                        break;
                    }
                    else
                    {
                        map[GuardY][GuardX].c = 'X';
                        map[GuardY][GuardX].Direction = Direction;
                        GuardX++;
                    }
                }
            }

            return counter;
        }

        static int MoveDown(ref List<List<Point>> map, ref int GuardX, ref int GuardY)
        {
            int counter = 0;

            if (map[GuardY][GuardX].c == 'X' && map[GuardY][GuardX].Direction == Direction)
            {
                LoopCount++;
                return 0;
            }
            else
            {
                while (WithinBounds(map, GuardX, GuardY))
                {
                    if (map[GuardY][GuardX].c == '#')
                    {
                        Direction = "LEFT";
                        GuardY--;
                        break;
                    }
                    else
                    {
                        map[GuardY][GuardX].c = 'X';
                        map[GuardY][GuardX].Direction = Direction;
                        GuardY++;
                    }
                }
            }
            return counter;
        }

        static int MoveLeft(ref List<List<Point>> map, ref int GuardX, ref int GuardY)
        {
            int counter = 0;

            if (map[GuardY][GuardX].c == 'X' && map[GuardY][GuardX].Direction == Direction)
            {
                LoopCount++;
                return 0;
            }
            else
            {
                while (WithinBounds(map, GuardX, GuardY))
                {
                    if (map[GuardY][GuardX].c == '#')
                    {
                        Direction = "UP";
                        GuardX++;
                        break;
                    }
                    else
                    {
                        map[GuardY][GuardX].c = 'X';
                        map[GuardY][GuardX].Direction = Direction;
                        GuardX--;
                    }
                }
            }
            return counter;
        }

        static int MoveRight(ref List<List<Point>> map, ref int GuardX, ref int GuardY)
        {
            int counter = 0;

            if (map[GuardY][GuardX].c == 'X' && map[GuardY][GuardX].Direction == Direction)
            {
                LoopCount++;
                return 0;
            }
            else
            {
                while(WithinBounds(map, GuardX, GuardY))
                {
                    if (map[GuardY][GuardX].c == '#')
                    {
                        Direction = "DOWN";
                        GuardX--;
                        break;
                    }
                    else
                    {
                        map[GuardY][GuardX].c = 'X';
                        map[GuardY][GuardX].Direction = Direction;
                        GuardX++;
                    }
                }
            }

            return counter;
        }

        static int MOVE(List<List<Point>> map, int GuardX, int GuardY)
        {
            int counter = 0;
            int LastCounter = -1;

            while (counter > LastCounter)
            {
                LastCounter = counter;
                switch (Direction)
                {
                    case "UP":
                        counter += MoveUp(ref map, ref GuardX, ref GuardY);
                        break;
                    case "DOWN":
                        counter += MoveDown(ref map, ref GuardX, ref GuardY);
                        break;
                    case "LEFT":
                        counter += MoveLeft(ref map, ref GuardX, ref GuardY);
                        break;
                    case "RIGHT":
                        counter += MoveRight(ref map, ref GuardX, ref GuardY);
                        break;
                }

                //for (int x = 0; x < map[0].Count(); x++)
                //{
                //    for (int y = 0; y < map.Count(); y++)
                //    {
                //        Console.Write(map[y][x].c);
                //    }
                //    Console.WriteLine();
                //}
                //Thread.Sleep(100);
                //Console.Clear();

            }

            return counter;
        }

        static void Main(string[] args)
        {
            List<List<Point>> map = new List<List<Point>>();
            int GuardX=0, GuardY=0;

            string path = "day6.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while (!reader.EndOfStream)
                {
                    line=reader.ReadLine();
                    if (line.Contains('^'))
                    {
                        GuardX = line.IndexOf('^');
                        GuardY = map.Count();
                    }
                    List<Point> myList = new List<Point>();
                    for(int i = 0; i < line.Length; i++)
                    {
                        myList.Add(new Point(i, map.Count(), line[i], Direction));
                    }
                    map.Add(myList);
                }
            }

            List<List<Point>> CLEANMAP = DeepCopy(map);


            int counter = MOVE(map,GuardX,GuardY);
            Console.WriteLine("COUNTER:");
            Console.WriteLine(counter);


            int ValidLoops = 0;
            for(int x = 0; x < map[0].Count(); x++)
            {
                for(int y = 0; y < map.Count(); y++)
                {
                    List<List<Point>> AlteredMap = DeepCopy(CLEANMAP);
                    if (AlteredMap[y][x].c!='#' && AlteredMap[y][x].c != '^')
                    {
                        AlteredMap[y][x].c = '#';
                    }
                    MOVE(AlteredMap, GuardX, GuardY);
                    if (LoopCount > 0) ValidLoops++;
                    LoopCount = 0;
                }
            }

            Console.WriteLine($"Loops: {LoopCount}");
            Console.WriteLine(ValidLoops);

            Console.ReadKey();
        }
        static List<List<Point>> DeepCopy(List<List<Point>> original)
        {
            List<List<Point>> copy = new List<List<Point>>();

            foreach (var row in original)
            {
                List<Point> newRow = new List<Point>();
                foreach (var point in row)
                {
                    newRow.Add(new Point(point.x, point.y, point.c, point.Direction));
                }
                copy.Add(newRow);
            }

            return copy;
        }


    }
}
