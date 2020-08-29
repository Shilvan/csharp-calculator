using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LinaConsole
{
    public class TrigonometryOperations
    {
        public static List<string> CalculateAndReplace(List<string>inputList, byte position, string trigType, double num)
        {
            inputList.RemoveRange(position, 2);
            inputList.Insert(position, Calculate(trigType, num).ToString());
            return inputList;
        }

        public  static double Calculate(string trigType, double num)
        {
           
            switch (trigType)
            {
                case "sin":
                    return Math.Sin(num * Math.PI / 180);
                case "sin^-1":
                    if (num > 1 || num < -1) throw new ArgumentException($"Out of inverse sine range, -1 < {num} > 1");
                    return Math.Asin(num) * 180 / Math.PI;
                case "sinh":
                    return Math.Sinh(num);
                case "sinh^-1":
                    return Math.Log(num + Math.Sqrt(num * num + 1));
                   

                case "cos":
                    return Math.Cos(num * Math.PI / 180);

                case "cos^-1":
                    if (num > 1 || num < -1) throw new ArgumentException($"Out of inverse cosine rang, -1 < {num} > 1");
                    return Math.Acos(num) * 180 / Math.PI;
                    
                case "cosh":
                    return  Math.Cosh(num);
                    
                case "cosh^-1":
                    return Math.Log(num + Math.Sqrt(num * num - 1));
                  

                case "tan":
                     return Math.Tan(num * Math.PI / 180);
                case "tan^-1":
                    return Math.Atan(num) * 180 / Math.PI;
                case "tanh":
                    return Math.Tanh(num);
                case "tanh^-1":
                    return Math.Log((1 + num) / (1 - num)) / 2;;
                default:
                    throw new ArgumentException($"Trigonometry - {trigType}");

            }
        
        }
          
        

      
    }
}
