﻿using MySch.Models;

namespace MySch.Bll.View
{
    public class ViBanTree: BllBase<ViewBan>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
    }
}