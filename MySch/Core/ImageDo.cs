using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace MySch.Core
{
    public class ImageDo
    {
        #region 正方型裁剪并缩放
        /// <summary>
        /// 正方型裁剪
        /// 以图片中心为轴心，截取正方型，然后等比缩放
        /// </summary>
        /// <param name="stream">原图像数据流</param>
        /// <param name="fileSaveUrl">缩略图存放地址（物理地址）</param>
        /// <param name="side">指定正方型边长</param>
        /// <param name="zoomIn">图像偏小时是否放大</param>
        public static Image CutForSquare(Image initImage, int side, bool zoomIn)
        {
            //原图宽高均小于模版，若不需要放大，则：不作处理，直接保存
            if (!zoomIn && initImage.Width <= side && initImage.Height <= side)
            {
                return initImage;
            }

            //原始图片的宽、高
            int initWidth = initImage.Width;
            int initHeight = initImage.Height;

            //非正方型先裁剪为正方型
            if (initWidth != initHeight)
            {
                //截图对象
                Image pickedImage = null;

                //宽大于高的横图
                if (initWidth > initHeight)
                {
                    //定位
                    Rectangle fromR = new Rectangle((initWidth - initHeight) / 2, 0, initHeight, initHeight);
                    Rectangle toR = new Rectangle(0, 0, initHeight, initHeight);
                    //创建目标图片
                    pickedImage = ImageFromSource(initWidth, initHeight, initImage, fromR, toR);
                    //重置宽
                    initWidth = initHeight;
                }
                //高大于宽的竖图
                else
                {
                    //定位
                    Rectangle fromR = new Rectangle(0, (initHeight - initWidth) / 2, initWidth, initWidth);
                    Rectangle toR = new Rectangle(0, 0, initWidth, initWidth);
                    //创建目标图片
                    pickedImage = ImageFromSource(initWidth, initHeight, initImage, fromR, toR);
                    //重置高
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
            return resultImage;
        }
        #endregion

        #region 固定模版裁剪并缩放
        /// <summary>
        /// 指定长宽裁剪
        /// 按模版比例最大范围的裁剪图片并缩放至模版尺寸
        /// </summary>
        /// <param name="stream">原图像数据流</param>
        /// <param name="fileSaveUrl">缩略图存放地址（物理地址）</param>
        /// <param name="maxWidth">缩略图宽度</param>
        /// <param name="maxHeight">缩略图高度</param>
        /// <param name="zoomIn">图像偏小时是否放大</param>
        public static Image CutForCustom(Image initImage, int maxWidth, int maxHeight, bool zoomIn)
        {
            //原图宽高均小于模版，若不需要放大，则：不作处理，直接保存
            if (!zoomIn && initImage.Width <= maxWidth && initImage.Height <= maxHeight)
            {
                return initImage;
            }

            //模版的宽高比例
            double tempRate = (double)maxWidth / maxHeight;
            //原图片的宽高比例
            double initRate = (double)initImage.Width / initImage.Height;

            //原图与模版比例相等，直接缩放
            if (tempRate == initRate)
            {
                //按模版大小生成最终图片
                Image tempImage = ImageFromSource(maxWidth, maxHeight, initImage, new Rectangle(0, 0, initImage.Width, initImage.Height), new Rectangle(0, 0, maxWidth, maxHeight));
                return tempImage;
            }
            //原图与模版比例不等，裁剪后缩放
            else
            {
                //裁剪对象
                Image pickedImage = null;

                //定位
                Rectangle fromR = new Rectangle(0, 0, 0, 0);
                Rectangle toR = new Rectangle(0, 0, 0, 0);

                //宽为标准进行裁剪
                if (tempRate > initRate)
                {
                    //裁剪源定位
                    fromR.X = 0;
                    fromR.Y = (int)Math.Floor((initImage.Height - initImage.Width / tempRate) / 2);
                    fromR.Width = initImage.Width;
                    fromR.Height = (int)Math.Floor(initImage.Width / tempRate);
                    //裁剪目标定位
                    toR.X = 0;
                    toR.Y = 0;
                    toR.Width = initImage.Width;
                    toR.Height = (int)Math.Floor(initImage.Width / tempRate);
                    //裁剪对象实例化
                    pickedImage = ImageFromSource(initImage.Width, (int)Math.Floor(initImage.Width / tempRate), initImage, fromR, toR);
                }
                //高为标准进行裁剪
                else
                {
                    //裁剪源定位
                    fromR.X = (int)Math.Floor((initImage.Width - initImage.Height * tempRate) / 2);
                    fromR.Y = 0;
                    fromR.Width = (int)Math.Floor(initImage.Height * tempRate);
                    fromR.Height = initImage.Height;
                    //裁剪目标定位
                    toR.X = 0;
                    toR.Y = 0;
                    toR.Width = (int)Math.Floor(initImage.Height * tempRate);
                    toR.Height = initImage.Height;
                    //裁剪对象实例化
                    pickedImage = ImageFromSource((int)Math.Floor(initImage.Height * tempRate), initImage.Height, initImage, fromR, toR);
                }
                //
                initImage.Dispose();
                //按模版大小生成最终图片
                Image tempImage = ImageFromSource(maxWidth, maxHeight, pickedImage, new Rectangle(0, 0, pickedImage.Width, pickedImage.Height), new Rectangle(0, 0, maxWidth, maxHeight));
                return tempImage;
            }
        }
        #endregion

        #region 等比缩放

        /// <summary>
        /// 图片等比缩放
        /// </summary>
        /// <param name="fromFile">上传图片文件流</param>
        /// <param name="savePath">缩略图存放路径（服务器实际路径）</param>
        /// <param name="Width">缩略图参考长度</param>
        /// <param name="Height">缩略图参考宽度</param>
        /// <param name="maskImage">水印图片路径（为""表示不使用水印图片）</param>
        /// <param name="maskLogo">水印徽标路径（为""表示不使用水印徽标）</param>
        /// <param name="maskAlpha">水印透明度（0-100）</param>
        /// <param name="Padding">水印的边框距离</param>
        /// <param name="pimage">水印图片位置</param>
        /// <param name="plogo">水印徽标位置</param>
        public static void ZoomAuto(Stream stream, string savePath, int Width, int Height, string maskImage, string maskLogo, int maskAlpha, int Padding, Position pimage, Position plogo)
        {
            //创建目录
            string dir = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //透明度百分比计算
            maskAlpha = maskAlpha < 0 ? 0 : (maskAlpha > 100 ? 100 : maskAlpha);
            Single alpha = (Single)maskAlpha / 100;

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
            Image initImage = Image.FromStream(stream, true);

            //原图宽高均小于模版，不作处理，直接保存
            if (initImage.Width <= Width && initImage.Height <= Height)
            {
                //保存
                initImage.Save(savePath, ImageFormat.Jpeg);
            }
            else
            {
                #region 缩略图宽、高计算

                //缩略图宽、高计算
                int newWidth = initImage.Width;
                int newHeight = initImage.Height;

                //宽大于高或宽等于高（横图或正方）
                if (initImage.Width >= initImage.Height)
                {
                    //如果宽大于模版
                    if (initImage.Width > Width)
                    {
                        //宽按模版，高按比例缩放
                        newWidth = Width;
                        newHeight = initImage.Height * (Width / initImage.Width);
                    }
                }
                //高大于宽（竖图）
                else
                {
                    //如果高大于模版
                    if (initImage.Height > Height)
                    {
                        //高按模版，宽按比例缩放
                        newHeight = Height;
                        newWidth = initImage.Width * (Height / initImage.Height);
                    }
                }

                #endregion

                //绘制目标图         
                Image newImage = ImageFromSource(newWidth, newHeight, initImage, new Rectangle(0, 0, initImage.Width, initImage.Height), new Rectangle(0, 0, newWidth, newHeight));
                //透明图片水印maskImage
                ImageDrawLogo(newImage, maskImage, pimage, Padding, alpha);
                //透明图片水印maskLogo
                ImageDrawLogo(newImage, maskLogo, plogo, Padding, alpha);
                //保存缩略图
                newImage.Save(savePath, ImageFormat.Jpeg);
                //释放资源
                newImage.Dispose();
            }
            //释放资源
            initImage.Dispose();
        }

        #endregion

        public static Image ImageFromSource(int imageWidth, int imageHeight, Image srcImage, Rectangle srcRect)
        {
            return ImageFromSource(imageWidth, imageHeight, srcImage, srcRect, new Rectangle(0, 0, imageWidth, imageHeight));
        }

        public static Image ImageFromSource(int imageWidth, int imageHeight, Image srcImage, Rectangle srcRect, Rectangle destRect)
        {
            return ImageFromSource(imageWidth, imageHeight, srcImage, srcRect, destRect, InterpolationMode.HighQualityBicubic, SmoothingMode.HighQuality, Color.White, GraphicsUnit.Pixel);
        }

        public static Image ImageFromSource(int imageWidth, int imageHeight, Image srcImage, Rectangle srcRect, Rectangle destRect, InterpolationMode interpolationmode, SmoothingMode smoothingmode, Color bgcolor, GraphicsUnit graphicsunit)
        {
            //按模版大小生成最终图片
            Image dest = new Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(dest);

            g.InterpolationMode = interpolationmode;
            g.SmoothingMode = smoothingmode;
            g.Clear(bgcolor);
            g.DrawImage(srcImage, destRect, srcRect, graphicsunit);

            return dest;
        }



        /// <summary>
        /// 根据给定的方位、透明度将水印打到图像上
        /// </summary>
        /// <param name="image">背景图片对象</param>
        /// <param name="logoFileName">水印文件名称</param>
        /// <param name="p">水印方位</param>
        /// <param name="Padding">偏移量</param>
        /// <param name="alpha">透明度</param>
        public static void ImageDrawLogo(Image image, string logoFileName, Position p, int Padding, Single alpha)
        {
            //透明度范围重置
            alpha = alpha < 0 ? 0 : (alpha > 1 ? 1 : alpha);

            //检测图片文件
            if (logoFileName != "")
            {
                if (File.Exists(logoFileName))
                {
                    //获取水印图片
                    using (Image logoImage = Image.FromFile(logoFileName))
                    {
                        //水印绘制条件：原始图片宽高均大于或等于水印图片
                        if (image.Width >= logoImage.Width + Padding * 2 && image.Height >= logoImage.Height + Padding * 2)
                        {
                            Graphics gWater = Graphics.FromImage(image);

                            //透明属性
                            ImageAttributes imgAttributes = new ImageAttributes();
                            ColorMap colorMap = new ColorMap();
                            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                            ColorMap[] remapTable = { colorMap };
                            imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                            float[][] colorMatrixElements = { 
                                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  0.0f,  alpha, 0.0f},
                                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                };

                            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                            imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            //计算位置
                            Rectangle destRect = DrawImageRect(image.Width, image.Height, logoImage.Width, logoImage.Height, Padding, p);

                            gWater.DrawImage(logoImage, destRect, 0, 0, logoImage.Width, logoImage.Height, GraphicsUnit.Pixel, imgAttributes);
                            gWater.Dispose();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 根据水印图片大小，计算绘制位置
        /// </summary>
        /// <param name="imageWidth">原始图像宽度</param>
        /// <param name="imageHeight">原始图像高度</param>
        /// <param name="logoWidth">水印图像宽度</param>
        /// <param name="logoHeight">水印图像高度</param>
        /// <param name="Padding">边距</param>
        /// <param name="position">图片绘制位置</param>
        /// <returns></returns>
        public static Rectangle DrawImageRect(int imageWidth, int imageHeight, int logoWidth, int logoHeight, int Padding, Position position)
        {
            switch (position)
            {
                case Position.Center:
                    return new Rectangle((imageWidth - logoWidth) / 2, (imageHeight - logoHeight) / 2, logoWidth, logoHeight);

                case Position.TopLeft:
                    return new Rectangle(Padding, Padding, logoWidth, logoHeight);

                case Position.TopRight:
                    return new Rectangle(imageWidth - logoWidth - Padding, Padding, logoWidth, logoHeight);

                case Position.BottomLeft:
                    return new Rectangle(Padding, imageHeight - logoHeight - Padding, logoWidth, logoHeight);

                case Position.BottomRight:
                    return new Rectangle(imageWidth - logoWidth - Padding, imageHeight - logoHeight - Padding, logoWidth, logoHeight);

                default:
                    return new Rectangle(0, 0, logoWidth, logoHeight);
            }
        }


        /// <summary>
        /// 位置定义
        /// </summary>
        public enum Position
        {
            Center,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }


        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }

        private static EncoderParameters GetEncoderParameters(int quality)
        {
            EncoderParameters encoderParams = new EncoderParameters();
            long[] Quality = new long[1];
            Quality[0] = quality; //压缩比例，决定图片大小的重要因素。
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Quality);
            encoderParams.Param[0] = encoderParam;

            return encoderParams;
        }

    }
}