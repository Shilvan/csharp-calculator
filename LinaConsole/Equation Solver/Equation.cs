using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Windows;
using System.Numerics;

namespace LinaConsole
{

    partial class Equation
    {

        static string Real = "(?<!([E][+-][0-9]+))([-]?\\d+\\.?\\d*([E][+-]" + "[0-9]+)?(?!([i0-9.E]))|[-]?\\d*\\.?\\d+([E][+-][0-9]+)?)(?![i0-9.E])";
        static string Img = "(?<!([E][+-][0-9]+))([-]?\\d+\\.?\\d*([E][+-]" + "[0-9]+)?(?![0-9.E])(?:i)|([-]?\\d*\\.?\\d+)?([E][+-][0-9]+)?\\s*(?:i)(?![0-9.E]))";

        

        public static void Depurate(string input)
        {
            int index = input.IndexOf("=");

            Regex regex = new Regex(@"[a-z]\^(\d+)[^\d]");

            foreach (Match match in regex.Matches(input))
            {
                if (Convert.ToInt32(match.Groups[1].Value) >10)
                {
                    throw new ArgumentException($"The variable exponet is out of range - variable^{match.Groups[1].Value}");
                }
                
            }
            
            string output = "";
            for (int i = 10; i != 1; i--)
            {
                string pattern = @"([+-]?[\d,]+)([a-z]+)\^"+i+"( |$)";
                string match = Regex.Match(input, pattern).Groups[1].Value;
                if (match == "" & output == "") { }
                else
                {
                    if(match != "")if (Regex.Match(input, pattern).Index > index) match = Convert.ToString(-double.Parse(match));
                    
                    output += (match == "" && output != "") ? ",0" : ',' + match.Replace(',','.');
                }
             
            }
            

            string x = Regex.Match(input, @"([+-]?[\d,]+)([a-z]+)(\^1)?(\s|$)").Groups[1].Value;

            if (x == "" & output == "") throw new ArgumentException("No variable found - Equations solver");

            if (x != "") if (Regex.Match(input, @"([+-]?[\d,]+)([a-z]+)(\^1)?(\s|$)").Index > index) x = Convert.ToString(-double.Parse(x));
            output += x == "" ? ",0" : ',' + x.Replace(',','.');

            
            string cataluñaNumber = Regex.Match(input, @"(^|\s)([+-]?[\d,]+)(\s|$)").Groups[2].Value;

            if (Regex.Match(input, @"(^|\s)([+-]?[\d,]+)(\s|$)").Index > index) cataluñaNumber = Convert.ToString(-double.Parse(cataluñaNumber));
            output += (cataluñaNumber =="")? ",0": ',' + cataluñaNumber.Replace(',', '.');

            if (output.Substring(0, 1) == ",") output = output.Remove(0, 1);

            string variable = Regex.Match(input, @"[\d]([a-z]+)(\s|\^)").Groups[1].Value;
            
           Solve(output, variable);
        }

        
        public static void Solve(string input, string variable) 
        {
            List<Complex> res = new List<Complex>();

            string[] arrString = input.Split(new char[] { ',' });
            Complex[] ComplexCoefficients = new Complex[arrString.Length];

            double[] RealCoefficients = new double[arrString.Length];
            int i = 0;

            List<Complex> lst = new List<Complex>();

            bool boolReal = new bool();
            boolReal = true;

            while (i < arrString.Length)
            {
                Complex Number = new Complex();
                Number = GenerateComplexNumberFromString(arrString[i]);
                ComplexCoefficients[i] = Number;
                RealCoefficients[i] = Number.Real;

                if (Number.Imaginary != 0)
                    boolReal = false;

                i += 1;
            }

            try
            {
                if (boolReal)
                {
                    res = RealPolynomialRootFinder.FindRoots(RealCoefficients);
                }
                else
                {
                    res = ComplexPolynomialRootFinder.FindRoots(ComplexCoefficients);
                }

                string answer = "";
                foreach (Complex p in res)
                {
                    string protoAnswer = string.Format(new ComplexFormatter(), "{0:I0}", p);
                    if (protoAnswer == answer) { }

                    else
                    {

                        answer = protoAnswer;

                        Console.WriteLine($"{variable}={answer}");
                    }
                }

            }
            catch //(Exception)
            {
                throw new ArgumentException();//just throw a new exception

            }
        }




