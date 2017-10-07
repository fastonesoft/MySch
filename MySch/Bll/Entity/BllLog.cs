using MySch.Dal;
using MySch.Models;
using System;

namespace MySch.Bll.Entity
{
    public class BllLog
    {
        public static void Add(string log)
        {
            try
            {
                var db = new TLog { GD = Guid.NewGuid().ToString("N"), CreateTime = DateTime.Now, Value = log };
                DataCRUD<TLog>.Add(db);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}