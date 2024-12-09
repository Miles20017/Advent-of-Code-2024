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
        static int LoopCounter = 0;
        static int LoopingError = 0;

        static bool FoundLoop(point node, string Direction)
        {
            if (node.Direction == Direction && node.c == 'X')
            {
                Console.WriteLine("Loop Found!");
                return true;
            }
            return false;
        }

        static void DrawTestGrid(point[,] map)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for(int y = 0; y < map.GetLength(0); y++)
                {
                    Console.Write(map[y, x].c); 
                }
                Console.WriteLine();
            }
        }

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
            int PathLength = 0;
            string Direction = "UP";

            //Values change based on direction, Starting up => y value decreases with each move, hence = -1
            coordinate VectorToAdd = new coordinate();
            VectorToAdd.y = 0;
            VectorToAdd.x = -1;

            int x = startPoint.y;
            int y = startPoint.x;

            while (WithinBounds(map, x, y)&&PathLength<10000)
            {
                while (WithinBounds(map, x, y) && (map[y, x].c != '#') && PathLength < 10000)
                {
                    if (FoundLoop(map[y,x],Direction)){
                        LoopCounter++;
                        return;
                    }
                    map[y, x].c = 'X';
                    map[y, x].Direction = Direction;
                    PathLength++;
                    x += VectorToAdd.x;
                    y += VectorToAdd.y;
                    //DrawTestGrid(map);
                    //Console.ReadKey();
                }

                //We moved our pointer onto a # so we need to move it back before turning
                if (WithinBounds(map, x, y))
                {
                    map[y, x].c = '#';
                    map[y, x].Direction = null;
                    x -= VectorToAdd.x;
                    y -= VectorToAdd.y;
                    //DrawTestGrid(map);
                    //Console.ReadKey();
                    //Console.Clear();
                }

                switch (Direction)
                {
                    case "UP":
                        //Vector for moving right
                        VectorToAdd.y = 1;
                        VectorToAdd.x = 0;
                        Direction = "RIGHT";
                        break;
                    case "DOWN":
                        //Vector for moving left
                        VectorToAdd.y = -1;
                        VectorToAdd.x = 0;
                        Direction = "LEFT";
                        break;
                    case "LEFT":
                        //Vector for moving up
                        VectorToAdd.y = 0;
                        VectorToAdd.x = -1;
                        Direction = "UP";
                        break;
                    case "RIGHT":
                        //Vector for moving down
                        VectorToAdd.y = 0;
                        VectorToAdd.x = 1;
                        Direction = "DOWN";
                        break;
                    default:
                        Console.WriteLine("Error Code 1: Direction value is not recognisde by program");
                        Console.WriteLine($"Direction Value: {Direction}");
                        break;
                }
            }

            if (PathLength > 9999)
            {
                Console.WriteLine("Looping Error");
                LoopingError++;
            }
        }

        static void Main(string[] args)
        {
            List<string> Lines = new List<string>();

            string path = "day6.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                Console.WriteLine("Text File");
                while (!reader.EndOfStream)
                {
                    Lines.Add(reader.ReadLine());
                    Console.WriteLine(Lines[Lines.Count() - 1]);
                }
                Console.WriteLine();
            }

            point[,] Map = new point[Lines.Count(), Lines[0].Count()];
            coordinate StartPos = new coordinate();
            bool FoundStartPos = false;

            Console.WriteLine("2D Array Equivelant");
            for(int x = 0; x < Lines[0].Count(); x++)
            {
                for(int y = 0; y < Lines.Count(); y++)
                {
                    Map[y, x].c = Lines[x][y];
                    Console.Write(Map[y, x].c);

                    if (Lines[y][x] == '^')
                    {
                        StartPos.x = x;
                        StartPos.y = y;
                        FoundStartPos = true;

                    }
                }

                Console.WriteLine();
            }

            if(!FoundStartPos) Console.WriteLine($"Error Code 2: Failed To Find Guard Start Position. Using Start Position ({StartPos.x},{StartPos.y})");
            else
            {
                Console.WriteLine($"Guard Start Point: ({StartPos.x},{StartPos.y})");
            }
            //Now we have a 2D array called Map, where each character has a direction (instantiated at null)
            point[,] CleanMap = new point[Lines.Count(), Lines[0].Count()];
            for (int x = 0; x < Lines[0].Count(); x++)
            {
                for (int y = 0; y < Lines.Count(); y++)
                {
                    CleanMap[y, x] = Map[y, x];
                }
            }

            int counter = 0;
            Console.ReadKey();
            Console.Clear();
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

            //Some gross infinite Loop in here somewhere
            for (int x = 0; x < Lines[0].Count(); x++)
            {
                for (int y = 0; y < Lines.Count(); y++)
                {
                    point[,] AugamentedMap = new point[Lines.Count(), Lines[0].Count()];
                    for (int i = 0; i < Lines[0].Count(); i++)
                    {
                        for (int j = 0; j < Lines.Count(); j++)
                        {
                            AugamentedMap[j, i] = CleanMap[j, i];
                        }
                    }
                    //Console.Clear();
                    //DrawTestGrid(AugamentedMap);
                    //Console.ReadKey();
                    //Console.WriteLine("Copied Array Successfully");

                    if ((AugamentedMap[y, x].c != '#') && (AugamentedMap[y, x].c != '^'))
                    {
                        //Console.WriteLine("Remooved a point!");
                        AugamentedMap[y, x].c = '#';
                        DrawPath(AugamentedMap, StartPos);
                    }
                }
            }

            Console.WriteLine($"Unique Positions For Guard: {counter}");
            Console.WriteLine($"Number of possible loops: {LoopCounter}");
            Console.WriteLine($"With {LoopingError} Infinite Loops");
            Console.ReadKey();
        }
    }
}