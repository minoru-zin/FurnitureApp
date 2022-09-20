using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace FurnitureApp.Utility
{
    public class PdfFormatter
    {
        /// <summary>
        /// PDFから画像イメージを生成する
        /// </summary>
        /// <param name = "pdfPath" ></ param >
        /// < param name="width"></param>
        /// <param name = "height" ></ param >
        /// < param name="dpiX"></param>
        /// <param name = "dpiY" ></ param >
        /// < returns ></ returns >

        public static BitmapFrame GetImageFromPdf(string pdfPath, int width, int height, float dpiX, float dpiY)
        {
            using (PdfDocument pdfDoc = PdfDocument.Load(pdfPath))
            using (Image img = pdfDoc.Render(0, width, height, dpiX, dpiY, false))
            {
                using (Stream stream = new MemoryStream())
                {
                    img.Save(stream, ImageFormat.Png);
                    stream.Seek(0, SeekOrigin.Begin);
                    return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }
            }
        }
        public static BitmapFrame GetImageFromPdf(string pdfPath, float dpiX, float dpiY)
        {
            using (PdfDocument pdfDoc = PdfDocument.Load(pdfPath))
            using (Image img = pdfDoc.Render(0, dpiX, dpiY, false))
            {
                using (Stream stream = new MemoryStream())
                {
                    img.Save(stream, ImageFormat.Png);
                    stream.Seek(0, SeekOrigin.Begin);
                    return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }
            }
        }
    }
}
