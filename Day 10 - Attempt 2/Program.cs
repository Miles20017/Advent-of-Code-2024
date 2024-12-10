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

        static int DFS(Node[,] map, Node CurrentNode, int Target)
        {

        }

        static int FindStartNodes(Node[,] map)
        {
            int Score = 0;

            foreach(Node node in map)
            {
                if (node.value == 0)
                {
                    Score += 
                }
            }
        }

        static void Main(string[] args)
        {
            Node[,] map = ConvertTextFile("day10test.txt");


        }
    }
}
