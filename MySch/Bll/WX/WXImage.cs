using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX
{
    public class WXImage
    {
        public static void CutForSquare(string imageID,  int side)
        {
            string imageName = HttpContext.Current.Server.MapPath(string.Format("~/Upload/XueImages/{0}.jpg", imageID));
            Image initImage = Image.FromFile(imageName); 

            //原始图片的宽、高
            int initWidth = initImage.Width;
            int initHeight = initImage.Height;

            //非正方型先裁剪为正方型
            if (initWidth != initHeight)
            {
                //截图对象
                Rectangle fromR = initWidth > initHeight ? new Rectangle((initWidth - initHeight) / 2, 0, initHeight, initHeight) : new Rectangle(0, (initHeight - initWidth) / 2, initWidth, initWidth);
                Rectangle toR = initWidth > initHeight ? new Rectangle(0, 0, initHeight, initHeight) : new Rectangle(0, 0, initWidth, initWidth);
                Image pickedImage = ImageFromSource(initWidth, initHeight, initImage, fromR, toR);
                if (initWidth > initHeight)
                {
                    initWidth = initHeight;
                }
                else
                {
                    initHeight = initWidth;
                }

                //将截图对象赋给原图
                initImage = (Image)pickedImage.Clone();
                //释放截图资源
                pickedImage.Dispose();
            }

            //创建缩略图
            Image resultImage = ImageFromSource(side, side, initImage, new Rectangle(0, 0, initWidth, initHeight), new Rectangle(0, 0, side, side));
            //保存缩略图
            HttpContext.Current.Response.ContentType = "image/jpeg";
            resultImage.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
            //释放缩略图资源
            resultImage.Dispose();
            //释放原始图片资源
            initImage.Dispose();
        }

        public static Image ImageFromSource(int imageWidth, int imageHeight, Image srcImage, Rectangle srcRect, Rectangle destRect)
        {
            //按模版大小生成最终图片
            Image dest = new Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(dest);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Clear(Color.White);
            g.DrawImage(srcImage, destRect, srcRect, GraphicsUnit.Pixel);

            return dest;
        }
    }
}