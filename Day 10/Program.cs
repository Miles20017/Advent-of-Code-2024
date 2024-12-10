using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Day_10
{
    internal class Program
    {
        struct Flag
        {
            public int score;
            public bool visited;
        }

        static List<List<Flag>> ConvertToJaggedArray(string path)
        {
            List<List<Flag>> map = new List<List<Flag>>();

            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    List<Flag> ToAdd = new List<Flag>();
                    string line = reader.ReadLine();
                    for (int i = 0; i < line.Length; i++)
                    {
                        Flag currentFlag = new Flag();
                        currentFlag.score =int.Parse(line.Substring(i,1));
                        currentFlag.visited = false;

                        ToAdd.Add(currentFlag);
                    }
                    map.Add(ToAdd);
                }
            }

            return map;
        }

        static List<Flag> AdjacentFlags(List<List<Flag>> map, Flag node, int x, int y)
        {
            List<Flag> adjacent = new List<Flag>();

            try
            {
                adjacent.Add(map[y - 1][x]);
            }
            catch { }
            try
            {
                adjacent.Add(map[y + 1][x]);
            }
            catch{ }
            try
            {
                adjacent.Add(map[y][x - 1]);
            }
            catch { }
            try
            {
                adjacent.Add(map[y][x + 1]);
            }
            catch{ }


            return adjacent;            
        }

        static void ResetFlags(List<List<Flag>> map)
        {
            for(int i = 0; i < map.Count; i++)
            {
                for(int j = 0; j < map[0].Count; j++)
                {
                    Flag currentFlag = map[i][j];
                    currentFlag.visited = false;
                }
            }

            DFSCounter = 0;
        }

        static int DFSCounter = 0;
        static void DFS(List<List<Flag>> map, Flag currentNode, int Target, int x, int y)
        {
            currentNode.visited = true;
            int cs = currentNode.score;
            Console.WriteLine($"Current Node Value: {cs}");

            if (cs == Target)
            {
                DFSCounter++;
            }
            else
            {
                List<Flag> Adjacent = AdjacentFlags(map, currentNode, x, y);
                foreach (Flag node in Adjacent)
                {
                    int score = node.score;
                    if ((!node.visited)  && (score == cs + 1))
                    {
                        var Coordinates = FindCoordinates(map, node);
                        DFS(map, node, Target, Coordinates.Item2, Coordinates.Item1);
                    }
                }
            }
        }

        static (int,int) FindCoordinates(List<List<Flag>> map, Flag node)
        {
            for (int row = 0; row < map.Count; row++)
            {
                for (int col = 0; col < map[0].Count; col++)
                {
                    if (map[row][col].Equals(node))
                    {
                        return (row, col);
                    }
                }
            }

            return (-1, -1);
        }

        static int DFSPreFunction(List<List<Flag>> map)
        {
            int score = 0;
            int NoOfTrailHeads = 0;

            for (int row = 0; row < map.Count; row++)
            {
                for (int col = 0; col < map[0].Count; col++)
                {
                    if (map[row][col].score == 0)
                    {
                        NoOfTrailHeads++;
                        DFS(map, map[row][col], 9, col, row);
                        Console.WriteLine($"TrailHead {NoOfTrailHeads} has score {DFSCounter}");
                        score+= DFSCounter;
                        ResetFlags(map);
                    }
                }
            }

            Console.WriteLine($"Found {NoOfTrailHeads} TrailHeads");
            return score;
        }

        static void PrintGrid(List<List<Flag>> map)
        {
            foreach(List<Flag> row in map)
            {
                foreach(Flag node in row)
                {
                    Console.Write(node.score);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            List<List<Flag>> map = ConvertToJaggedArray("day10test.txt");
            PrintGrid(map);
            Console.WriteLine($"Sum of all trailheads = {DFSPreFunction(map)}");
            PrintGrid(map);
            Console.ReadKey();
        }
    }
}
