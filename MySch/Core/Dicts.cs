using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MySch.Core
{
    public class Dicts
    {
        Dictionary<string, string> dicts = new Dictionary<string, string>();

        public void Add(string key, string value)
        {
            dicts.Add(key, value);
        }

        public string ToPost(Encoding encoding)
        {
            var res = string.Empty;
            foreach (var dict in dicts)
            {
                var key = dict.Key;
                var value = HttpUtility.UrlEncode(dict.Value, encoding);
                res += string.Format("{0}={1}&", key, value);
            }
            return res == string.Empty ? res : res.Substring(0, res.Length - 1);
        }

        public string ToPost(string encodingName)
        {
            return ToPost(Encoding.GetEncoding(encodingName));
        }
    }
}