using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll
{

    /// <summary>
    /// 数据排序方式
    /// </summary>
    public enum OrderType { ASC, DESC }

    public class LoginModel
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
        public bool Student { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
    }

    //出错返回类
    public class BllError
    {
        public bool error { get; set; }
        public object message { get; set; }
    }
}