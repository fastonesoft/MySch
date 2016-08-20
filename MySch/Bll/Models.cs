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

    public class BrowserModel
    {
        public string IP { get; set; }
        public string DNS { get; set; }
        public string Browser { get; set; }
        public BrowserModel(string ip, string dns, string browser)
        {
            IP = ip;
            DNS = dns;
            Browser = browser;
        }
    }

    public class LoginModel
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
        public BrowserModel Browser { get; set; }
        public LoginModel(string ip, string dns, string browser, BllAcc acc)
        {
            ID = acc.ID;
            IDS = acc.IDS;
            Name = acc.Name;
            Parent = acc.Parent;
            Browser = new BrowserModel(ip, dns, browser);
        }
    }

    //出错返回类
    public class BllError
    {
        public bool error { get; set; }
        public string message { get; set; }
    }

}