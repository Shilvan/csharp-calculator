using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Numerics;


namespace LinaConsole
{
    public class Factorial
    {
        public static List<string> CalculateAndReplace(List<string> inputList,byte i,int n, char nOrP, int r)
        {
            BigInteger result = nOrP =='c'? Calculate(n) / (Calculate(r) * Calculate(n - r)): Calculate(n) / Calculate(n - r);
            
            inputList.RemoveRange(i-1, 3);
            inputList.Insert(i-1, result.ToString());

            return inputList;
        }


        public static BigInteger Calculate(int number)
        {
            BigInteger result = 1;
            if (number == 1 || number == 0)
                return result = 1;

            else
            {
                for (BigInteger i = 1; i <= number; ++i)
                    result *= i;
            }

            return result;

        }






        /*


        public static string FactorizeAndCalculatePercentage(string formulaExtras)
        {
            inv = null;
            if (Regex.Match(formulaExtras,@"[!%]").Success)
            {
                formulaExtras = formulaExtras.Replace("!", " !").Replace("%", " %").Replace("  ", " ");
                
                i = 0;
                string[] operation = formulaExtras.Split(' ', ';');
                foreach (var item in operation)
                {
                    if (item == "!")
                    {
                        int factor = 0;
                        if (!int.TryParse(operation[i-1],out factor))throw new ArgumentException($"Invalid Factor - {operation[i-1]}");
                        if (factor > 170 || factor < -170) throw new ArgumentException("Result Close To Infinity - Factorial");

                        
                        if (factor < 0)
                        {
                            factor = Math.Abs(factor);
                            inv = "-"; 
                        }

                        Factorize(factor);
                        return FactorizeAndCalculatePercentage(formulaExtras = new Regex(Regex.Escape(operation[i - 1] + " !")).Replace(formulaExtras, inv + result.ToString(), 1));

                    }
                    if (item == "%")
                    {
                        double percent = double.Parse(operation[i-1]) / 100;
                        return FactorizeAndCalculatePercentage(formulaExtras = new Regex(Regex.Escape(operation[i - 1] + " %")).Replace(formulaExtras,  Convert.ToString(percent), 1));
                    }
                    else
                        i += 1;
                    

                }
            }
            return CalculateTheProbability(formulaExtras);
        }
        
    
        
        public static string CalculateTheProbability(string formulaNCR)
        {
            i = 0;
            if (formulaNCR.Contains('p') || formulaNCR.Contains('c'))
            {
                string[] operation = formulaNCR.Split(' ');
                foreach (var item in operation)
                {
                    if (item == "c")
                    {
                        int n = int.Parse(operation[i - 1]);
                        int r = int.Parse(operation[i + 1]);
                        result = Factorize(n) / (Factorize(r) * Factorize(n - r));
                        return CalculateTheProbability(formulaNCR = new Regex(Regex.Escape(operation[i - 1] + " c " + operation[i + 1])).Replace(formulaNCR, result.ToString(), 1));

                    }
                    if (item == "p")
                    {
                        int n = int.Parse(operation[i - 1]);
                        int r = int.Parse(operation[i + 1]);
                        result = Factorize(n) / Factorize(n - r);
                        return CalculateTheProbability(formulaNCR = new Regex(Regex.Escape(operation[i - 1] + " p " + operation[i + 1])).Replace(formulaNCR, result.ToString(), 1));

                    }
                    else
                        i += 1;
                   
                }
            }
            //return TrigonometryOperations.CalculateTrig(formulaNCR);
            return null;
        }
   
        public static BigInteger Factorize(int number)
        {
            result = 1;
            if (number == 1 || number == 0)
                return result = 1;
            
            else
            {
                for (BigInteger i = 1; i <= number; ++i)
                    result *= i;
            }
            
            return result;

        }
        */
    }
}
