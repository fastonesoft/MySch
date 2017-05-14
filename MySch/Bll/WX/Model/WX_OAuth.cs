using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_OAuth_Base
    {
        public string state { get; set; }
    }

    public class WX_OAuth : WX_OAuth_Base
    {
        public string code { get; set; }
    }
}