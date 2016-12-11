using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class Theme
    {
        public Theme()
        {
            this.TPages = new List<TPage>();
        }

        public string IDS { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
        public virtual ICollection<TPage> TPages { get; set; }
    }
}
