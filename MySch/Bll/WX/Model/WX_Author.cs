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
    }

    public class WX_Author_Ex : WX_Author
    {
        //作不同的参数，用以区分不同的Control
        public string encrypt_type { get; set; }
        public string msg_signature { get; set; }
    }
}