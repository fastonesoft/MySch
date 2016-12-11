using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class TPage
    {
        public TPage()
        {
            this.TColumns = new List<TColumn>();
        }

        public string IDS { get; set; }
        public string Name { get; set; }
        public bool Bootup { get; set; }
        public string ThemeIDS { get; set; }
        public virtual ICollection<TColumn> TColumns { get; set; }
        public virtual Theme Theme { get; set; }
    }
}
