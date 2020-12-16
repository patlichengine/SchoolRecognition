using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Helpers
{
    public static class StringExtensions
    {

        public static string MakeAlphaNumeric(this string input, params char[] exceptions)
        {
            var charArray = input.ToCharArray();
            var alphaNumeric = Array.FindAll<char>(charArray, (c => char.IsLetterOrDigit(c) || exceptions?.Contains(c) == true));
            return new string(alphaNumeric);
        }

        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

    }
}
