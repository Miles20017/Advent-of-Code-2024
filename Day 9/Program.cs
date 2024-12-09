using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_9
{
    internal class Program
    {
        struct file
        {
            public int fileId;
            public int length;
            public bool Tested;
        }

        static List<file> RepresentFragmentation(List<char> Disk)
        {
            int fileID = 0;
            List<file> FragmentedDrive = new List<file>();
            for (int i = 0; i < Disk.Count-1; i++)
            {
                if (i % 2 != 0) //Funily doesn't work if you use (i%2==0.5)
                {
                    file myFile = new file();
                    myFile.fileId = -1; //Not a file
                    myFile.length = int.Parse(Disk[i].ToString());

                    for (int j = 0; j < myFile.length; j++)
                    {
                        FragmentedDrive.Add(myFile);
                    }
                }
                else
                {
                    file myFile = new file();
                    myFile.fileId = fileID;
                    myFile.length = int.Parse(Disk[i].ToString());

                    for (int j = 0; j < myFile.length; j++)
                    {
                        FragmentedDrive.Add(myFile);
                    }

                    fileID++;
                }
            }

            return FragmentedDrive;
        }

        static void Defragment(List<file> Disk) //List is parsed by reference
        {
            for(int i=0;i< Disk.Count; i++)
            {
                if (Disk[i].fileId==-1) //Free Space
                {
                    for(int j = Disk.Count-1; j > i; j--) //For every element after the free space
                    {
                        if (Disk[j].fileId!=-1) //Is a file
                        {
                            Disk[i]= Disk[j];

                            file myFile = new file();
                            myFile.fileId = -1;

                            Disk[j] = myFile;
                            break;
                        }
                    }
                }
            }
        }

        static void PrintDrive(List<file> Drive)
        {
            for (int i = 0; i < Drive.Count; i++)
            {
                if (Drive[i].fileId == -1)
                {
                    Console.Write('.');
                }
                else
                {
                    Console.Write(Drive[i].fileId);
                }
            }
            Console.WriteLine();
        }

        static long CalculateCheckSum(List<file> disk)
        {
            long CheckSum = 0;
            for(int i=0;i< disk.Count; i++)
            {
                if (disk[i].fileId != -1)
                {
                    CheckSum += i * disk[i].fileId;
                }
            }

            return CheckSum;
        }

        static void DefragmentPT2(List<file> Disk)
        {
            for (int i = 0; i < Disk.Count; i++)
            {
                if (Disk[i].fileId == -1) //Free Space
                {
                    for (int j = Disk.Count - 1; j > i; j--) //For every element after the free space
                    {
                        if (Disk[j].fileId != -1) //Is a file
                        {
                            if (Disk[i].length >= Disk[j].length)
                            {
                                int FileStartIndex = (j - Disk[j].length) + 1;

                                //Move It!
                                int length = Disk[i].length;
                                for(int p = 0; p < length; p++)
                                {
                                    file myFile = new file();
                                    myFile.fileId = -1;
                                    myFile.length = length - Disk[j].length;

                                    Disk[i + p] = myFile;
                                }

                                for (int p = 0; p < Disk[j].length; p++)
                                {
                                    Disk[i + p] = Disk[FileStartIndex + p];

                                    file myFile = new file();
                                    myFile.fileId = -1;
                                    Disk[FileStartIndex + p] = myFile;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            List<char> Disk = new List<char>();

            string path = "day9.txt";
            using(StreamReader reader = new StreamReader(path))
            {
                Disk = reader.ReadToEnd().ToList();
            }

            List<file> FragmentedDrive = RepresentFragmentation(Disk);
            //Uncomment the following line for the solution to part 1
            //Defragment(FragmentedDrive);

            //Comment out the following line for the soltuion to part 1
            DefragmentPT2(FragmentedDrive);
            Console.WriteLine(CalculateCheckSum(FragmentedDrive));
            

            Console.ReadKey();
        }
    }
}
