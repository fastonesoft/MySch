using MySch.Core;
using MySch.Bll.WX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;

namespace MySch.Controllers.WX
{
    public class ImageController : RoleController
    {

        //文字转二维码
        public void Code(string id)
        {
            XingCode.CodeOutputStream(360, 150, id, 0, BarcodeFormat.QR_CODE);
        }

        //右显示
        public void Coder(string id, string title)
        {
            XingCode.CodeOutputStreamRight(360, 300, id, title, 0, BarcodeFormat.QR_CODE);
        }

        //居中显示
        public void Codem(string id, string title)
        {
            XingCode.CodeOutputStreamMid(360, 260, id, title, 0, BarcodeFormat.QR_CODE);
        }


        public void Code1(string id)
        {
            XingCode.CodeOutputStream(150, 150, id, 0, BarcodeFormat.QR_CODE);
        }

        //我的关注
        public void MyCode()
        {
            XingCode.CodeOutputStream(240, 240, "http://weixin.qq.com/r/Q3WpsR7Ej0PwrVrS9yBR", 0, BarcodeFormat.QR_CODE);
        }

        //我的图片
        public void Image(string id)
        {
            var fileName = "~/Images/" + id + ".jpg";
            XingCode.CodeOutputStream(fileName);
        }

        //图文缩略图
        public void Nail(string id)
        {
            WXImage.CutForSquare(id, 200);
        }

        //图片浏览
        public void Uploaded(string id)
        {
            var fileName = "~/Upload/XueImages/" + id + ".jpg";
            XingCode.CodeOutputStream(fileName);
        }

        //图片裁剪
        public void Cut(string id)
        {
            CutToSize(id, 90, 120);
        }

        public void CutToSize(string id , int width, int height)
        {
            var fileName = "~/Upload/XueImages/" + id + ".jpg";
            var bitmap = Bitmap.FromFile(Server.MapPath(fileName));

            using (var output = ImageDo.CutForCustom(bitmap, width, height, true))
            {
                Response.ContentType = "image/jpeg";
                output.Save(Response.OutputStream, ImageFormat.Jpeg);
            }
        }

    }
}