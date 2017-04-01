using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using ZXing;
using ZXing.QrCode;

namespace MySch.Bll.Func
{
    public class XingCode
    {
        public static Bitmap CodeBitmap(int width, int height, string content)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Height = height,
            };
            ZXing.BarcodeWriter xing = new ZXing.BarcodeWriter();
            xing.Format = BarcodeFormat.QR_CODE;
            xing.Options = options;

            Bitmap bitmap = xing.Write(content);

            return bitmap;
        }

        public static void CodeOutputStream(int width, int height, string content)
        {
            Bitmap bitmap = CodeBitmap(width, height, content);

            HttpContext.Current.Response.ContentType = "image/jpeg";
            bitmap.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
        }


        public static byte[] CodeBuffer(int width, int height, string content)
        {
            Bitmap bitmap = CodeBitmap(width, height, content);

            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Jpeg);

            return ms.GetBuffer();
        }
    }
}