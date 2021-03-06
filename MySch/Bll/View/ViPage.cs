﻿using MySch.Core;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class ViPage : BllBase<AdminPage>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public bool Bootup { get; set; }
        public bool Fixed { get; set; }
        public string ParentID { get; set; }
    }
}