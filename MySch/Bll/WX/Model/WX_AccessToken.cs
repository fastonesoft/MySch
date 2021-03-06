﻿using MySch.Core;
using MySch.Dal;
using MySch.Helper;
using MySch.Models;
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
                var token = (WX_AccessToken)HttpContext.Current.Application["access_token"];
                if (token != null)
                {
                    if (WX_Time.TimeDiffer(DateTime.Now, token.create_time) < token.expires_in)
                    {
                        //有，且不超时，直接返回
                        return token.access_token;
                    }
                }

                token = WX_Url.AccessToken(WX_Const.goneAppID, WX_Const.goneAppSecret);
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