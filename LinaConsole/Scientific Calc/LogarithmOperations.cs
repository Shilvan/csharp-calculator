using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaConsole
{
    class LogarithmOperations
    {
        public static List<string> CalculateAndReplace(List<string> lista, byte position, string logType, double num1, double num2)
        {
            double result = Calculate(logType,num1, num2);
            if (logType == ";")
            {
                lista.RemoveRange(position, 4);
                lista.Insert(position, result.ToString());
            }
            else
            {
                lista.RemoveRange(position, 2);
                lista.Insert(position, result.ToString());
            }
            
            return lista;
        }

        public static double Calculate(string logType, double num1, double num2)
        {
            switch (logType)
            {
                case "log":
                    return Math.Log10(num1);
                    
                case "ln":
                    return Math.Log(num1);
                    
                case ";":
                    return Math.Log(num2, num1);
                default:
                    throw new ArgumentException($"Logarithm - {logType}");
            }
            
        }

    }
}
