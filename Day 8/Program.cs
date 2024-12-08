using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_8
{
    internal class Program
    {
        struct Vector
        {
            public int x;
            public int y;
        }

        static Vector FindDifference(int x1, int y1, int x2, int y2)
        {
            Vector vector = new Vector();
            vector.x = x2 - x1;
            vector.y = y2 - y1;
            return vector;
        }

        static void drawGrid(List<List<char>> map)
        {
            for (int y = 0; y < map.Count; y++)
            {
                for (int x = 0; x < map[0].Count; x++)
                {
                    Console.Write(map[y][x]);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            List<List<char>> map = new List<List<char>>();
            List<List<char>> cleanMap = new List<List<char>>();

            string path = "day8.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    List<char> line = new List<char>();
                    List<char> cleanList = new List<char>();
                    foreach (char c in reader.ReadLine())
                    {
                        line.Add(c);
                        cleanList.Add('.');
                    }
                    map.Add(line);
                    cleanMap.Add(cleanList);
                }
            }


            int antinodeCounter = 0;
            //Foreach character in the map
            for (int y = 0; y < map.Count; y++)
            {
                for (int x = 0; x < map[0].Count; x++)
                {
                    //if there's an antenna
                    if (map[y][x] != '.')
                    {
                        //foreach other antenna in the map
                        for (int y2 = 0; y2 < map.Count; y2++)
                        {
                            for (int x2 = 0; x2 < map[0].Count; x2++)
                            {
                                if (map[y2][x2] == map[y][x] && y2 != y && x2 != x)
                                {
                                    Vector myVector = FindDifference(x, y, x2, y2);
                                    bool addPoints1 = true;
                                    int i = 1;
                                    bool addPoints2 = true;
                                    while (addPoints1 || addPoints2)
                                    {
                                        try
                                        {
                                            cleanMap[y - (i*myVector.y)][x - (i*myVector.x)] = '#';
                                            //drawGrid(cleanMap);
                                        }
                                        catch
                                        {
                                            addPoints1 = false;
                                        }

                                        try
                                        {
                                            cleanMap[y + (i * myVector.y)][x + (i * myVector.x)] = '#';
                                            //drawGrid(cleanMap);
                                        }
                                        catch
                                        {
                                            addPoints2 = false;
                                        }
                                        i++;
                                    }
                                    Console.WriteLine("Working");
                                }
                            }
                        }
                    }
                }
            }

            for (int y = 0; y < map.Count; y++)
            {
                for (int x = 0; x < map[0].Count; x++)
                {
                    if (cleanMap[y][x] == '#')
                    {
                        antinodeCounter++;
                    }
                }
            }

            Console.WriteLine(antinodeCounter);

            Console.ReadKey();
        }
    }
}
