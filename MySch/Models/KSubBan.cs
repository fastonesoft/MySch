namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KSubBan")]
    public partial class KSubBan
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(32)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string BanIDS { get; set; }

        [Required]
        [StringLength(20)]
        public string SubGradeIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string AccIDS { get; set; }

        public bool IsMaster { get; set; }
    }
}
