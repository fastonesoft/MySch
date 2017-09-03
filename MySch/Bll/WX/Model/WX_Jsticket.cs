using MySch.Core;
using MySch.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_Jsticket
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public int expires_in { get; set; }
        public DateTime create_time { get; set; }

        public static string GetJsticket(string accessToken)
        {
            try
            {
                //从全局缓存里读
                WX_Jsticket token = (WX_Jsticket)HttpContext.Current.Application["jsapi_ticket"];
                if (token != null)
                {
                    if (WX_Time.TimeDiffer(DateTime.Now, token.create_time) < token.expires_in)
                    {
                        //有，且不超时，直接返回
                        return token.ticket;
                    }
                }
                
                //读取jsticket
                token = WX_Url.Jsticket(accessToken);
                //设置时间
                token.create_time = DateTime.Now;
                //保存
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application["jsapi_ticket"] = token;
                HttpContext.Current.Application.UnLock();

                //返回
                return token.ticket;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}