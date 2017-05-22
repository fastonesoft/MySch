using MySch.Bll.Func;
using MySch.Bll.WX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing;

namespace MySch.Controllers.WX
{
    public class ImageController : Controller
    {

        //图文图片
        public void Code(string content)
        {
            XingCode.CodeOutputStream(260, 40, content, 0, BarcodeFormat.CODE_128);
        }

        //我的关注
        public void MyCode()
        {
            XingCode.CodeOutputStream(240, 240, "http://weixin.qq.com/r/Q3WpsR7Ej0PwrVrS9yBR", 0, BarcodeFormat.QR_CODE);
        }

        //我的图片
        public void Image(string name)
        {
            var fileName = "~/Images/" + name + ".jpg";
            XingCode.CodeOutputStream(fileName);
        }

        //图文缩略图
        public void Nail(string name)
        {
            WXImage.CutForSquare(name, 200);
        }

        //图片浏览
        public void Uploaded(string name)
        {
            var fileName = "~/Upload/XueImages/" + name + ".jpg";
            XingCode.CodeOutputStream(fileName);
        }
    }
}