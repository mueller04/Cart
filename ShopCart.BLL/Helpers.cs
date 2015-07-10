using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShopCart.BLL
{
    public static class Helpers
    {
        public static string GetStringNameOnly(string inputStr)
        {
            var regex = new Regex("\\d");
            var NameOnlystr = regex.Replace(inputStr, String.Empty);
            char[] charsToTrim = { ':', ' ', '$' };
            NameOnlystr = NameOnlystr.TrimEnd(charsToTrim);
            return NameOnlystr;
        }

    }
}
