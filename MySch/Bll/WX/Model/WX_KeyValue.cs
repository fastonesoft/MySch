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
        public object value { get; set; }
        public List<WX_KeyValue> lists { get { return _lists; } }

        public WX_KeyValue Add(string skey, object svalue)
        {
            var res = new WX_KeyValue { key = skey, value = svalue };
            _lists.Add(res);

            return res;
        }
    }

    public class WX_Key
    {
        public object key { get; set; }
        public object value { get; set; }
    }
}