using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinaConsole
{
    class SimultaneousEquation
    {

        public static void Main(List<string> equations)
        {

            var sort = new List<Dictionary<string, double>>();

            var line0 = SaveTheVariables(equations[0]);
            var line1 = SaveTheVariables(equations[1]);
            var line2 = new Dictionary<string, double>();
            if (equations.Count > 2)
            {
                line2 = SaveTheVariables(equations[2]);

                sort.Add(line0);
                sort.Add(line1);
                sort.Add(line2);
                
            }
            else
            {
                sort.Add(line0);
                sort.Add(line1);
            }
            
            var sorted = SortfromSmallerToBigger(sort);

            var answer = new Dictionary<string, string>();

            if (sorted.Count==2)answer = TwoVariables(sorted[0], sorted[1]);

            else if (sorted.Count == 3)answer = ThreeVariables(sorted[0], sorted[1], sorted[2]);
            
            else throw new ArgumentException();
           


            foreach (var item in answer)
            {
                Console.WriteLine($"{item.Key}={item.Value.ToString().Replace(',','.')}");
            }
            
        }

        static Dictionary<string, string> TwoVariables(Dictionary<string, double> line0, Dictionary<string, double> line1)
        {
            if (line0.Count > 3 | line1.Count > 3) throw new ArgumentException("There are more variables than equations");
           
            string x = line0.ElementAt(0).Key;
            string y = line1.ElementAt(0).Key == x ? line1.ElementAt(1).Key : line1.ElementAt(0).Key;

            var answer = new Dictionary<string, string>();

            double xVal = SolveToTwoVariables(line0, x, line1, y);
            xVal = Double.Accuracy(xVal);
            answer.Add(x, xVal.ToString());


            double yVal = SolveTheSecondVariable(line1, y, x, xVal);
            yVal = Double.Accuracy(yVal);
            answer.Add(y, yVal.ToString());
            
            return answer;
        }
        static double SolveTheSecondVariable(Dictionary<string, double> line1, string y, string x, double xVal)
        {
            if (line1.Count == 2) return -line1["CATALUÑA.NUMBER"] / line1[y];
            double number = (-line1["CATALUÑA.NUMBER"]) + (-line1[x] * xVal);
            return number / line1[y];
        }

        static Dictionary<string, string> ThreeVariables(Dictionary<string, double> line0, Dictionary<string, double> line1, Dictionary<string, double> line2)
        {

            string x = line0.ElementAt(0).Key;
            string y = line1.ElementAt(0).Key == x? line1.ElementAt(1).Key : line1.ElementAt(0).Key;
          
            string z = null;
            

            int i= 0;
            foreach (var item in line2)
            {
                if (item.Key != x && item.Key != y && item.Key != "CATALUÑA.NUMBER")
                {
                    i += 1;
                    if (i == 2) throw new ArgumentException("There are more variables than equations");
                    z = item.Key;
                }
            }
            if (z == null) return TwoVariables(line0, line1);

            var answer = new Dictionary<string, string>();
            if (line1.Count==4 & line0.Count !=4)
            {
                string thirdVar = line0.Count == 3 && line0.ElementAt(1).Key == z ? y : z;
                string secondVar = thirdVar == z ? y : z;

                var newLine0 = SimplifyEquation(line1, x, line2, secondVar, thirdVar);
                
                answer = TwoVariables(line0, newLine0);
                answer.Add(thirdVar, Double.Accuracy(SolveTheThirdVariable(line2, thirdVar, x, double.Parse(answer[x]), secondVar, double.Parse(answer[secondVar]))).ToString());
                return answer;
            }
          

            int num = 0;
            if (line0.Count <4  & line1.Count <4)
            {
                if (!line1.ContainsKey(x)) num += 1;
                if (line1.ContainsKey(z) && !line0.ContainsKey(z)) num += 1;
                if (!line1.ContainsKey(z) && line0.ContainsKey(z)) num += 1;

            }

            if (num > 0)
            {

                string VarToEliminate = line1.ContainsKey(z) ? x : z;
                string variable =VarToEliminate== x? y: x; 

             
                var newLine = Replace(line0,VarToEliminate , line2, variable );

               
                answer = newLine.Count < line1.Count? TwoVariables(newLine ,line1) : TwoVariables(line1, newLine);
                double thirdValue = line0.Count ==2? -line0["CATALUÑA.NUMBER"]/line0[VarToEliminate]:((line0[variable]* -double.Parse(answer[variable]))+ -line0["CATALUÑA.NUMBER"])/line0[VarToEliminate];

                answer.Add(VarToEliminate,Double.Accuracy(thirdValue).ToString());
                return answer;
            }
          
            else
            {
                var newLine0 = SimplifyEquation(line0, x, line1, y, z);
                var newLine1 = SimplifyEquation(line1, x, line2, y, z);
                answer = TwoVariables(newLine0, newLine1);
                double thirdValue =SolveTheThirdVariable(line2, z, answer.ElementAt(0).Key, double.Parse(answer.ElementAt(0).Value), answer.ElementAt(1).Key, double.Parse(answer.ElementAt(1).Value));
                answer.Add(z, Double.Accuracy(thirdValue).ToString());
            }

            
            return answer;
        }

        static double SolveTheThirdVariable(Dictionary<string, double> lineToReplace, string varToFind, string var0, double var0VAl, string var1, double var1Val)
        {
            if (lineToReplace.Count == 2) return -lineToReplace["CATALUÑA.NUMBER"] / lineToReplace[varToFind];
            double number = (-lineToReplace["CATALUÑA.NUMBER"]) + (-lineToReplace[var0] * var0VAl) + (-lineToReplace[var1] * var1Val);

            return number / lineToReplace[varToFind];
        }

      
        static List<Dictionary<string, double>> SortfromSmallerToBigger(List<Dictionary<string, double>> outPut )
        {
            for (int i = 0; i < outPut.Count -1; i++)
            {
                if (outPut.ElementAt(i + 1).Count < outPut.ElementAt(i).Count)
                {
                    var item = outPut.ElementAt(i + 1);
                    outPut.RemoveAt(i + 1);
                    outPut.Insert(i, item);

                }
            }
           
            if (outPut.ElementAt(0).Count > outPut.ElementAt(1).Count) return SortfromSmallerToBigger(outPut);
               
            return outPut;
        }

        static Dictionary<string, double> SimplifyEquation(Dictionary<string, double> lineToReplace, string var0, Dictionary<string, double> resultantLine, string var1, string varToEliminate)
        {
            if (lineToReplace.Count < 4) return lineToReplace;
            var result = new Dictionary<string, double>();

            double factor = -resultantLine[varToEliminate] / lineToReplace[varToEliminate];
            
            result.Add(var0, resultantLine[var0] + (lineToReplace[var0] * factor));
            if (result[var0]==0) result.Remove(var0);

            result.Add(var1, resultantLine[var1] + (lineToReplace[var1] * factor));
            if (result[var1] == 0) result.Remove(var1);

            result.Add("CATALUÑA.NUMBER", resultantLine["CATALUÑA.NUMBER"] + (lineToReplace["CATALUÑA.NUMBER"] * factor));
            return result;
          
        }

        static Dictionary<string, double> SaveTheVariables(string equation)
        {
            
            int index = equation.IndexOf("=");
            if (index < 0) throw new ArgumentException("No equal sign found - Simultaneous Equation");

            Dictionary<string, double> outPut = new Dictionary<string, double>();

            Regex regex = new Regex(@"([-+]?[\d,]+)([a-z]+)");
            foreach (Match match in regex.Matches(equation))
            {
                double value =(match.Index > index)? -double.Parse(match.Groups[1].Value): double.Parse(match.Groups[1].Value);
                string key = match.Groups[2].Value;

                if (outPut.ContainsKey(key)) outPut[key] += value;
                else outPut.Add(key, value);

                if (outPut[key] == 0)outPut.Remove(key);
              
            }

            Regex regexi = new Regex(@"(^|\s)([-+]?[\d,]+)(\s|$)");
            foreach (Match match in regexi.Matches(equation))
            {
                double value = (match.Index > index) ? -double.Parse(match.Groups[2].Value) : double.Parse(match.Groups[2].Value);
               
                if (outPut.ContainsKey("CATALUÑA.NUMBER")) outPut["CATALUÑA.NUMBER"] += value;
                else outPut.Add("CATALUÑA.NUMBER", value);
            }

            if (outPut.Count < 2) throw new ArgumentException("Not enough variables in the equation - Simultaneous Equation");
            return outPut;

        }
        
        static double SolveToTwoVariables(Dictionary<string, double> lineToRemove, string resultantVar, Dictionary<string, double> resultantLine, string varToRemove )
        {
            if (lineToRemove.Count == 2) return -lineToRemove["CATALUÑA.NUMBER"]/ lineToRemove[resultantVar];

            double factor = -resultantLine[varToRemove]/lineToRemove[varToRemove];
            double dividend = (lineToRemove[resultantVar] * factor) + resultantLine[resultantVar];
            double number = (-lineToRemove["CATALUÑA.NUMBER"] * factor) + -resultantLine["CATALUÑA.NUMBER"];
            return number / dividend;
        }

        static Dictionary<string, double> Replace(Dictionary<string, double> lineToReplace, string varToRemove, Dictionary<string, double> resultantLine,  string variable)
        {
            var result = new Dictionary<string, double>();
            if (lineToReplace.Count == 2)
            {
                result = resultantLine;
                result.Remove(varToRemove);
                result["CATALUÑA.NUMBER"]+=resultantLine["CATALUÑA.NUMBER"] + (-lineToReplace["CATALUÑA.NUMBER"] / lineToReplace[varToRemove]);
                return result;
            }
            else
            {

                double yPotoVal = -lineToReplace[variable] / lineToReplace[varToRemove];
                double catNumber = -lineToReplace["CATALUÑA.NUMBER"] / lineToReplace[varToRemove];
                
                result.Add(variable, resultantLine[variable] + yPotoVal);
                if (resultantLine[variable] == 0) resultantLine.Remove(variable);
               
                result.Add("CATALUÑA.NUMBER",resultantLine["CATALUÑA.NUMBER"] + catNumber);

                return result;
            }
        }
    }
       
    }
