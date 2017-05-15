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
        public int Image { get; set; }
        public int Mobil { get; set; }

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
                        Mobil = 0,
                        Image = 0,
                    };
                }
                else
                {
                    return new WX_Command_Rec
                    {
                        IDC = !string.IsNullOrEmpty(db.IDC),
                        Mobil = Convert.ToInt32(!string.IsNullOrEmpty(db.Mobil1)) + Convert.ToInt32(!string.IsNullOrEmpty(db.Mobil2)),
                        Image = DataCRUD<WxUploadFile>.Count(a => a.Author == openID),
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