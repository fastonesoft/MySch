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
    public class ImageController : RoleController
    {

        //图文图片
        public void Code(string id)
        {
            XingCode.CodeOutputStream(360, 150, id, 0, BarcodeFormat.QR_CODE);
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
    }
}