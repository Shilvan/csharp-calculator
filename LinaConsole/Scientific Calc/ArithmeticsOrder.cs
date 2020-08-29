using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Numerics;

namespace LinaConsole
{
    public class ArithmeticsOrder 
    {

        public static double Exponentiation(List<string> inputList)
        {
            for (byte i = 1; inputList.Contains("^") || inputList.Contains("$^"); i++)
            {
                if (inputList[i] == "^")
                    return Exponentiation(Arithmetics.CalculateAndReplace(inputList, i, double.Parse(inputList[i - 1]), '^', double.Parse(inputList[i + 1])));

                if (inputList[i] == "$^")
                    return Exponentiation(Arithmetics.CalculateAndReplace(inputList, i, double.Parse(inputList[i - 1]), '$', double.Parse(inputList[i + 1])));
            }
            return MultiplicationAndDivision(inputList);
        }


        public static double MultiplicationAndDivision(List<string> inputList)
        {
            for (byte i = 1; inputList.Contains("*") || inputList.Contains("/"); i++)
            {
                if (inputList[i] == "*")
                    return MultiplicationAndDivision(Arithmetics.CalculateAndReplace(inputList, i, double.Parse(inputList[i - 1]), '*', double.Parse(inputList[i + 1])));

                if (inputList[i] == "/")
                    return MultiplicationAndDivision(Arithmetics.CalculateAndReplace(inputList, i, double.Parse(inputList[i - 1]), '/', double.Parse(inputList[i + 1])));
            }
            return SumAndSubstraction(inputList);
        }


        public static double SumAndSubstraction(List<string> inputList)
        {
            for (byte i = 1; inputList.Contains("+") || inputList.Contains("-"); i++)
            {
                if (inputList[i] == "+")
                    return SumAndSubstraction(Arithmetics.CalculateAndReplace(inputList, i, double.Parse(inputList[i - 1]), '+', double.Parse(inputList[i + 1])));

                if (inputList[i] == "-")
                    return SumAndSubstraction(Arithmetics.CalculateAndReplace(inputList, i, double.Parse(inputList[i - 1]), '-', double.Parse(inputList[i + 1])));
            }

            if (inputList.Count != 1) throw new ArgumentException("Invalid Input");

            return double.Parse(inputList[0]);
        }

    }



}
