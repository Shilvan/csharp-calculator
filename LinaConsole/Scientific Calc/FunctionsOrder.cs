using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinaConsole
{
    public class FunctionsOrder
    {
        public static double FunctionsAttachedToNumber (List<string> inputList)
        {
            for (byte i = 1; inputList.Contains("!") || inputList.Contains("%"); i++)
            {
                if (inputList[i] == "!")
                {
                    int factor = int.Parse(inputList[i - 1]);
                    BigInteger result = factor < 0 ? -Factorial.Calculate(Math.Abs(factor)) : Factorial.Calculate(factor);
                    inputList.RemoveRange(i - 1, 2);
                    inputList.Insert(i-1, result.ToString());
                    return FunctionsAttachedToNumber(inputList);
                }
                if (inputList[i] == "%")
                {
                    double result = double.Parse(inputList[i - 1]) / 100;
                    inputList.RemoveRange(i-1,2);
                    inputList.Insert(i-1, (result).ToString());
                    return FunctionsAttachedToNumber(inputList);
                }
            }
            return NormalFunctions(inputList);
        }
        
        public static double NormalFunctions(List<string> inputList)
        {
            
            for (byte i = 0; (inputList.Exists(element => Regex.Match(element, @"sin|cos|tan|log|ln|[;v]|\^v").Success)); i++)
            {
                if (Regex.Match(inputList[i], @"sin|cos|tan").Success)
                {
                    return NormalFunctions(TrigonometryOperations.CalculateAndReplace(inputList,i, inputList[i], double.Parse(inputList[i+1])));
                }

                if (inputList.Count > i + 2 && inputList[i] == "log" && inputList[i + 2] == ";")
                {
                        return NormalFunctions(LogarithmOperations.CalculateAndReplace(inputList, i, ";", double.Parse(inputList[i + 1]), double.Parse(inputList[i + 3])));

                }
                
                if (Regex.Match(inputList[i], @"log|ln").Success)
                {
                    return NormalFunctions(LogarithmOperations.CalculateAndReplace(inputList, i, inputList[i], double.Parse(inputList[i + 1]), 0));
                }

                if (inputList[i] == "v")
                {
                    return NormalFunctions(Arithmetics.CalculateAndReplace(inputList, i, double.Parse(inputList[i + 1]), 'v', 1.0 / 2.0));

                }
                if (inputList[i] == "^v")
                {
                    return NormalFunctions(Arithmetics.CalculateAndReplace(inputList, i, double.Parse(inputList[i + 1]), '$', 1.0 / double.Parse(inputList[i - 1])));
                }
                
            }

            return PermutationsAndCombinations(inputList);
        }


        public static double PermutationsAndCombinations(List<string> inputList)
        {
            for (byte i = 1; inputList.Contains("c") || inputList.Contains("p"); i++)
            {
                if (inputList[i] == "c")
                {
                    return PermutationsAndCombinations(Factorial.CalculateAndReplace(inputList, i, int.Parse(inputList[i-1]), 'c', int.Parse(inputList[i + 1])));
                }
                if (inputList[i] == "p")
                {

                    return PermutationsAndCombinations(Factorial.CalculateAndReplace(inputList, i, int.Parse(inputList[i - 1]), 'p', int.Parse(inputList[i + 1])));
                }
            }
            return ArithmeticsOrder.Exponentiation(inputList);
        }


    }
}
