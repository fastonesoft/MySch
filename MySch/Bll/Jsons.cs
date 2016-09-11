using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace MySch.Bll
{
    public class Jsons<Entity>
    {
        /// <summary>
        /// JSON方式：将 对象 -> 实体
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Entity JsonEntity(object obj)
        {
            try
            {
                var javas = new JavaScriptSerializer();
                var jsons = javas.Serialize(obj);
                return JsonEntity(jsons);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Entity JsonEntity(string jsons)
        {
            try
            {
                //这里不能用Newtonsoft.Json
                //原因：未知，暂时这样

                //日期格式修正
                jsons = Regex.Replace(jsons, @"\\/Date\((\d+)\)\\/", match =>
                {
                    DateTime datetime = new DateTime(1970, 1, 1);
                    datetime = datetime.AddMilliseconds(long.Parse(match.Groups[1].Value));
                    datetime = datetime.ToLocalTime();
                    return datetime.ToString("yyyy-MM-dd HH:mm:ss");
                });

                var javas = new JavaScriptSerializer();
                return javas.Deserialize<Entity>(jsons);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    /// <summary>
    /// 转换Json的基类，主要应用于模型输出
    /// </summary>
    public class Jsons
    {
        public static string ToJsons(object obj)
        {
            try
            {
                var javas = new JavaScriptSerializer();
                var jsons = javas.Serialize(obj);

                //日期格式修正
                jsons = Regex.Replace(jsons, @"\\/Date\((\d+)\)\\/", match =>
                {
                    DateTime datetime = new DateTime(1970, 1, 1);
                    datetime = datetime.AddMilliseconds(long.Parse(match.Groups[1].Value));
                    datetime = datetime.ToLocalTime();
                    return datetime.ToString("yyyy-MM-dd HH:mm:ss");
                });

                return jsons;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}