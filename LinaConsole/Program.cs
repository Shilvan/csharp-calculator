using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;


namespace LinaConsole
{

    class Program
    {

        public static string input;
          public static double answer;

        static void Main(string[] args)
        {
            Console.Title = "LINA CALCULATOR";
            answer = 0;
            List<string> simultaneousEquation = new List<string>();
            do
            {
                try
                {
                    string inputChecker = Console.ReadLine();

                    if (string.IsNullOrEmpty(inputChecker) && !string.IsNullOrEmpty(input))
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Clear.ConsoleLine();
                        Console.WriteLine(input.Replace(',','.'));
                    }
                    else
                    {
                        input = inputChecker;
                    }
                    

                    if (input == "clear") throw new Exception();

                    Console.ForegroundColor = ConsoleColor.Cyan;

                    if (input == "fr" | input == "fraction")
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Clear.ConsoleLine();
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Clear.ConsoleLine();
                        Console.WriteLine( "=" + DecimalToFraction.ConvertToFraction(Convert.ToDecimal(answer)) ); 
                    }

                    else if (Regex.Match(input,@";$").Success)
                    {
                        input = DepurateEquation.NonrepeatedOccurences(input);

                        simultaneousEquation.Add(input);
                        
                    }
                    else if (input.Contains("=") && simultaneousEquation.Count !=0)
                    {
                        input = DepurateEquation.NonrepeatedOccurences(input);
                        simultaneousEquation.Add(input);
                        SimultaneousEquation.Main(simultaneousEquation);
                        simultaneousEquation.Clear();
                    }
                    else if (input.Contains("="))
                    {
                        input = DepurateEquation.NonrepeatedOccurences(input);
                        Equation.Depurate(input);
                    }
                    else
                    {
                        string depuratedInput = Depurate.NonRepeatedOccurences(input);
                        answer =double.TryParse(depuratedInput, out double result)? double.Parse(depuratedInput) : ComandoCentral.ManageBrackets(depuratedInput);
                        
                        if (double.IsInfinity(answer)) throw new ArgumentException("Result Close To Infinity");
                       
                        answer = Double.Accuracy(answer);
                        Console.WriteLine($"={answer.ToString().Replace(',','.')}");
                    }

                    Console.ResetColor();
                }
                catch (Exception ex)
                {

                    if (input == "clear")
                    {
                        Console.Clear();
                        Console.Beep(220, 400);

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: " + ex.Message);

                        Console.Beep(920, 500);

                        Console.ResetColor();
                    }
                }
                
            } while (true);
            
        }

    }
   
}
