using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace MySch.Core
{
    public class ImageCode : IDisposable
    {
        private int _num;
        private Bitmap _src;
        private Bitmap[] _bits;
        private Bitmap _CloneBit;

        /// <summary>
        /// 创建灰度、黑白图片处理类
        /// </summary>
        /// <param name="src">源图</param>
        /// <param name="minWidth">最小字符宽度</param>
        public ImageCode(Bitmap src, int minWidth)
        {
            _src = src;

            //生成所需的图片
            _CloneBit = cloneBit();
            //三、切割
            _bits = splitBitByPixel(_CloneBit, minWidth);
        }

        public void Dispose()
        {
            _src.Dispose();

            for (int i = 0; i < _num; i++)
            {
                _bits[i].Dispose();
            }

            this.Dispose();
        }
        public int BitsNum { get { return _num; } }
        public Bitmap[] Bits { get { return _bits; } }
        public Bitmap CloneBit { get { return _CloneBit; } }

        //黑白
        private Bitmap cloneBit()
        {
            int width = _src.Width;
            int height = _src.Height;
            Rectangle rect = new Rectangle(0, 0, width, height);

            return _src.Clone(rect, PixelFormat.Format1bppIndexed);     
        }

        //灰度
        private Bitmap cloneGray()
        {
            int width = _src.Width;
            int height = _src.Height;
            Rectangle rect = new Rectangle(0, 0, width, height);

            return _src.Clone(rect, PixelFormat.Format4bppIndexed);
        }

        private Bitmap[] splitBitByPixel(Bitmap bt, int charWid)
        {
            int width = bt.Width;
            int height = bt.Height;
            bool[] haspixel = new bool[width];
            //查找图片列方向是否有字符数据
            for (int x = 0; x < width; x++)
            {
                haspixel[x] = false;
                for (int y = 0; y < height; y++)
                {
                    if (bt.GetPixel(x, y).R == 0)
                    {
                        haspixel[x] = true;
                        break;
                    }
                }
            }
            //准备切割
            _num = 0;
            int w = 0, begin = 0;
            Bitmap[] b = new Bitmap[width];  //开足够大的图片存储空间

            while (w < width)
            {
                if (haspixel[w])
                {
                    //确定是字符，推进->
                    begin = w++;
                    while (w < width && haspixel[w])
                    {
                        w++;
                    }
                    //字符长度达标，切割
                    if (w - begin > charWid)
                    {
                        int wid = w - begin;
                        Bitmap tmp = new Bitmap(wid, height);

                        using (Graphics g = Graphics.FromImage(tmp))
                        {
                            g.DrawImage(bt, new Rectangle(0, 0, wid, height), new Rectangle(begin, 0, wid, height), GraphicsUnit.Pixel);
                        }
                        //黑白
                        b[_num] = tmp.Clone(new Rectangle(0, 0, wid, height), PixelFormat.Format1bppIndexed);

                        //切开保存
                        //b[n].Save(@"d:\vcode\" + Guid.NewGuid().ToString() + ".bmp",ImageFormat.Bmp);

                        //切到一个图，记数器+1；
                        _num++;
                    }
                }
                else
                {
                    //确定不是字符，推进->
                    while (w < width && !haspixel[w])
                    {
                        w++;
                    }
                }
            }
            return b;
        }

        private static bool CompareBits(Bitmap src, Bitmap dest)
        {
            int swid = src.Width;
            int shei = src.Height;
            int dwid = dest.Width;
            int dhei = dest.Height;

            int count = 0;
            //长、宽不匹配
            if (swid != dwid || shei != dhei) return false;

            for (int y = 0; y < shei; y++)
            {
                for (int x = 0; x < swid; x++)
                {
                    Color scolor = src.GetPixel(x, y);
                    Color dcolor = dest.GetPixel(x, y);
                    //颜色不同，退出
                    //if (scolor != dcolor) return false;
                    if (scolor != dcolor )
                    {
                        count++;
                    }
                }
            }

            //完全相同，返回true
            if (count > 0) return false;
            return true;
        }

        //检测验证码
        public static string GetValidedCode(Bitmap dest, Bitmap[] src)
        {
            string code = string.Empty;
            ImageCode destBit = new ImageCode(dest, 1);

            for (int x = 0; x < destBit.BitsNum; x++)
            {
                for (int y = 0; y < src.Length; y++)
                {
                    if (ImageCode.CompareBits(destBit.Bits[x], src[y]))
                    {
                        code += Convert.ToChar(Convert.ToInt16('a') + y);
                        break;
                    }
                }
            }

            return code;
        }

        #region 比较 -> 完全相同  public static bool CompareTheSame(Bitmap src, Bitmap dest)
        //public static bool CompareTheSame(Bitmap src, Bitmap dest)
        //{
        //    int swid = src.Width;
        //    int shei = src.Height;
        //    int dwid = dest.Width;
        //    int dhei = dest.Height;

        //    //如果长、宽不匹配，则返回false
        //    if (swid != dwid || shei != dhei) return false;

        //    Rectangle rect = new Rectangle(0, 0, swid, shei);
        //    BitmapData srcData = src.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
        //    IntPtr srcPtr = srcData.Scan0;
        //    int rowBytes = srcData.Stride;
        //    int totalBytes = rowBytes * shei;
        //    byte[] srcVals = new byte[totalBytes];
        //    Marshal.Copy(srcPtr, srcVals, 0, totalBytes);
        //    //解锁位图  
        //    src.UnlockBits(srcData);

        //    BitmapData destData = dest.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
        //    IntPtr destPtr = destData.Scan0;
        //    byte[] destVals = new byte[totalBytes];
        //    Marshal.Copy(destPtr, destVals, 0, totalBytes);
        //    //解锁位图  
        //    dest.UnlockBits(destData);

        //    for (int y = 0; y < shei; y++)
        //    {
        //        for (int x = 0; x < swid; x++)
        //        {
        //            int k = x * 3;
        //            int rowsBytes = y * rowBytes;

        //            for (int z = 0; z < 3; z++)
        //            {
        //                //有一点不同，返回false
        //                if (srcVals[rowsBytes + k + z] != destVals[rowsBytes + k + z]) return false;
        //            }
        //        }
        //    }
        //    //完全相同，返回true
        //    return true;
        //}
        #endregion
    }

}