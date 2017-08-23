namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QrBanCurrent")]
    public partial class QrBanCurrent
    {
        [Key]
        [StringLength(32)]
        public string AccIDS { get; set; }

        [StringLength(32)]
        public string MasterIDS { get; set; }

        public bool? IsCurrent { get; set; }
    }
}
