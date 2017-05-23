using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_KeyValue
    {
        List<WX_KeyValue> _lists = new List<WX_KeyValue>();

        public string key { get; set; }
        public string value { get; set; }
        public List<WX_KeyValue> lists { get { return _lists; } }

        public void Add(string skey, string svalue)
        {
            _lists.Add(new WX_KeyValue { key = skey, value = svalue });
        }
    }
}