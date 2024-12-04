using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_4
{
    internal class Program
    {
        static bool checkLeftDiagonalDown(char[,] input, int x, int y)
        {
            string goal = "MAS";

            if (x > 1 && y<input.GetLength(0)-2)
            {
                for(int i = 0; i <3; i++)
                {
                    if (input[y + i, x - i] == goal[i])
                    {
                        //Yippie
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        static bool checkLeftDiagonalUp(char[,] input, int x, int y)
        {
            string goal = "MAS";

            if (x > 1 && y>1)
            {
                for (int i = 0; i <3; i++)
                {
                    if (input[y - i, x - i] == goal[i])
                    {
                        //Yippie
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        static bool checkRightDiagonalUp(char[,] input, int x, int y)
        {
            string goal = "MAS";

            if (x < input.GetLength(1)-2 && y > 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (input[y - i, x + i] == goal[i])
                    {
                        //Yippie
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        static bool checkRightDiagonalDown(char[,] input, int x, int y)
        {
            string goal = "MAS";

            if (x < input.GetLength(1)-2 && y < input.GetLength(0) - 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (input[y + i, x + i] == goal[i])
                    {
                        //Yippie
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        static bool VerticleUp(char[,] input, int x, int y)
        {
            string goal = "XMAS";

            if (y >2)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (input[y - i, x] == goal[i])
                    {
                        //Yippie
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        static bool VerticleDown(char[,] input, int x, int y)
        {
            string goal = "XMAS";

            if (y < input.GetLength(0)-3)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (input[y + i, x] == goal[i])
                    {
                        //Yippie
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        static bool HorizontalRight(char[,] input, int x, int y)
        {
            string goal = "XMAS";

            if (x<input.GetLength(1)-3)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (input[y, x + i] == goal[i])
                    {
                        //Yippie
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        static bool HorizontalLeft(char[,] input, int x, int y)
        {
            string goal = "XMAS";

            if (x > 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (input[y, x - i] == goal[i])
                    {
                        //Yippie
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        static int FindWords(char[,] input, int x, int y)
        {
            int counter = 0;

            if (checkLeftDiagonalDown(input, x, y)) counter++;
            if (checkLeftDiagonalUp(input, x, y)) counter++;
            if (checkRightDiagonalDown(input, x, y)) counter++;
            if (checkRightDiagonalUp(input, x, y)) counter++;
            if (VerticleDown(input, x, y)) counter++;
            if (VerticleUp(input, x, y)) counter++;
            if (HorizontalLeft(input, x, y)) counter++;
            if (HorizontalRight(input, x, y)) counter++;

            return counter;

        }

        static int IsXMAS(char[,] input, int x, int y)
        {
            int c = 0;

            if (checkRightDiagonalDown(input, x, y) && checkLeftDiagonalDown(input, x + 2, y)) c++;
            if (checkRightDiagonalDown(input, x, y) && checkRightDiagonalUp(input, x, y+2)) c++;
            if (checkRightDiagonalUp(input, x, y) && checkLeftDiagonalUp(input, x + 2, y)) c++;
            if (checkLeftDiagonalDown(input, x, y) && checkLeftDiagonalUp(input, x, y+2)) c++;

            return c;
        }

        static void Main(string[] args)
        {
            List<List<char>> WordSearch = new List<List<char>>();

            string path = "day4.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    WordSearch.Add(line.ToList());
                }
            }

            char[,] myArray = new char[WordSearch.Count,WordSearch[0].Count()];

            for(int i = 0; i < WordSearch.Count();i++)
            {
                for(int j = 0; j < WordSearch[0].Count(); j++)
                {
                    myArray[i, j] = WordSearch[i][j];
                }
            }

            int counter = 0;

            for(int x = 0; x < WordSearch[0].Count;x++)
            {
                for(int y = 0; y < WordSearch.Count(); y++)
                {
                    if (myArray[y, x] == 'M')
                    {
                        counter+=IsXMAS(myArray, x, y);
                    }
                }
            }

            Console.WriteLine(counter);
            Console.ReadKey();
        }
    }
}
