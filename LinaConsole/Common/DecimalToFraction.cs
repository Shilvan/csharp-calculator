using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinaConsole
{
    public class DecimalToFraction
    {
        
        public static string ConvertToFraction(decimal value)
        {
            //===========================================================================
            //check for constants, pi e...
            //===========================================================================
            if (!value.ToString().Contains(',')) return(value.ToString()); 
            
            for (int i = 1; i < 10000000; i++)
            {
                decimal multipliedValue = value * i;

                if (Math.Abs(Math.Round(multipliedValue) - multipliedValue) <= 0.00000001M)
                   return Math.Round(multipliedValue).ToString() + "/" + i.ToString(); 
                
                
            }

            return value.ToString();
        }
        
        
    }
}
