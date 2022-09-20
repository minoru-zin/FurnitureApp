using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Utility
{
    public class FileReader
    {
        public static string ReadToEnd(string fileName, string characterCode = "shift_jis")
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var sr = new System.IO.StreamReader(fileName, Encoding.GetEncoding(characterCode)))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
