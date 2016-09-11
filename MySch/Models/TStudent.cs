using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class TStudent
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public string CID { get; set; }
        public string FromSch { get; set; }
        public string FromGrade { get; set; }
        public string NationID { get; set; }
        public string ReadState { get; set; }
        public bool IsProblem { get; set; }
        public string PartStepIDS { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> Come { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public string Mobil1 { get; set; }
        public string Mobil2 { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Home { get; set; }
        public string Birth { get; set; }
        public string Memo { get; set; }
        public bool Checked { get; set; }
        public string AccIDS { get; set; }
        public string OpenID { get; set; }
    }
}
