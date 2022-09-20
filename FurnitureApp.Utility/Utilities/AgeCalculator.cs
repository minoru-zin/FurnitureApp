using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Utility
{
    public class AgeCalculator
    {
        /// <summary>
        /// 年齢を返す
        /// </summary>
        /// <param name="birthDate"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static int? GetAge(DateTime? birthDate, DateTime? startDate)
        {
            try
            {
                if (birthDate == null || startDate == null) { return null; }

                var bdInt = int.Parse(birthDate?.ToString("yyyyMMdd"));
                var sdInt = int.Parse(startDate?.ToString("yyyyMMdd"));

                return (sdInt - bdInt) / 10000;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
