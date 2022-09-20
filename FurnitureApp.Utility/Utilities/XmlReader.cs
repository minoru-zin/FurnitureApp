using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FurnitureApp.Utility
{
    public class XmlReader
    {
        /// <summary>
        /// XMLファイルをクラスのインスタンスに変換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T ReadXml<T>(string fileName, string characterCode = "shift_jis")
        {
            if (!File.Exists(fileName))
            {
                return default(T);
            }
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var sr = new System.IO.StreamReader(fileName, Encoding.GetEncoding(characterCode)))
            {
                return (T)serializer.Deserialize(sr);
            }
        }
    }
}
