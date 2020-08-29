using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinaConsole
{
    public class Arithmetics
    {
        public static List<string> CalculateAndReplace(List<string> lista, byte position, double num1,char operation, double num2)
        {
            double result = Calculate(num1, operation, num2);
            if (operation == 'v')
            {
                lista.RemoveRange(position, 2);
                lista.Insert(position, result.ToString());
            }
            else
            {
                 lista.RemoveRange(position-1, 3);
                 lista.Insert(position - 1, result.ToString());
            }
            
            return lista;
        }

        public static double Calculate (double num1, char operation, double num2)
        {
            switch (operation)
            {
                case 'v':
                    goto case '$';

                case '^':
                    if (Regex.Match(num1.ToString(), @"^-?0$").Success && Regex.Match(num2.ToString(), @"^-?0$").Success) throw new ArgumentException($"Indeterminate Result - {num1}^{num2}");
                    else if (Regex.Match(num2.ToString(), @"^-?0$").Success && num1 < 0)
                        return -1;
                    else return num1 < 0 ? -Math.Pow(Math.Abs(num1), num2) : Math.Pow(num1, num2);
                   
                case '$':
                    if (num1 > 0) return Math.Pow(num1, num2);

                    string fraction = DecimalToFraction.ConvertToFraction(Convert.ToDecimal(num2));
                    if (Regex.Match(num1.ToString(), @"^-?0$").Success && Regex.Match(num2.ToString(), @"^-?0$").Success) throw new ArgumentException($"Indeterminate Result - ({num1})^{num2}");
                    
                    else if (num1 < 0 && Regex.Match(fraction, @"/(\d+)?[02468]$").Success) throw new ArgumentException($"Non-Real Result - ({num1})^{num2}");
                    else if (Regex.Match(fraction, @"[02468]/|^-?(\d+)?[02468]$").Success)
                        return  Math.Pow(Math.Abs(num1), num2);
                    else return num1 < 0 ? -Math.Pow(Math.Abs(num1), num2) : Math.Pow(num1, num2);


                case '*':
                    return num1 * num2;
                    
                case '/':
                    if (num2 == 0) throw new ArgumentException($"indeterminate Result - {num1}/{num2}");
                    return num1 / num2;
                   
                case '+':
                    return num1 + num2;

                case '-':
                    return num1 - num2;

                default:
                    throw new ArgumentException($"Arithmetic - {operation}");
                    
                    
            }
            
        }


     
    }


}
