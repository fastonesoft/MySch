using MySch.Core;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MySch.Bll.View
{
    public class ViDatum: BllBase<AdminDatum>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
    }
}