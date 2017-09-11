using MySch.Bll;
using MySch.Core;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.Wall
{
    public class VmWxAccPrize : BllEntity<WxAccPrize>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string WxAccIDS { get; set; }
        public string WxActionIDS { get; set; }
        public string WxPrizeIDS { get; set; }
    }

    public class VqWxAccPrize : BllBase<QrWxAccPrize>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string ActionName { get; set; }
        public string AccName { get; set; }
        public string AccImage { get; set; }
        public string PrizeName { get; set; }
    }
}