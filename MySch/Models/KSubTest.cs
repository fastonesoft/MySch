namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KSubTest")]
    public partial class KSubTest
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(20)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string KaoIDS { get; set; }

        [Required]
        [StringLength(20)]
        public string SubIDS { get; set; }

        public int Value { get; set; }

        public bool Scoring { get; set; }
    }
}
