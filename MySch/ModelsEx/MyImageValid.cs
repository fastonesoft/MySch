using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MySch.ModelsEx
{
    public class MyImageValid
    {

        #region 局部静态变量描述

        // Length：验证码长度(默认6个验证码的长度  长度不得超过颜色表的颜色数)
        private static int length = 6;

        // FontSize：验证码字体大小(为了显示扭曲效果，默认40像素，可以自行修改)
        private static int fontSize = 8;

        // padding：内边框(默认5像素)
        private static int padding = 5;

        // paddingShow：是否显示边框
        private static bool paddingShow = false;

        // wordSpacing：字间距(默认2像素)
        private static int wordSpacing = 3;

        // chaos：是否输出燥点
        private static bool chaos = true;

        // chaosNum：燥点倍数
        private static int chaosNum = 5;

        // chaosColorRand：是否随机设置燥点颜色
        private static bool chaosColorRand = true;

        // chaosColor：输出燥点的颜色(默认灰色)
        private static Color chaosColor = Color.LightGray;

        // backgroundColor：背景色(默认白色)
        private static Color backgroundColor = Color.FromArgb(0, 102, 204);

        // Colors：自定义随机颜色数组
        //private static Color[] colors = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Brown, Color.DarkCyan, Color.Purple };
        private static Color[] colors = { Color.Brown, Color.Chocolate, Color.CornflowerBlue, Color.DarkGoldenrod, Color.DarkGreen, Color.DarkMagenta, Color.DarkRed, Color.DimGray, Color.ForestGreen };

        // Fonts：自定义字体数组
        private static string[] fonts = { "Arial", "Georgia", "Comic Sans MS", "Verdana", "Courier New" };

        // CodeSerial：随机码字符串序列(使用逗号分隔)
        private static string codeSerial = "A,B,E,F,H,J,K,M,N,P,R,S,T,U,V,W,X,Y";

        #endregion

        #region 属性参数

        public static int Length
        {
            get { return length; }
            set { length = value; }
        }

        public static int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }

        public static int Padding
        {
            get { return padding; }
            set { padding = value; }
        }

        public static bool PaddingShow
        {
            get { return paddingShow; }
            set { paddingShow = value; }
        }

        public static int WordSpacing
        {
            get { return wordSpacing; }
            set { wordSpacing = value; }
        }

        public static bool Chaos
        {
            get { return chaos; }
            set { chaos = value; }
        }

        public static int ChaosNum
        {
            get { return chaosNum; }
            set { chaosNum = value; }
        }

        public static bool ChaosColorRand
        {
            get { return chaosColorRand; }
            set { chaosColorRand = value; }
        }

        public static Color ChaosColor
        {
            get { return chaosColor; }
            set { chaosColor = value; }
        }

        public static Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        public static Color[] Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        public static string[] Fonts
        {
            get { return fonts; }
            set { fonts = value; }
        }

        public static string CodeSerial
        {
            get { return codeSerial; }
            set { codeSerial = value; }
        }

        #endregion

        #region 1、CreateVerifyCode：生成随机字符码(int codeLen)

        /// <summary>
        /// 生成随机字符码
        /// </summary>
        /// <param name="codeLen"></param>
        /// <returns></returns>

        private static string CreateVerifyCode(int codeLen)
        {
            if (codeLen == 0)
            {
                codeLen = length;
            }

            string[] arr = codeSerial.Split(',');

            string code = "";

            int randValue = -1;

            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));

            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);

                code += arr[randValue];
            }

            return code;
        }

        #endregion

        #region 2、CreateImageCode：生成校验码图片(string code)

        private static Bitmap CreateImageCode(string code)
        {
            //字体大小
            int fSize = fontSize;

            //字符宽度、高度
            int cWidth = fSize + wordSpacing * 2;
            int cHeight = fSize * 2;

            //图片宽度、高度
            int imageWidth = code.Length * cWidth + padding * 3;
            int imageHeight = cHeight + padding * 3;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(image);
            g.Clear(backgroundColor);

            Random rand = new Random();

            int left = 0, top = 0;
            int n1 = imageHeight;
            int n2 = n1 / 3;
            //int n2 = n1 - cHeight + padding * 2;

            Font f;
            Brush b;

            int cindex, findex;

            //随机字体和颜色的验证码字符
            string colorindex = string.Empty;
            for (int i = 0; i < code.Length; i++)
            {
                //字体颜色只能出现一次
                do
                {
                    cindex = rand.Next(colors.Length);
                    findex = rand.Next(fonts.Length);

                } while (colorindex.IndexOf(cindex.ToString()) != -1);
                //记录出现过的颜色
                colorindex += cindex.ToString() + ",";


                f = new System.Drawing.Font(fonts[findex], fSize + 5, System.Drawing.FontStyle.Bold);
                b = new System.Drawing.SolidBrush(colors[cindex]);

                if (i % 2 == 0)
                {
                    top = 0;
                }
                else
                {
                    top = n2;
                }

                left = i * cWidth + wordSpacing;


                g.DrawString(code.Substring(i, 1), f, b, left, top);


            }

            //随机添加生成的燥点
            if (chaos)
            {
                int c = length * chaosNum;
                Pen pen = new Pen(chaosColor);

                for (int i = 0; i < c; i++)
                {
                    int x = rand.Next(image.Width);
                    int y = rand.Next(image.Height);

                    //给燥点设置随机色彩
                    if (chaosColorRand)
                    {
                        int cr = rand.Next(0, 256);
                        int cg = rand.Next(0, 256);
                        int cb = rand.Next(0, 256);

                        pen = new Pen(Color.FromArgb(cr, cg, cb));
                    }

                    g.DrawRectangle(pen, x, y, 1, 1);
                }
            }

            g.Dispose();
            return image;
        }

        #endregion

        #region 3、TwistImage：产生波形滤镜效果

        private const double PI1 = 3.1415926535897932384626433832795;
        private const double PI2 = 6.283185307179586476925286766559 * 0.8;



        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp"></param>
        /// <param name="distImage"></param>
        /// <returns></returns>
        private static System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool distImage)
        {
            Random rand = new Random();
            int dPhase = rand.Next(100);
            int dMultValue = rand.Next(3, 5);

            System.Drawing.Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(backgroundColor), 0, 0, destBmp.Width, destBmp.Height);

            double dBaseAxisLen = distImage ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = distImage ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = distImage ? i + (int)(dy * dMultValue) : i;
                    nOldY = distImage ? j : j + (int)(dy * dMultValue);

                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            if (paddingShow)
            {
                //画一个边框 边框颜色为Color.Gainsboro
                graph.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, destBmp.Width - 1, destBmp.Height - 1);
            }
            graph.Dispose();

            return destBmp;
        }

        #endregion

        /// <summary>
        /// 将验证码产生的图片输出
        /// </summary>
        /// <param name="session"></param>
        /// <param name="alength"></param>
        /// <param name="afontsize"></param>
        /// <param name="apadding"></param>
        /// <param name="apaddingshow"></param>
        /// <param name="awordspacing"></param>
        /// <param name="achaos"></param>
        /// <param name="achaosnum"></param>
        /// <param name="achaosColorRand"></param>
        /// <param name="achaoscolor"></param>
        /// <param name="abgcolor"></param>
        /// <param name="acolors"></param>
        /// <param name="afonts"></param>
        /// <param name="acodeserial"></param>
        /// <returns></returns>
        public static byte[] OutputImageByteArray(HttpSessionStateBase session, string validnum, int? alength, int? afontsize, int? apadding, bool? apaddingshow, int? awordspacing, bool? achaos, int? achaosnum, bool? achaosColorRand, Color? achaoscolor, Color? abgcolor, Color[] acolors, string[] afonts, string acodeserial)
        {
            //参数设置
            Length = alength == null ? Length : alength.Value; ;
            FontSize = afontsize == null ? FontSize : afontsize.Value;
            Padding = apadding == null ? Padding : apadding.Value;
            PaddingShow = apaddingshow == null ? PaddingShow : apaddingshow.Value;
            WordSpacing = awordspacing == null ? WordSpacing : awordspacing.Value;
            Chaos = achaos == null ? Chaos : achaos.Value;
            ChaosNum = achaosnum == null ? ChaosNum : achaosnum.Value;
            ChaosColor = achaoscolor == null ? ChaosColor : achaoscolor.Value;
            ChaosColorRand = achaosColorRand == null ? ChaosColorRand : achaosColorRand.Value;
            BackgroundColor = abgcolor == null ? BackgroundColor : abgcolor.Value;
            Colors = acolors == null ? Colors : acolors;
            Fonts = afonts == null ? Fonts : afonts;
            CodeSerial = acodeserial == string.Empty ? CodeSerial : acodeserial;

            //创建验证串
            string code = CreateVerifyCode(Length);

            //保存图片session标识
            session[validnum] = code.ToUpper();

            //创建验证图片
            Bitmap image = CreateImageCode(code);
            image = TwistImage(image, true);

            //创建格式流
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            //保存数据流
            byte[] imagebytearr = ms.GetBuffer();

            //关闭
            ms.Close();
            ms = null;
            image.Dispose();
            image = null;

            //返回数据流
            return imagebytearr;
        }

        /// <summary>
        /// 将验证码产生的图片输出
        /// </summary>
        /// <param name="session"></param>
        /// <param name="validnum"></param>
        /// <param name="alength"></param>
        /// <param name="abgcolor"></param>
        /// <param name="apaddingshow"></param>
        /// <returns></returns>
        public static byte[] OutputImageByteArray(HttpSessionStateBase session, string validnum, int alength, Color abgcolor, bool apaddingshow)
        {
            return OutputImageByteArray(session, validnum, alength, null, null, apaddingshow, null, null, null, null, null, abgcolor, null, null, string.Empty);
        }


        /// <summary>
        /// 检测验证码是否正确
        /// </summary>
        /// <param name="session">当前session</param>
        /// <param name="validcode">用户提交的验证码</param>
        /// <returns></returns>
        public static bool CheckValidateCode(HttpSessionStateBase session, string validnum, string validcode)
        {
            return session[validnum] == null ? false : session[validnum].ToString().ToUpper() == validcode.ToUpper();
        }

    }
}