using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MySch.Bll.WX.Model
{
    //命令行
    public class WX_Command
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public static WX_Command GetCommand(string regs, string command)
        {
            try
            {
                Regex regex = new Regex(regs);
                Match match = regex.Match(command);
                //
                return match.Success ? new WX_Command { Name = match.Groups["name"].ToString() } : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    //命令记录
    public class WX_Command_Rec
    {
        public bool IDC { get; set; }
        public bool Mobil1 { get; set; }
        public bool Mobil2 { get; set; }

        public static WX_Command_Rec GetFromOpenID(string openID)
        {
            try
            {
                //没记录
                var db = DataCRUD<Student>.Entity(a => a.OpenID == openID);
                if (db == null)
                {
                    return new WX_Command_Rec
                    {
                        IDC = false,
                        Mobil1 = false,
                        Mobil2 = false
                    };
                }
                else
                {
                    return new WX_Command_Rec
                    {
                        IDC = !string.IsNullOrEmpty(db.IDC),
                        Mobil1 = !string.IsNullOrEmpty(db.Mobil1),
                        Mobil2 = !string.IsNullOrEmpty(db.Mobil2),
                    };
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}