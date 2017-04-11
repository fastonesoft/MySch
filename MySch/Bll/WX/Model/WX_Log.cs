using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_Log
    {
        public static void Add(string posts)
        {
            //记录
            TLog log = new TLog();
            log.CreateTime = DateTime.Now;
            log.GD = Guid.NewGuid().ToString("N");
            log.Value = posts;
            DataCRUD<TLog>.Add(log);
        }
    }
}