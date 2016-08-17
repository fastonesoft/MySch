using MySch.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.ModelsEx
{
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

    public class ResultModel
    {
        public bool ok { get; set; }
        public string message { get; set; }
        public object result { get; set; }

        public ResultModel(bool o, string m, object r)
        {
            ok = o;
            message = m;
            result = r;
        }
    }

    public class ErrorModel
    {
        public bool error { get; set; }
        public string message { get; set; }
    }




}