using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_Signature
    {

        public string appid { get; set; }
        public long timestamp { get; set; }
        public string noncestr { get; set; }
        public string signature { get; set; }
        public string idc { get; set; }
        public string name { get; set; }
        public string regno { get; set; }
        public bool exam { get; set; }
        public string examuid { get; set; }
        public string rexamuid { get; set; }

        public WX_Signature(string sappid, string sticket, string surl, string sidc, string sname,string sregno, bool bexam, string sexamuid, string srexamuid)
        {
            appid = sappid;
            //
            timestamp = WXApi.GetTimestamp();
            noncestr = Guid.NewGuid().ToString("N");
            //
            var str = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", sticket, noncestr, timestamp, surl);

            //
            signature = Setting.GetSHA1(str).ToLower();
            //绑定的学生
            idc = sidc;
            name = sname;
            exam = bexam;
            regno = sregno;
            //
            examuid = sexamuid;
            rexamuid = srexamuid;
        }
    }
}