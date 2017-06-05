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
}