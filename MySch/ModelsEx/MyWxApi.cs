using MySch.Bll;
using MySch.Bll.Func;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Xml;

namespace MySch.ModelsEx
{
    public class MyWxApi
    {
        /// <summary>
        /// 签名：参数方式验证
        /// </summary>
        /// <param name="token">与第三方共同持有</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce"></param>
        /// <param name="signature">加密后的数据</param>
        /// <returns></returns>
        public static bool CheckSignature(string token, string timestamp, string nonce, string signature)
        {
            string[] ArrTmp = { token, timestamp, nonce };

            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);

            tmpStr = Setting.GetSHA1(tmpStr);
            tmpStr = tmpStr.ToLower();

            return tmpStr == signature ? true : false;
        }

        /// <summary>
        /// 签名：对象封装方式验证
        /// </summary>
        /// <param name="au"></param>
        /// <returns></returns>
        public static bool CheckSignature(WX_Author au)
        {
            if (au == null) return false;

            return CheckSignature("WX1979ToKen", au.timestamp, au.nonce, au.signature);
        }

        /// <summary>
        /// 自动报名：以查找省网学籍方式，记录学生信息
        /// </summary>
        /// <param name="ID">身份证号</param>
        /// <param name="Name">学生姓名</param>
        /// <param name="openID">ID</param>
        /// <returns></returns>
        public static string StudReg(string Name, string ID, string openID)
        {
            try
            {
                //检测身份证号是否有效
                IDC.IDS(ID);

                CookieCollection cookies = null;
                //一、做Get请求网页
                string url = "http://jcjy.etec.edu.cn/studman2/cidGetInfo.jsp";
                using (HttpWebResponse resp = MyHtml.GetResponse(url))
                {
                    cookies = resp.Cookies;
                }

                //二、做验证图片请求
                //模板图片读取
                Bitmap[] srcBit = new Bitmap[26];
                for (int i = 0; i < 26; i++)
                {
                    srcBit[i] = new Bitmap(HttpContext.Current.Server.MapPath(string.Format("~/Images/vbit/{0}.bmp", Convert.ToChar(Convert.ToInt16('a') + i))));
                }
                //读取图片
                Bitmap dest = null;
                string valid = string.Empty;
                string imageurl = "http://jcjy.etec.edu.cn/studman2/genImageCode?rnd=" + DateTime.Now.Ticks.ToString();
                //异常循环记录器
                int errorcount = 0;
                //循环读取图片  直到识别出 5 个字符
                do
                {
                    using (HttpWebResponse resp = MyHtml.GetResponse(imageurl, cookies))
                    {
                        dest = MyHtml.GetBitmap(resp);
                    }
                    valid = MyImageCode.GetValidedCode(dest, srcBit);
                    //循环记录
                    errorcount++;
                    //异常退出
                    if (errorcount > 30) throw new Exception("请确认验证码是否变更");
                } while (valid.Length != 5);

                //三、准备Post请求数据
                Random rnd = new Random();
                Dictionary<string, string> dicts = new Dictionary<string, string>();
                dicts.Add("name", Name);
                dicts.Add("cid", ID);
                dicts.Add("randomCode", valid);
                dicts.Add("v", rnd.NextDouble().ToString());
                string postdata = MyHtml.DictToPostData(dicts, Encoding.GetEncoding("GBK"));
                //提交请求
                HttpWebResponse postresp = MyHtml.PostResponse(url, cookies, postdata, Encoding.GetEncoding("GBK"));
                string html = MyHtml.GetHtml(postresp, Encoding.GetEncoding("GBK"));
                //分析返回数据
                Regex regx = new Regex(@"<td>([\u4e00-\u9fa5]+|\d{17}[0-9X]|[A-Z]\d{17}[0-9X])</td>");
                MatchCollection matchs = regx.Matches(html);

                //如果没有找到数据，则返回提示
                if (matchs.Count == 0) throw new Exception("无学籍记录！检查身份证与姓名是否正确");
                //排除重复身份证号
                string id = matchs[2].Groups[1].ToString();
                var db = DataCRUD<TStudReg>.Entity(a => a.IDS == id);
                if (db != null) throw new Exception("该身份证号学籍已注册");
                //根据返回数据 -> 创建学生报名记录
                TStudReg stud = new TStudReg();
                stud.FromSch = matchs[0].Groups[1].ToString();
                stud.Name = matchs[1].Groups[1].ToString();
                stud.IDS = id;
                stud.FromGrade = matchs[3].Groups[1].ToString();
                stud.NationID = matchs[4].Groups[1].ToString();
                stud.ReadState = matchs[5].Groups[1].ToString();
                stud.IsProblem = matchs[6].Groups[1].ToString() == "是" ? true : false;
                stud.ID = Guid.NewGuid().ToString("N");
                //
                stud.SchChoose = false;
                stud.StudNo = null;
                stud.Memo = null;
                //
                stud.Reged = false;
                //
                stud.OpenID = openID;

                //添加
                DataCRUD<TStudReg>.Add(stud);
                //返回
                return stud.ID;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 完成学生信息的绑定
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="openID"></param>
        public static void StudBinding(string Name, string ID, string openID)
        {
            try
            {
                //检测身份证号是否有效
                IDC.IDS(ID);

                var db = DataCRUD<TStudReg>.Entity(a => a.IDS == ID && a.Name == Name && string.IsNullOrEmpty(a.OpenID));
                if (db == null) throw new Exception("学生姓名与身份证号不匹配，或者已经完成登记");

                //找到对应学生，绑定
                db.OpenID = openID;

                //提交更新
                DataCRUD<TStudReg>.Update(db);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 学生报名信息
        /// </summary>
        /// <param name="openID">编号</param>
        /// <returns></returns>
        public static string StudInfor(string openID)
        {
            try
            {
                var db = DataCRUD<TStudReg>.Entity(a => a.OpenID == openID);
                if (db == null) throw new Exception("未完成帐号与学生信息的绑定");
                if (string.IsNullOrEmpty(db.StudNo)) throw new Exception("报名资料未审核，请按公示时间携带相关证件到指定地点审核！");

                string res = string.Empty;
                res += string.Format("姓名：{0}\n", db.Name);
                res += string.Format("身份：{0}\n", db.IDS);
                res += string.Format("学校：{0}\n", db.FromSch);
                res += string.Format("年级：{0}\n", db.FromGrade);
                res += "---------------------------\n";
                res += string.Format("录取编号：{0}", db.StudNo.Substring(db.StudNo.Length - 4, 4));
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 录取人数统计
        /// </summary>
        /// <param name="openID"></param>
        /// <returns></returns>
        public static string StudCount(string openID)
        {
            try
            {
                var db = DataCRUD<TStudReg>.Entity(a => a.OpenID == openID);
                if (db == null) throw new Exception("未完成帐号与学生信息的绑定");
                if (string.IsNullOrEmpty(db.StudNo)) throw new Exception("报名资料未审核，请按公示时间携带相关证件到指定地点审核！");

                var count = DataCRUD<TStudReg>.Count(a => true);
                string res = string.Empty;
                res += string.Format("实验初中2016级新生已录取：{0}人", count);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 常用查询命令
        /// </summary>
        /// <returns></returns>
        public static string NormalCommand()
        {
            string res = string.Empty;

            res += "一、输入学生的身份证号码\n";
            res += "二、学籍\n";
            res += "三、成绩\n";

            return res;
        }

        /// <summary>
        /// 检测ID是否绑定学生
        /// </summary>
        /// <param name="openID">ID</param>
        /// <returns></returns>
        public static bool Binding(string openID)
        {
            //ID为空，则没有绑定
            if (string.IsNullOrEmpty(openID)) return false;

            //检测是否存在ID记录
            var db = DataCRUD<TStudReg>.Entitys(a => a.OpenID == openID);

            //有记录，说明已绑定
            return db.Count() > 0;
        }

    }


}