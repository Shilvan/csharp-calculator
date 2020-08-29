using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinaConsole
{
    public class Depurate
    {
        public static string NonRepeatedOccurences(string formulaReplaced)
        {
            formulaReplaced = Regex.Replace(formulaReplaced, @"\s", "");
            //replace maths variables
            formulaReplaced = formulaReplaced.Replace("e", '(' + Convert.ToString(Math.E) + ')').Replace("pi", '(' + Convert.ToString(Math.PI) + ')').Replace("ans", '(' + Program.answer.ToString() + ')');
            
            //add obligatory spaces
            formulaReplaced = formulaReplaced.Replace("!", " !").Replace("%", " %").Replace("log", "log ").Replace("ln", "ln ");//.Replace("v", "v ")

            //add signs spaces
            formulaReplaced = formulaReplaced.Replace("*", " * ").Replace("/", " / ").Replace(";", " ; ").Replace('.',',');

            //Add multiplication signs
            formulaReplaced = formulaReplaced.Replace(")(", ") * (").Replace(")||(", ")| * |(");
            
            if (double.TryParse(formulaReplaced, out double result)) return formulaReplaced;
            
            return ConsecutivePlusMinusSigns(formulaReplaced);
   
        }


        public static string ConsecutivePlusMinusSigns(string formulaReplace)
        {
            if (Regex.Match(formulaReplace, @"[+-][-+]").Success) 
            {
                formulaReplace = formulaReplace.Replace("--", "+").Replace("-+", "-").Replace("+-", "-");
                formulaReplace = Regex.Replace(formulaReplace, @"([+]+)", "+");
                return ConsecutivePlusMinusSigns(formulaReplace);
            }
            return TrigonometricFunctions(formulaReplace);
            
        }

        public static string TrigonometricFunctions(string formulaCases) 
        {
            if (Regex.Match(formulaCases, @"(sin|cos|tan)h?(\^-1)?([-+\d|(])").Success)
            {
                int add = Regex.Match(formulaCases, @"(sin|cos|tan)h?(\^-1)?([-+\d|(])").Groups[3].Index;//check group index
                formulaCases = formulaCases.Insert(add, " ");
                return TrigonometricFunctions(formulaCases);

            }

            return nthRoots(formulaCases);
        }
        
     

        public static string nthRoots(string formulaReplace)
        {
            if (Regex.Match(formulaReplace, @"[\d!%)|](\^v)[-+|(\d]").Success) 
            {
                int add = Regex.Match(formulaReplace, @"[\d!%)|](\^v)[-+|(\d]").Groups[1].Index;
                formulaReplace = formulaReplace.Insert(add, " ");
                return nthRoots(formulaReplace);

            }
            return SquareRoot(formulaReplace);
        }

        public static string SquareRoot(string formulaReplace)
        {
            if (Regex.Match(formulaReplace, @"(v)[-+|(\d]").Success)
            {
                int add = Regex.Match(formulaReplace, @"(v)[-+|(\d]").Groups[1].Index;
                formulaReplace = formulaReplace.Insert(add +1, " ");
                return SquareRoot(formulaReplace);

            }
            return OperationSigns(formulaReplace);
        }



        public static string OperationSigns(string formulaReplace)
        {
            if (Regex.Match(formulaReplace, @"[\da-z|!%)|,]([-+cp])[-+,\da-z|(]").Success)
            {
                int add = Regex.Match(formulaReplace, @"[\da-z|!%)|,]([-+cp])[-+,\da-z|(]").Groups[1].Index;
                formulaReplace = formulaReplace.Insert(add, " ").Insert(add + 2, " ");
                return OperationSigns(formulaReplace);

            }
                
            return DepuratePowerSign(formulaReplace);
        }

        public static string DepuratePowerSign(string formulaReplace)
        {
            if (Regex.Match(formulaReplace, @"[\d|!%)|](\^)[-+\d|(]").Success)
            {
                int add = Regex.Match(formulaReplace, @"[\d|!%)|](\^)[-+\d|(]").Groups[1].Index;
                formulaReplace = formulaReplace.Insert(add, " ").Insert(add + 2, " ");
                return DepuratePowerSign(formulaReplace);

            }
            return AddMultiplicationSign(formulaReplace);
        }

        public static string AddMultiplicationSign(string formulaSign)
        {
            string pattern = @"[\d!%a-z][|(]|[)|][\da-z]|[\d!%][a-z]|[a-z][\d]";
            if (!Regex.Match(formulaSign, pattern).Success)
                return formulaSign;
            
            int indexShould = Regex.Match(formulaSign, pattern).Index;

            if (Regex.Match(formulaSign, pattern).Groups[1].Success)
                indexShould += 1;
            
            formulaSign = formulaSign.Insert(indexShould + 1, " * ");
            return AddMultiplicationSign(formulaSign);

        }


     
    }
}
