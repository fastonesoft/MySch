using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class TKey
    {
        public string ID { get; set; }
        public string GD { get; set; }
        public string Name { get; set; }
        public bool Fixed { get; set; }
        public string KeyClassID { get; set; }
    }
}
