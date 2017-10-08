using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.Custom
{

    //出错返回类
    public class ErrorMessage
    {
        public bool error { get; set; }
        public object message { get; set; }
    }
}