using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_10___Attempt_2
{
    public class Node
    {
        public int value;
        public bool visited = false;

        private int row;
        private int col;
        

        public Node(int value, int row, int col)
        {
            this.value = value;
            this.row = row;
            this.col = col;
        }

        public (int,int) GetCoordinates()
        {
            return (row,col);
        }
    }

    internal class Program
    {
        static Node[,] ConvertTextFile(string path)
        {
            Node[,] map;

            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();
                map = new Node[line.Length, line.Length];
            }

            using (StreamReader reader = new StreamReader(path))
            {
                for(int row = 0; row < map.GetLength(0); row++)
                {
                    string line = reader.ReadLine();

                    for (int col = 0; col < map.GetLength(1) ; col++)
                    {
                        map[row, col] = new Node(int.Parse(line.Substring(col, 1)), row, col);
                    }
                }
            }

            return map;
        }

        static List<Node> Adjacent(Node[,] map, Node node)
        {
            List<Node> adjacent = new List<Node>();

            int y = node.GetCoordinates().Item1;
            int x = node.GetCoordinates().Item2;

            try
            {
                adjacent.Add(map[y - 1 , x]);
            }
            catch { }
            try
            {
                adjacent.Add(map[y + 1 , x]);
            }
            catch { }
            try
            {
                adjacent.Add(map[y, x - 1]);
            }
            catch { }
            try
            {
                adjacent.Add(map[y, x + 1]);
            }
            catch { }


            return adjacent;
        }

        static void DFS(Node[,] map, Node CurrentNode, int Target, ref int count)
        {
            CurrentNode.visited = true;

            if(CurrentNode.value == Target)
            {
                count++;
            }
            else
            {
                foreach(Node node in Adjacent(map, CurrentNode))
                {
                    if((!node.visited) && (node.value - CurrentNode.value) == 1)
                    {  
                        DFS(map, node, Target,ref count);
                    }
                }
            }
        }

        static int  DFS2(Node[,] map, Node CurrentNode, int Target)
        {
            CurrentNode.visited = true;

            if (CurrentNode.value == Target)
            {
                return 1;
            }
            else
            {
                int total = 0;
                foreach (Node node in Adjacent(map, CurrentNode))
                {
                    if ((node.value - CurrentNode.value) == 1)
                    {
                        total+=DFS2(map, node, Target);
                    }
                }
                return total;
            }
        }

        static void ResetFlags(Node[,] map)
        {
            foreach(Node node in map)
            {
                node.visited = false;
            }
        }

        static int FindStartNodes(Node[,] map)
        {
            int Score = 0;

            foreach(Node node in map)
            {
                if (node.value == 0)
                {
                    int CurrentSearch = 0;
                    //DFS(map, node, 9, ref CurrentSearch);
                    CurrentSearch = DFS2(map, node, 9);
                    ResetFlags(map);
                    Score += CurrentSearch;
                    Console.WriteLine(CurrentSearch);
                }
            }

            return Score;
        }

        static void Main(string[] args)
        {
            Node[,] map = ConvertTextFile("day10.txt");

            Console.WriteLine($"Score: {FindStartNodes(map)}");
            Console.ReadKey();
        }
    }
}
