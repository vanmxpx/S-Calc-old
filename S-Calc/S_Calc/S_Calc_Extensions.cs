using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Calc
{
    static class S_Calc_Extensions
    {
        internal static string ReplaceAllIncorrectChars(this string s)
        {
            return s.Replace(" ", String.Empty).Replace("×", "*").Replace("√", "sqrt");
        }
    }
}