        private static Complex GenerateComplexNumberFromString(string input)
        {
            input = input.Replace(" ", "");

            string Number = "((?<Real>(" + Real + "))|(?<Imag>(" + Img + ")))";
            double Re = 0;
            double Im = 0;
            Re = 0;
            Im = 0;


            foreach (Match Match in Regex.Matches(input, Number))
            {

                if (!(Match.Groups["Real"].Value == string.Empty))
                {
                    Re = double.Parse(Match.Groups["Real"].Value, CultureInfo.InvariantCulture);
                }


                if (!(Match.Groups["Imag"].Value == string.Empty))
                {
                    if (Match.Groups["Imag"].Value.ToString().Replace(" ", "") == "-i")
                    {
                        Im = double.Parse("-1", CultureInfo.InvariantCulture);
                    }
                    else if (Match.Groups["Imag"].Value.ToString().Replace(" ", "") == "i")
                    {
                        Im = double.Parse("1", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Im = double.Parse(Match.Groups["Imag"].Value.ToString().Replace("i", ""), CultureInfo.InvariantCulture);
                    }
                }
            }

            Complex result = new Complex(Re, Im);
            return result;
        }


    }


    public class ComplexFormatter : IFormatProvider, ICustomFormatter
    {

        public object GetFormat(Type formatType)
        {
            if (object.ReferenceEquals(formatType, typeof(ICustomFormatter)))
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        public string Format(string fmt, object arg, IFormatProvider provider)
        {

            if (arg is Complex)
            {
                Complex c1 = (Complex)arg;
                
                string realNumber = Double.Accuracy(c1.Real).ToString();
                string imaginaryNumber = Double.Accuracy(c1.Imaginary).ToString();


                // Check if the format string has a precision specifier.
                int precision = 0;
                string fmtString = string.Empty;
                if (fmt.Length > 1)
                {
                    try
                    {
                        precision = Int32.Parse(fmt.Substring(1));
                    }
                    catch //(FormatException e)
                    {
                        precision = 0;
                    }
                    fmtString = "N" + precision.ToString();
                }

                if (fmt.Substring(0, 1).Equals("I", StringComparison.OrdinalIgnoreCase))
                {
                    string s = "";
                    if (imaginaryNumber == "0" & realNumber == "0")
                    {
                        s = "0";
                    }
                    else if (imaginaryNumber == "0")
                    {
                        s = realNumber;
                    }
                    else if (realNumber == "0")
                    {
                        s = imaginaryNumber + "i";
                    }
                    else
                    {
                        if (double.Parse(imaginaryNumber) >= 0)
                        {

                            s = string.Format($"{realNumber}+{imaginaryNumber}i");
                        }
                        else
                        {
                            
                            s = string.Format($"{realNumber}-{Math.Abs(double.Parse(imaginaryNumber)).ToString()}i");
                        }
                    }

                    if (s == "1i")  return "i";
                   
                    else return s.Replace(",", ".").Replace("+1i", "+i").Replace("-1i", "-i");
                  
                }
                else if (fmt.Substring(0, 1).Equals("J", StringComparison.OrdinalIgnoreCase))
                {
                    string imaginaryCoefficient = c1.Imaginary.ToString(fmtString)=="1"? "" : c1.Imaginary.ToString(fmtString);

                    return realNumber + " + " + imaginaryCoefficient + "j"; 
                }
                else
                {
                    return c1.ToString(fmt, provider); 
                }
            }
            else
            {
                if (arg is IFormattable)
                {
                    return ((IFormattable)arg).ToString(fmt, provider);
                }
                else if (arg != null)
                {
                    return arg.ToString();
                }
                else
                {
                    return string.Empty;   
                }
            }
        }
    }


}

