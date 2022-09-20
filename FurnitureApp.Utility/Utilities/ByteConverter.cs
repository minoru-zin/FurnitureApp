using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Utility
{
    public class ByteConverter
    {
        /// <summary>
        /// バイト列を文字列に変換して返す
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="characterCode"></param>
        /// <returns></returns>
        public static string ConvertToString(IEnumerable<byte> bytes, string characterCode = "shift_jis")
        {
            try
            {
                return $"{Encoding.GetEncoding(characterCode).GetString(bytes.ToArray())}";
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 文字列をバイト列に変換して返す
        /// </summary>
        /// <param name="text"></param>
        /// <param name="characterCode"></param>
        /// <returns></returns>
        public static IEnumerable<byte> ConvertStringToBytes(string text, string characterCode = "shift_jis")
        {
            try
            {
                return Encoding.GetEncoding(characterCode).GetBytes(text);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 文字列のバイト数を返す
        /// nullは空文字に変換され0を返す
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int GetByteCount(string text, string characterCode= "shift_jis")
        {
            text ??= "";

            return Encoding.GetEncoding(characterCode).GetByteCount(text);
        }

        /// <summary>
        /// 指定バイト数に文字列を整形して返す
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="byteCount">戻り値のバイト数</param>
        /// <param name="isPadLeft">データ右詰め左指定文字埋め: true データ左詰め指定文字埋め: false</param>
        /// <param name="padChar">残りバイトを埋める指定文字</param>
        /// <returns></returns>
        public static string CreatePaddedString(string text, int byteCount, bool isPadLeft, char padChar, string characterCode = "shift_jis")
        {
            try
            {
                text ??= "";

                var count = GetByteCount(text, characterCode);

                if (isPadLeft)
                {
                    return $"{"".PadRight(byteCount - count, padChar)}{text}";
                }
                else
                {
                    return $"{text}{"".PadRight(byteCount - count, padChar)}";
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
