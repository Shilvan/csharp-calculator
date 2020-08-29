using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinaConsole
{
    class DepurateEquation
    {
        public static string NonrepeatedOccurences(string input)
        {

            input = Regex.Replace(input, @"\s", "");
           
            input = input.Replace("=", " = ").Replace('.', ',').Replace(";","");
            if (!input.Contains('='))throw new ArgumentException("No equal sign found - Equation solver") ;
            return AddValueToLetters(input);
        }

        public static string AddValueToLetters(string input)
        {
            if (Regex.Match(input, @"(^|[-+])([a-z])").Success)
            {
                int index = Regex.Match(input, @"(^|[-+])([a-z])").Groups[2].Index;
                input = input.Insert(index, "1");
                return AddValueToLetters(input);
            }
            return ConsecutivePlusMinusSigns(input);
        }

        static string ConsecutivePlusMinusSigns(string formulaReplace)
        {
            if (Regex.Match(formulaReplace, @"[+-][-+]").Success)
            {
                formulaReplace = formulaReplace.Replace("--", "+").Replace("-+", "-").Replace("+-", "-");
                formulaReplace = Regex.Replace(formulaReplace, @"([+]+)", "+");
                return ConsecutivePlusMinusSigns(formulaReplace);
            }
            return OperationSigns(formulaReplace);

        }
        
        static string OperationSigns(string formulaReplace)
        {
            if (Regex.Match(formulaReplace, @"[\da-z,]([-+])[,\da-z]").Success)
            {
                int add = Regex.Match(formulaReplace, @"[\da-z,]([-+])[,\da-z]").Groups[1].Index;
                formulaReplace = formulaReplace.Insert(add, " ");
                return OperationSigns(formulaReplace);

            }

            return formulaReplace;
        }

    }
}
