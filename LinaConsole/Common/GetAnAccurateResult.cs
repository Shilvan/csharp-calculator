using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinaConsole
{
    public static class Double
    {
       
        public static double Accuracy(double number)
        {
            string input = (Convert.ToDecimal(number)).ToString();
         

            if (Regex.Match(input, @",(\d+)?(0{6}[0-4])").Success)
                input = double.Parse(input.Remove(Regex.Match(input, @",(\d+)?(0{5}[0-4])").Groups[2].Index)).ToString();

            if (Regex.Match(input, @",(\d+)?(9{6}[5-9])").Success)
            {
                input = double.Parse(input.Remove(Regex.Match(input, @",(\d+)?(9{5}[5-9])").Groups[2].Index + 1)).ToString();
                string sumando = Regex.Replace(input.Substring(0, input.Length - 1), @"\d", "0") + '1';
                input = (double.Parse(input) + double.Parse(sumando)).ToString();
            }
            return double.Parse(input);
        }
    }
}
