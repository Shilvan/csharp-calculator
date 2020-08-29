using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinaConsole
{
    public class ComandoCentral
    {

        public static double ManageBrackets(string formulaWithBrackets)
        {
            if (double.TryParse(formulaWithBrackets, out double parsed)) return double.Parse(formulaWithBrackets);

            /*==============================================this is the real thing==========================================================
            Regex regex = new Regex(@"\(([^()]+)\)");

            foreach (Match match in regex.Matches(formulaWithBrackets))
            {
                if (!match.Value.Contains(';'))
                {
                    var insideTheBracket = match.Groups[1].Value.Split(' ').ToList();
                    double result = FunctionsOrder.FunctionsAttachedToNumber(insideTheBracket);


                    int expIndex = 0;
                    expIndex = Regex.Match(formulaWithBrackets, @"\(-.+\)\s(\^)\s").Groups[1].Index;// (-2)^2
                    if (expIndex != 0) formulaWithBrackets = formulaWithBrackets.Insert(expIndex, "$");

                    formulaWithBrackets = formulaWithBrackets.Replace(match.Value, result.ToString());


                    formulaWithBrackets = formulaWithBrackets.Replace('|' + result.ToString() + '|', Convert.ToString(Math.Abs(result)));//absolute


                    formulaWithBrackets = formulaWithBrackets.Replace("+-", "-").Replace("--", "+");//replace if -(-4) or +(-4)
                    
                }

                return ManageBrackets(formulaWithBrackets);

            }
            */

            if (Regex.Match(formulaWithBrackets, @"[(]([^()]+)[)]").Success)
            {
                string formulaInsideTheBracket = Regex.Match(formulaWithBrackets, @"[(]([^()]+)[)]").Groups[1].Value;

                //if (!formulaInsideTheBracket.Contains(';'))
                double result =prueba(formulaInsideTheBracket);
                formulaWithBrackets = formulaWithBrackets.Replace('('+formulaInsideTheBracket + ')', '(' + result.ToString() + ')');

                //problems with (-2)^2
                int expIndex = 0;
                expIndex = Regex.Match(formulaWithBrackets, @"[(]-.+[)]\s([\^])\s(.+)([\s)]|$)").Groups[1].Index;// (-2)^2
                if (expIndex != 0)formulaWithBrackets = formulaWithBrackets.Insert(expIndex, "$");

                formulaWithBrackets = formulaWithBrackets.Replace('(' + Convert.ToString(result) + ')', Convert.ToString(result));
                //find the absolute value
                if (formulaWithBrackets.Contains('|' + result.ToString() + '|')) //absolute
                    formulaWithBrackets = formulaWithBrackets.Replace('|' + result.ToString() + '|', Convert.ToString(Math.Abs(result)));

                //replace if -(-4) or +(-4)
                formulaWithBrackets = formulaWithBrackets.Replace("+-", "-").Replace("--", "+");

                return ManageBrackets(formulaWithBrackets);
            }
            
            else return prueba(formulaWithBrackets);
            
        }


        public static double prueba(string prueba)
        {
            List<string> lista = prueba.Split(' ').ToList();

            return FunctionsOrder.FunctionsAttachedToNumber(lista);

        }

        

    }


}













/*
 public class ComandoCentral
    {

        public static string ManageBrackets(string formulaWithBrackets)
        {
            if (!Regex.Match(formulaWithBrackets, @"[\s!%)]").Success) //if its a number
                return formulaWithBrackets;

            if (formulaWithBrackets.Contains('('))
            {
                string[] brackets = formulaWithBrackets.Split('(');
                foreach (var item in brackets)
                {
                    if (item.Contains(')'))
                    {
                        int index = item.IndexOf(')');
                        if (index < 0)
                            continue;
                        string InsideTheBracket = item.Substring(0, index);
                        string proto = "(" + InsideTheBracket + ')';
                        double result = 0;
                        result = double.Parse(prueba(InsideTheBracket));//double.Parse(FactorialFunctions.FactorizeAndCalculatePercentage(InsideTheBracket));

                        formulaWithBrackets = formulaWithBrackets.Replace(proto, '(' + Convert.ToString(result) + ')');
                        int expIndex = 0;
                        expIndex = Regex.Match(formulaWithBrackets, @"[(]-.+[)]\s([\^])\s(.+)([\s)]|$)").Groups[1].Index;// (-2)^2
                        if (expIndex != 0)
                            formulaWithBrackets = formulaWithBrackets.Insert(expIndex, "$");

                        formulaWithBrackets = formulaWithBrackets.Replace('(' + Convert.ToString(result) + ')', Convert.ToString(result));

                        if (formulaWithBrackets.Contains('|' + result.ToString() + '|')) //absolute
                            formulaWithBrackets = formulaWithBrackets.Replace('|' + result.ToString() + '|', Convert.ToString(Math.Abs(result)));

                        formulaWithBrackets = formulaWithBrackets.Replace("+-", "-").Replace("--", "+");
                        return ManageBrackets(formulaWithBrackets);
                    }

                }
            }
            else return prueba(formulaWithBrackets);

            //  return null;// return prueba(formulaWithBrackets).ToString();
            // return FactorialFunctions.FactorizeAndCalculatePercentage(formulaWithBrackets);


            throw new ArgumentException("Unmatched Brackets");
        }


        public static string prueba(string prueba)
        {
            List<string> lista = prueba.Split(' ').ToList();

            return FunctionsOrder.FunctionsAttachedToNumber(lista);

        }



  

    }


}
*/