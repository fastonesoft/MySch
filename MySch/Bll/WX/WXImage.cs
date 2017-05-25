using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;

namespace MySch.Bll.WX
{
    public class WXImage
    {
        //私有裁剪
        static Image ImageFromSource(Image srcImage, Rectangle srcRect, Rectangle destRect)
        {
            Image dest = new Bitmap(destRect.Width, destRect.Height);
            Graphics g = Graphics.FromImage(dest);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Clear(Color.White);
            g.DrawImage(srcImage, destRect, srcRect, GraphicsUnit.Pixel);

            return dest;
        }

        /// <summary>
        /// 将任意图片裁剪成正方形
        /// </summary>
        /// <param name="imageID">图片名称</param>
        /// <param name="side">边长</param>
        public static void CutForSquare(string imageID, int side)
        {
            try
            {
                string imageName = HttpContext.Current.Server.MapPath(string.Format("~/Upload/XueImages/{0}.jpg", imageID));
                Image initImage = Image.FromFile(imageName);

                //原始图片的宽、高
                int initWidth = initImage.Width;
                int initHeight = initImage.Height;

                Rectangle fromR = initWidth > initHeight ? new Rectangle((initWidth - initHeight) / 2, 0, initHeight, initHeight) : new Rectangle(0, (initHeight - initWidth) / 2, initWidth, initWidth);
                Rectangle toR = initWidth > initHeight ? new Rectangle(0, 0, initHeight, initHeight) : new Rectangle(0, 0, initWidth, initWidth);
                //非正方型先裁剪为正方型
                if (initWidth != initHeight)
                {
                    //截图对象
                    Image pickedImage = ImageFromSource(initImage, fromR, toR);

                    //将截图对象赋给原图
                    initImage = (Image)pickedImage.Clone();
                    //释放截图资源
                    pickedImage.Dispose();
                }

                //创建缩略图
                Image resultImage = ImageFromSource(initImage, toR, new Rectangle(0, 0, side, side));
                //释放原始图片资源
                initImage.Dispose();
                //保存缩略图
                HttpContext.Current.Response.ContentType = "image/jpeg";
                resultImage.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
                //释放缩略图资源
                resultImage.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}