using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    public static class RomanNumeralsConverter
    {
        public static string convertExpression(string expression)
        {
            List<Char> signs = new List<char>();
            string[] parts = expression.Split('+', '-', '*', '/');
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '+' || expression[i] == '-' || expression[i] == '*' || expression[i] == '/')
                {
                    signs.Add(expression[i]);
                }
            }
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = convertNumberToDecimal(parts[i]);
            }
            expression = "";
            for (int i = 0; i < signs.Count; i++)
            {
                expression += parts[i];
                expression += signs[i];
            }
            expression += parts[parts.Length - 1];
            return expression;
        }

        public static string convertNumberToDecimal(string num)
        {
            if(num != "")
            {
                string symbols = "IVXLCDM";
                double res = getValue(num[num.Length - 1]);
                for (int i = num.Length - 2; i >= 0; i--)
                {
                    if (symbols.IndexOf(num[i]) >= symbols.IndexOf(num[i + 1]))
                    {
                        res += getValue(num[i]);
                    }
                    else
                    {
                        res -= getValue(num[i]);
                    }
                }
                return res.ToString();
            }
            else
            {
                return "";
            }
        }

        public static int getValue(char num)
        {
            switch(num)
            {
                case 'I': return 1;
                case 'V': return 5;
                case 'X': return 10;
                case 'L': return 50;
                case 'C': return 100;
                case 'D': return 500;
                case 'M': return 1000;
                default: return 0;
            }
        }

        public static string convertNumberToRoman(string num)
        {
            bool isNegative = false;
            string result = "";
            if (num[0] == '-')
            {
                num = num.Remove(0, 1);
                isNegative = true;
            }
            for(int i = num.Length-1; i >= 0; i--)
            {
                if(i == num.Length - 1)
                {
                    switch (num[i])
                    {
                        case '1': result = result.Insert(0, "I"); break;
                        case '2': result = result.Insert(0, "II"); break;
                        case '3': result = result.Insert(0, "III"); break;
                        case '4': result = result.Insert(0, "IV"); break;
                        case '5': result = result.Insert(0, "V"); break;
                        case '6': result = result.Insert(0, "VI"); break;
                        case '7': result = result.Insert(0, "VII"); break;
                        case '8': result = result.Insert(0, "VIII"); break;
                        case '9': result = result.Insert(0, "IX"); break;
                        default: result += ""; break;
                    }
                }
                if (i == num.Length - 2)
                {
                    switch (num[i])
                    {
                        case '1': result = result.Insert(0, "X"); break;
                        case '2': result = result.Insert(0, "XX"); break;
                        case '3': result = result.Insert(0, "XXX"); break;
                        case '4': result = result.Insert(0, "XL"); break;
                        case '5': result = result.Insert(0, "L"); break;
                        case '6': result = result.Insert(0, "LX"); break;
                        case '7': result = result.Insert(0, "LXX"); break;
                        case '8': result = result.Insert(0, "LXXX"); break;
                        case '9': result = result.Insert(0, "XC"); break;
                        default: result += ""; break;
                    }
                }
                if (i == num.Length - 3)
                {
                    switch (num[i])
                    {
                        case '1': result = result.Insert(0, "C"); break;
                        case '2': result = result.Insert(0, "CC"); break;
                        case '3': result = result.Insert(0, "CCC"); break;
                        case '4': result = result.Insert(0, "CD"); break;
                        case '5': result = result.Insert(0, "D"); break;
                        case '6': result = result.Insert(0, "DC"); break;
                        case '7': result = result.Insert(0, "DCC"); break;
                        case '8': result = result.Insert(0, "DCCC"); break;
                        case '9': result = result.Insert(0, "CM"); break;
                        default: result += ""; break;
                    }
                }
                if (i < num.Length - 3)
                {
                    result = result.Insert(0, new string('M', int.Parse(num[i].ToString()) * int.Parse(Math.Pow(10, num.Length - i - 4).ToString())));
                }
            }
            if(isNegative)
            {
                result = result.Insert(0, "-");
            }
            return result;
        }
    }
}
