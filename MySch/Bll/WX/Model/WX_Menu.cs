using MySch.Bll.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_Menu
    {
        List<object> _button = new List<object>();
        public List<object> button { get { return _button; } }
        public object Add(object obj)
        {
            _button.Add(obj);
            return obj;
        }
    }

    public class WX_Menu_Click
    {
        public string type { get; set; }
        public string name { get; set; }
        public string key { get; set; }
    }

    public class WX_Menu_View
    {
        public string type { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class WX_Menu_Sub
    {
        List<object> _sub = new List<object>();

        public string name { get; set; }
        public List<object> sub_button { get { return _sub; } }

        public void Add(object sub)
        {
            _sub.Add(sub);
        }
    }
}