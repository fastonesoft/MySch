using MySch.Bll.Func;
using MySch.Dal;
using MySch.Helper;
using MySch.Models;
using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    /////////////////////////////////////////
    //WX实体
    public class WX_AccessToken
    {
        //时差计算，返回：秒
        public static double TimeDiffer(DateTime begin, DateTime end)
        {
            var tb = new TimeSpan(begin.Ticks);
            var te = new TimeSpan(end.Ticks);
            return tb.Subtract(te).Duration().TotalSeconds;
        }

        public static string GetAccessToken()
        {
            try
            {
                //var db = DataCRUD<AccessToken>.Entity(a => true);

                ////1、检测库里的有没有超时，没超，直接使用，
                ////2、超时，删除，重新生成
                //if (db != null)
                //{
                //    //7200-1800，大约1.5小时
                //    if (TimeDiffer(DateTime.Now, db.create_time) > db.expires_in - 1800)
                //    {
                //        DataCRUD<AccessToken>.Delete(db);
                //    }
                //    else
                //    {
                //        return db.access_token;
                //    }
                //}

                ////读取token
                //var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", "wx8e6ce1260ba9f214", "a4ab64afec190ea5b618b6e8eec9c4ae");
                //var jsons = HtmlHelp.GetHtml(url, "UTF-8");

                ////生成新的数据记录
                //AccessToken token = Jsons.JsonEntity<AccessToken>(jsons);
                //token.create_time = DateTime.Now;

                ////保存
                //DataCRUD<AccessToken>.Add(token);

                ////返回
                //return token.access_token;
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static AccessTokenOauth SaveAccessTokenOauth(AccessTokenOauth auth)
        {
            try
            {
                var db = DataCRUD<AccessTokenOauth>.Entity(a => a.openid == auth.openid);
                if(db)
            }
            catch (Exception e)
            {                
                throw e;
            }
        }
    }
}