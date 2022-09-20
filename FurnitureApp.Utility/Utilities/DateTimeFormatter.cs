using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Utility
{
    public class DateTimeFormatter
    {
        /// <summary>
        /// 文字列をDateTime?型に変換してを返す
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DateTime? GetDateTime(string text)
        {
            var now = DateTime.Now.Date;

            DateTime resultDate;

            text = $"{text}".ToUpper();

            if (DateTime.TryParse(text, out resultDate))
            {
                return resultDate;
            }

            switch (text?.Length)
            {
                case 1:
                case 2:
                    if (DateTime.TryParse($"{now.Year}/{now.Month}/{text}", out resultDate))
                    {
                        return resultDate;
                    }
                    return null;
                case 3:
                    if (DateTime.TryParse($"{now.Year}/{text.Substring(0, 1)}/{text.Substring(1)}", out resultDate))
                    {
                        return resultDate;
                    }
                    return null;
                case 4:
                    if (DateTime.TryParse($"{now.Year}/{text.Substring(0, 2)}/{text.Substring(2)}", out resultDate))
                    {
                        return resultDate;
                    }
                    return null;
                case 5:
                    if (DateTime.TryParse($"{now.Year.ToString().Substring(0, 3)}{text.Substring(0, 1)}/{text.Substring(1, 2)}/{text.Substring(3)}", out resultDate))
                    {
                        return resultDate;
                    }
                    return null;
                case 6:
                    if (DateTime.TryParse($"{now.Year.ToString().Substring(0, 2)}{text.Substring(0, 2)}/{text.Substring(2, 2)}/{text.Substring(4)}", out resultDate))
                    {
                        return resultDate;
                    }
                    return null;
                case 7:
                    switch (text.Substring(0, 1))
                    {
                        case "1":
                            text = $"M{text.Substring(1)}";
                            break;
                        case "2":
                            text = $"T{text.Substring(1)}";
                            break;
                        case "3":
                            text = $"S{text.Substring(1)}";
                            break;
                        case "4":
                            text = $"H{text.Substring(1)}";
                            break;
                        case "5":
                            text = $"R{text.Substring(1)}";
                            break;
                    }

                    if (DateTime.TryParse($"{text.Substring(0, 3)}/{text.Substring(3, 2)}/{text.Substring(5)}", out resultDate))
                    {
                        return resultDate;
                    }
                    return null;
                case 8:
                    if (DateTime.TryParse($"{text.Substring(0, 4)}/{text.Substring(4, 2)}/{text.Substring(6)}", out resultDate))
                    {
                        return resultDate;
                    }
                    return null;
                case 10:
                    if (DateTime.TryParse($"{text}", out resultDate))
                    {
                        return resultDate;
                    }
                    return null;
            }
            return null;
        }

        /// <summary>
        /// DateTime?型を和暦文字列に変換して返す
        /// ex : 令和1年1月1日
        /// </summary>
        /// <param name="nullableDate"></param>
        /// <returns></returns>
        public static string GetWareki(DateTime? nullableDate)
        {
            try
            {
                if (nullableDate == null) { return null; }

                var date = (DateTime)nullableDate;

                var meiji_s = new DateTime(1873, 1, 1);
                var taisho_s = new DateTime(1912, 7, 30);
                var showa_s = new DateTime(1926, 12, 25);
                var heisei_s = new DateTime(1989, 1, 8);
                var reiwa_s = new DateTime(2019, 5, 1);

                if (date.CompareTo(reiwa_s) >= 0)
                {
                    return $"令和{date.Year - 2018}年{date.Month}月{date.Day}日";
                }
                else if (date.CompareTo(heisei_s) >= 0)
                {
                    return $"平成{date.Year - 1988}年{date.Month}月{date.Day}日";
                }
                else if (date.CompareTo(showa_s) >= 0)
                {
                    return $"昭和{date.Year - 1925}年{date.Month}月{date.Day}日";
                }
                else if (date.CompareTo(taisho_s) >= 0)
                {
                    return $"大正{date.Year - 1911}年{date.Month}月{date.Day}日";
                }
                else if (date.CompareTo(meiji_s) >= 0)
                {
                    return $"明治{date.Year - 1868}年{date.Month}月{date.Day}日";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string GetZeroPaddingWareki(DateTime? nullableDate)
        {
            try
            {
                if (nullableDate == null) { return null; }

                var date = (DateTime)nullableDate;

                var meiji_s = new DateTime(1873, 1, 1);
                var taisho_s = new DateTime(1912, 7, 30);
                var showa_s = new DateTime(1926, 12, 25);
                var heisei_s = new DateTime(1989, 1, 8);
                var reiwa_s = new DateTime(2019, 5, 1);

                if (date.CompareTo(reiwa_s) >= 0)
                {
                    return $"令和{date.Year - 2018:00}年{date.Month:00}月{date.Day:00}日";
                }
                else if (date.CompareTo(heisei_s) >= 0)
                {
                    return $"平成{date.Year - 1988:00}年{date.Month:00}月{date.Day:00}日";
                }
                else if (date.CompareTo(showa_s) >= 0)
                {
                    return $"昭和{date.Year - 1925:00}年{date.Month:00}月{date.Day:00}日";
                }
                else if (date.CompareTo(taisho_s) >= 0)
                {
                    return $"大正{date.Year - 1911:00}年{date.Month:00}月{date.Day:00}日";
                }
                else if (date.CompareTo(meiji_s) >= 0)
                {
                    return $"明治{date.Year - 1868:00}年{date.Month:00}月{date.Day:00}日";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string GetSpacePaddingWareki(DateTime? nullableDate)
        {
            try
            {
                if (nullableDate == null) { return null; }

                var date = (DateTime)nullableDate;

                var meiji_s = new DateTime(1873, 1, 1);
                var taisho_s = new DateTime(1912, 7, 30);
                var showa_s = new DateTime(1926, 12, 25);
                var heisei_s = new DateTime(1989, 1, 8);
                var reiwa_s = new DateTime(2019, 5, 1);

                if (date.CompareTo(reiwa_s) >= 0)
                {
                    return $"令和{$"{date.Year - 2018}",2}年{$"{date.Month}",2}月{$"{date.Day}",2}日";
                }
                else if (date.CompareTo(heisei_s) >= 0)
                {
                    return $"平成{$"{date.Year - 1988}",2}年{$"{date.Month}",2}月{$"{date.Day}",2}日";
                }
                else if (date.CompareTo(showa_s) >= 0)
                {
                    return $"昭和{$"{date.Year - 1925}",2}年{$"{date.Month}",2}月{$"{date.Day}",2}日";
                }
                else if (date.CompareTo(taisho_s) >= 0)
                {
                    return $"大正{$"{date.Year - 1911}",2}年{$"{date.Month}",2}月{$"{date.Day}",2}日";
                }
                else if (date.CompareTo(meiji_s) >= 0)
                {
                    return $"明治{$"{date.Year - 1868}",2}年{$"{date.Month}", 2}月{$"{date.Day}", 2}日";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
