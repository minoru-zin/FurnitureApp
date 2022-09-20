using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Utility
{
    public class FileWriter
    {
        public static void Write(string text, string fileName, bool append, string characterCode = "shift_jis")
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var sw = new System.IO.StreamWriter(fileName, append, Encoding.GetEncoding(characterCode)))
            {
                sw.Write(text);
            }
        }
        public static void WriteLine(string text, string fileName, bool append, string characterCode = "shift_jis")
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var sw = new System.IO.StreamWriter(fileName, append, Encoding.GetEncoding(characterCode)))
            {
                sw.WriteLine(text);
            }
        }
    }
}
