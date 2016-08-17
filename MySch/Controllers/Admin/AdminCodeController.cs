using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing;
using ZXing.QrCode;

namespace MySch.Controllers.Admin
{
    public class AdminCodeController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        public void GetCode(string id)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 360,
                Height = 200
            };
            ZXing.BarcodeWriter xing = new ZXing.BarcodeWriter();
            xing.Format = BarcodeFormat.CODE_128;
            xing.Options = options;

            //画条码
            Bitmap bitmap = xing.Write(id);

            //输出
            Response.ContentType = "image/jpeg";
            bitmap.Save(Response.OutputStream, ImageFormat.Bmp);
        }
    }
}