using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_Signature
    {

        public string appId{get;set;}
        public long timestamp{get;set;}
        public string signature{get;set;}

        public  WX_Signature(string sappid, string surl)
        {
            appId = sappid;
            //
        }
    }
}