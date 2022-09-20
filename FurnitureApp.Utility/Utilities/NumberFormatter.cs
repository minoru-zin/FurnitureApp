using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Utility
{
    public class NumberFormatter
    {
        /// <summary>
        /// 1, 001などはint型で返す
        /// それ以外はnullで返す
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int? GetNullInt(string text)
        {
            if(int.TryParse($"{text}", out var number))
            {
                return number;
            }

            return null;
        }

        public static double? GetNullDouble(string text)
        {
            if (double.TryParse($"{text}", out var number))
            {
                return number;
            }

            return null;
        }
    }

}
