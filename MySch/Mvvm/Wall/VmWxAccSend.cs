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
    public class VmWxAccSend : BllEntity<WxAccSend>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string WxAccIDS { get; set; }
        public string WxActionIDS { get; set; }
        public DateTime CreateTime { get; set; }
        public string SendMsg { get; set; }
        public string MsgType { get; set; }
    }

    public class VmWxAccSendToShow : BllEntity<WxAccSend>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public bool Showed { get; set; }
    }

    public class VqWxAccSend : BllBase<ViewWxAccSend>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string WxAccIDS { get; set; }
        public string ActionName { get; set; }
        public string AccName { get; set; }
        public string AccImage { get; set; }
        public bool Showed { get; set; }
        public DateTime CreateTime { get; set; }
        public string MsgType { get; set; }
        public string SendMsg { get; set; }
    }
}