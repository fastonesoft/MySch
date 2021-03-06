﻿using MySch.Bll;
using MySch.Bll.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Core
{
    public class IDC
    {
        public static ErrorMessage IDS(string ids)
        {
            try
            {
                Check(ids);
                return new ErrorMessage { error = false, message = "身份证号检测无误！" };
            }
            catch (Exception e)
            {
                return new ErrorMessage { error = true, message = e.Message };
            }
        }

        public static ErrorMessage IDS(string ids, int begin, int end)
        {
            try
            {
                Check(ids);
                var year = int.Parse(ids.Substring(6, 4));
                if (year < begin || year > end) throw new Exception(string.Format("出生年份不在范围【{0}－{1}】", begin, end));

                return new ErrorMessage { error = false, message = "身份证号检测无误！" };
            }
            catch (Exception e)
            {
                return new ErrorMessage { error = true, message = e.Message };
            }
        }

        public static void Check(string ids)
        {
            if (ids.Length != 18)
            {
                throw new Exception("身份证号：长度不满18位！");
            }

            long n = 0;
            if (long.TryParse(ids.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(ids.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                throw new Exception("身份证号：数字验证无法通过！");
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(ids.Remove(2)) == -1)
            {
                throw new Exception("身份证号：省份验证无法通过！");
            }
            string birth = ids.Substring(6, 8).Insert(6, "-").Insert(4, "-");

            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                throw new Exception("身份证号：生日验证无法通过！");
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');

            char[] Ai = ids.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != ids.Substring(17, 1).ToLower())
            {
                throw new Exception("身份证号：校验码验证无法通过！");
            }
            //符合GB11643-1999标准
        }

    }
}