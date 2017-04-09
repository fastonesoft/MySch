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
        public static Bitmap CodeBitmap(int width, int height, string content, int margin, BarcodeFormat format)
        {
            try
            {
                QrCodeEncodingOptions options = new QrCodeEncodingOptions
                {
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = width,
                    Height = height,
                    Margin = margin,
                    PureBarcode = true,
                };
                ZXing.BarcodeWriter xing = new ZXing.BarcodeWriter();
                xing.Format = format;
                xing.Options = options;

                Bitmap bitmap = xing.Write(content);

                return bitmap;
            }
            catch
            {
                return null;
            }
        }

        public static void CodeOutputStream(int width, int height, string content, int margin, BarcodeFormat format)
        {
            try
            {
                Bitmap bitmap = CodeBitmap(width, height, content, margin, format);

                HttpContext.Current.Response.ContentType = "image/jpeg";
                bitmap.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
            }
            catch
            {

            }
        }

        public static void CodeOutputStream(string fileName)
        {
            try
            {
                Bitmap bitmap = new Bitmap(HttpContext.Current.Server.MapPath(fileName));

                HttpContext.Current.Response.ContentType = "image/jpeg";
                bitmap.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
            }
            catch
            {

            }
        }

        public static byte[] CodeBuffer(int width, int height, string content)
        {
            try
            {
                Bitmap bitmap = CodeBitmap(width, height, content, 0, BarcodeFormat.QR_CODE);

                MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Jpeg);

                return ms.GetBuffer();
            }
            catch
            {
                return null;
            }
        }
    }
}