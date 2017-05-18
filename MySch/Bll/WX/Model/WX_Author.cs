using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    //验证类
    public class WX_Author
    {
        public string timestamp { get; set; }
        public string nonce { get; set; }
        public string signature { get; set; }
        public string echostr { get; set; }

        public override string ToString()
        {
            return string.Format("timestamp:{0},nonce:{1},signature:{2},echostr:{3}", timestamp, nonce, signature, echostr);
        }
    }

    public class WX_Author_Ex : WX_Author
    {
        //作不同的参数，用以区分不同的Control
        public string encrypt_type { get; set; }
        public string msg_signature { get; set; }

        public override string ToString()
        {
            var s = base.ToString();
            return string.Format("{0},encrypt_type:{1},msg_signature:{2}", s, encrypt_type, msg_signature);
        }
    }
}