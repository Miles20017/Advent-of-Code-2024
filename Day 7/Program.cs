using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Day_7
{
    internal class Program
    {
        struct Equation
        {
            public long Target;
            public List<int> Operands;
        }

        static bool thisIterationValid(Equation equation, int Iteration)
        {
            long calc = equation.Operands[0];

            for(int i = 1; i < equation.Operands.Count(); i++)
            {
                bool operatorIsMul = (Iteration % 3) == 1;
                bool operatorIsAdd = (Iteration % 3) == 0;

                Iteration /= 3;

                if (operatorIsMul)
                {
                    calc *= equation.Operands[i];
                }
                else if(operatorIsAdd){
                    calc += equation.Operands[i];
                }
                else
                {
                    calc = concatonate(calc, equation.Operands[i]);
                }
                if (calc > equation.Target) return false;
            }

            if (calc == equation.Target) return true;
            return false;
        }

        static bool isValidEquation(Equation equation)
        {
            for(int i = 0; i < Math.Pow(3, equation.Operands.Count()-1); i++)
            {
                if (thisIterationValid(equation, i)) return true;
            }
            Console.WriteLine("DONE 1");
            return false;
        }

        static long concatonate(long a,int b)
        {
            string p1 = a.ToString();
            string p2 = b.ToString();
            string f = p1 + p2;
            return long.Parse(f);
        }

        static Equation Split(string equation)
        {
            Equation SplitEquation = new Equation();
            SplitEquation.Target = long.Parse(equation.Substring(0,equation.IndexOf(':')));
            SplitEquation.Operands = new List<int>();

            string ToAdd = "";
            for(int i = equation.IndexOf(':')+2; i < equation.Length; i++)
            {
                if (equation[i]==' ' || equation[i]=='\n')
                {
                    SplitEquation.Operands.Add(int.Parse(ToAdd));
                    ToAdd = "";
                }
                else
                {
                    ToAdd += equation[i];
                }

                if (i == equation.Length-1)
                {
                    SplitEquation.Operands.Add(int.Parse(ToAdd));
                    ToAdd = "";
                }
            }

            return SplitEquation;
        }

        static void Main(string[] args)
        {
            List<Equation> Equations = new List<Equation>();
            long total = 0;

            string path = "day7.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    Equation Equation = Split(reader.ReadLine());
                    if (isValidEquation(Equation)){
                        total += Equation.Target;
                    }
                }
            }
            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
