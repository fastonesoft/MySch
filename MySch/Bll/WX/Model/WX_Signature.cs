using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_Signature
    {

        public string appId { get; set; }
        public string jsticket { get; set; }
        public long timestamp { get; set; }
        public string noncestr { get; set; }
        public string signature { get; set; }

        public WX_Signature(string sappid, string sticket, string surl)
        {
            appId = sappid;
            jsticket = sticket;
            //
            timestamp = WXApi.GetTimestamp();
            noncestr = Guid.NewGuid().ToString("N");
            //
            var str = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url=http://a.jysycz.cn{3}", sticket, noncestr, timestamp, surl);
            //
            signature = Setting.GetSHA1(str);
        }
    }
}