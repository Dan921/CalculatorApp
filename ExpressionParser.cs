using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    public static class ExpressionParser
    {
        public static double calculate(string expression, int roudingNum)
        {
            List<Char> signs1 = new List<char>();
            string[] parts1 = expression.Split('+', '-');
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '+' || expression[i] == '-')
                {
                    signs1.Add(expression[i]);
                }
            }

            for (int i = 0; i < parts1.Length; i++)
            {
                string[] parts2 = parts1[i].Split('*', '/');
                List<Char> signs2 = new List<char>();
                for (int j = 0; j < parts1[i].Length; j++)
                {
                    if (parts1[i][j] == '*' || parts1[i][j] == '/')
                    {
                        signs2.Add(parts1[i][j]);
                    }
                }

                //Корень и факториал
                for (int f = 0; f < parts2.Length; f++)
                {
                    if (parts2[f] != "" && parts2[f][parts2[f].Length - 1] == '!' && parts2[f][0] == '√')
                    {
                        parts2[f] = parts2[f].Remove(parts2[f].Length - 1, 1);
                        parts2[f] = parts2[f].Remove(0, 1);
                        parts2[f] = Math.Sqrt(double.Parse(parts2[f])).ToString();
                        parts2[f] = factorial(double.Parse(parts2[f])).ToString();
                    }
                }

                //Факториал
                for (int f = 0; f < parts2.Length; f++)
                {
                    if (parts2[f] != "" && parts2[f][parts2[f].Length - 1] == '!')
                    {
                        parts2[f] = factorial(double.Parse(parts2[f].Remove(parts2[f].Length - 1, 1))).ToString();
                    }
                }

                //Корень
                for (int k = 0; k < parts2.Length; k++)
                {
                    if (parts2[k] != "" && parts2[k][0] == '√')
                    {
                        parts2[k] = Math.Sqrt(double.Parse(parts2[k].Remove(0, 1))).ToString();
                    }
                }

                for (int j = 0; j < signs2.Count; j++)
                {
                    if (signs2[j] == '*')
                    {
                        parts2[j + 1] = (double.Parse(parts2[j]) * double.Parse(parts2[j + 1])).ToString();
                    }
                    else
                    {
                        parts2[j + 1] = (double.Parse(parts2[j]) / double.Parse(parts2[j + 1])).ToString();
                    }
                }
                parts1[i] = parts2[parts2.Length - 1];
            }

            if (expression[0] != '+' && expression[0] != '-')
            {
                for (int i = 0; i < signs1.Count; i++)
                {
                    if (signs1[i] == '+')
                    {
                        parts1[i + 1] = (double.Parse(parts1[i]) + double.Parse(parts1[i + 1])).ToString();
                    }
                    else
                    {
                        parts1[i + 1] = (double.Parse(parts1[i]) - double.Parse(parts1[i + 1])).ToString();
                    }
                }
            }

            else
            {
                parts1[1] = signs1[0] + parts1[1];
                for (int i = 1; i < signs1.Count; i++)
                {
                    if (signs1[i] == '+')
                    {
                        parts1[i + 1] = (double.Parse(parts1[i]) + double.Parse(parts1[i + 1])).ToString();
                    }
                    else
                    {
                        parts1[i + 1] = (double.Parse(parts1[i]) - double.Parse(parts1[i + 1])).ToString();
                    }
                }
            }

            return Math.Round(double.Parse(parts1[parts1.Length - 1]), roudingNum);
        }

        private static double factorial(double num)
        {
            double fact = 1;
            for (int k = 2; k <= num; k++)
            {
                fact *= k;
            }
            return fact;
        }
    }
}
