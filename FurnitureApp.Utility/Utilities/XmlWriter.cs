using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Utility
{
    public class XmlWriter
    {
        /// <summary>
        /// クラスをXMLファイルに変換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="fileName"></param>
        public static void WriteXml<T>(T x, string fileName, string characterCode = "shift_jis")
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var sw = new System.IO.StreamWriter(fileName, false, Encoding.GetEncoding(characterCode)))
            {
                serializer.Serialize(sw, x);
            }
        }
    }
}
