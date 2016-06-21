using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class Theme
    {
        public string ID { get; set; }
        public string GD { get; set; }
        public string Name { get; set; }
        public bool Defaulted { get; set; }
    }
}
