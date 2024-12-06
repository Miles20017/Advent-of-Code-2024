using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6___Attempt_2
{
    internal class Program
    {
        struct point
        {
            public char c;
            public string Direction;
        }

        struct coordinate
        {
            public int x, y;
        }

        static bool WithinBounds(point[,] map, int x, int y)
        {
            return x > -1 && y > -1 && x < map.GetLength(1) && y < map.GetLength(0);
        }

        static void DrawPath(point[,] map, coordinate startPoint)
        {
            string Direction = "UP";

            //Values change based on direction, Starting up => y value decreases with each move, hence = -1
            coordinate VectorToAdd = new coordinate();
            VectorToAdd.x = 0;
            VectorToAdd.y = -1;

            int x = startPoint.x;
            int y = startPoint.y;

            while (WithinBounds(map, x, y) && map[y, x].c != '#')
            {
                while (WithinBounds(map, x, y) && map[y, x].c != '#')
                {
                    map[y, x].c = 'X';
                    map[y,x].Direction = Direction;
                    x += VectorToAdd.x;
                    y += VectorToAdd.y;
                }

                //We moved our pointer onto a # so we need to move it back before turning
                if (WithinBounds(map, x, y))
                {
                    map[y, x].c = '#';
                    map[y, x].Direction = null;
                    x -= VectorToAdd.x;
                    y -= VectorToAdd.y;
                }

                switch (Direction)
                {
                    case "UP":
                        VectorToAdd.x = 0;
                        VectorToAdd.y = -1;
                        Direction = "RIGHT";
                        break;
                    case "DOWN":
                        VectorToAdd.x = 0;
                        VectorToAdd.y = 1;
                        Direction = "LEFT";
                        break;
                    case "LEFT":
                        VectorToAdd.x = -1;
                        VectorToAdd.y = 0;
                        Direction = "UP";
                        break;
                    case "RIGHT":
                        VectorToAdd.x = 1;
                        VectorToAdd.y = 0;
                        Direction = "DOWN";
                        break;
                    default:
                        Console.WriteLine("Error Code 1: Direction value is not recognisde by program");
                        Console.WriteLine($"Direction Value: {Direction}");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            List<string> Lines = new List<string>();

            string path = "day6.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                Lines.Add(reader.ReadLine());
            }

            point[,] Map = new point[Lines.Count(), Lines[0].Count()];
            coordinate StartPos = new coordinate();

            for(int x = 0; x < Lines[0].Count(); x++)
            {
                for(int y = 0; y < Lines.Count(); y++)
                {
                    Map[y, x].c = Lines[y][x];

                    if (Map[y, x].c == '^')
                    {
                        StartPos.x = x;
                        StartPos.y = y;
                    }
                }
            }

            //Now we have a 2D array called Map, where each character has a direction (instantiated at null)

            int counter = 0;
            DrawPath(Map, StartPos);
            for (int x = 0; x < Lines[0].Count(); x++)
            {
                for (int y = 0; y < Lines.Count(); y++)
                {
                    if (Map[y, x].c == 'X')
                    {
                        counter++;
                    }
                }
            }

            Console.WriteLine($"Unique Positions For Guard: {counter}");
            Console.ReadKey();
        }
    }
}