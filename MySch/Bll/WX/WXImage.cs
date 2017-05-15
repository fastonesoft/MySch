using MySch.Bll.WX.Model;
using MySch.Dal;
using MySch.Helper;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
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


        /// <summary>
        /// 将临时图片保存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaID"></param>
        /// <param name="openID"></param>
        public static void SaveUnloadImage(string mediaID, string openID)
        {
            try
            {
                //中控token
                var wxtoken = WX_AccessToken.GetAccessToken();

                var url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", wxtoken, mediaID);

                //保存
                var web = new WebClient();
                var fileName = Guid.NewGuid().ToString("N");
                var fileType = "jpg";
                var filePath = HttpContext.Current.Server.MapPath(string.Format("~/Upload/XueImages/{0}.{1}", fileName, fileType));
                web.DownloadFile(url, filePath);

                //根据openID读取学生编号
                //var db = DataCRUD<Student>.Entity(a => a.OpenID == openID);

                //下载成功,记录
                var upload = new WxUploadFile
                {
                    ID = fileName,
                    IDS = "32128402012017010001",
                    FileType = fileType,
                    UploadType = "WX",
                    CreateTime = DateTime.Now,
                    Author = openID,
                };
                DataCRUD<WxUploadFile>.Add(upload);
            }
            catch (Exception e)
            {                
                throw e;
            }
        }
    }
}