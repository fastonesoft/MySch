namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KScore")]
    public partial class KScore
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(20)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string KStudIDS { get; set; }

        [Required]
        [StringLength(20)]
        public string KaoIDS { get; set; }

        [Required]
        [StringLength(20)]
        public string SubIDS { get; set; }

        public double? Value { get; set; }

        public int? BanIndex { get; set; }

        public int? GradeIndex { get; set; }

        public int? GroupIndex { get; set; }

        public int? TotalIndex { get; set; }
    }
}
