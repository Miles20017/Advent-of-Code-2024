using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_5
{
    internal class Program
    {
        static int FindCentre(List<int> myList)
        {
            int length = myList.Count();
            length = length / 2;

            return myList[length];
        }

        static List<int> convertToIntList(string update) 
        {
            List<int> ordering = new List<int>();

            for (int i = 0; i < update.Length; i += 3)
            {
                ordering.Add(int.Parse(update.Substring(i, 2)));
            }

            return ordering;
        }


        static List<int> ReOrder(List<int> update, List<string> rules)
        {
            update.Sort((a, b) => {
                foreach (var rule in rules)
                {
                    int firstPage = int.Parse(rule.Substring(0, 2));
                    int secondPage = int.Parse(rule.Substring(3, 2));

                    if ((firstPage == a && secondPage == b) || (firstPage == b && secondPage == a))
                    {
                        return a == firstPage ? -1 : 1;
                    }
                }
                return 0;
            });
            return update;
        }

        static bool FollowsRule(string update, string rule)
        {
            List<int> ordering = convertToIntList(update);

            int preceding = int.Parse(rule.Substring(0, 2));
            int post = int.Parse(rule.Substring(3, 2));

            if (ordering.IndexOf(preceding) < ordering.IndexOf(post))
            {
                return true;
            }

            return false;
        }

        static void Main(string[] args)
        {
            List<string> Rules = new List<string>();
            List<string> updates = new List<string>();
            bool firstHalf = true;


            string path = "day5.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == "")
                    {
                        firstHalf = false;
                    }
                    else
                    {
                        if (firstHalf)
                        {
                            Rules.Add(line);
                        }
                        else
                        {
                            updates.Add(line);
                        }
                    }
                }
            }

            int counter = 0;
            int counter2 = 0;

            foreach (string Update in updates)
            {
                bool UpdateIsOkay = true;
                foreach (string Rule in Rules)
                {
                    if (Update.Contains(Rule.Substring(0, 2)) && Update.Contains(Rule.Substring(3, 2)))
                    {
                        if (!FollowsRule(Update, Rule))
                        {
                            UpdateIsOkay = false;
                        }
                    }
                }

                if (UpdateIsOkay)
                {
                    counter += FindCentre(convertToIntList(Update));
                }
                else
                {
                    counter2 += FindCentre(ReOrder(convertToIntList(Update),Rules));
                }
            }

            Console.WriteLine(counter);
            Console.WriteLine("------");
            Console.WriteLine(counter2);


            Console.ReadKey();
        }
    }
}
