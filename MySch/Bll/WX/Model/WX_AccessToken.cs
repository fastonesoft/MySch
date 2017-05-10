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
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public DateTime create_time { get; set; }

        //时差计算，返回：秒
        public static string GetAccessToken()
        {
            try
            {
                //从全局缓存里读
                WX_AccessToken token = (WX_AccessToken)HttpContext.Current.Application["access_token"];
                if (token != null)
                {
                    if (WX_Time.TimeDiffer(DateTime.Now, token.create_time) < token.expires_in)
                    {
                        //有，且不超时，直接返回
                        return token.access_token;
                    }
                }

                //读取token
                //var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", "wx8e6ce1260ba9f214", "a4ab64afec190ea5b618b6e8eec9c4ae");
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", "wx01df6a9fe809485f", "c2ac6bc689b690f54d72f8479a26714b");
                var jsons = HtmlHelp.GetHtml(url, "UTF-8");
                token = Jsons.JsonEntity<WX_AccessToken>(jsons);
                //设置时间
                token.create_time = DateTime.Now;
                //保存
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application["access_token"] = token;
                HttpContext.Current.Application.UnLock();

                //返回
                return token.access_token;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}