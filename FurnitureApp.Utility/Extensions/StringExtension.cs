using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Utility
{
    public static class StringExtension
    {
        public static string SubstringEx(this string text, int startIndex)
        {
            text = $"{text}";

            if (text.Length <= startIndex) { return ""; }

            return text.Substring(startIndex);
            
        }
        public static string SubstringEx(this string text, int startIndex, int length)
        {
            text = $"{text}";

            if (text.Length <= startIndex) { return ""; }

            return text.PadRight(startIndex + length + 1, ' ').Substring(startIndex, length).TrimEnd();
        }
    }
}
