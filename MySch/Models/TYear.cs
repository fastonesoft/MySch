namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TYear")]
    public partial class TYear
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(20)]
        public string IDS { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public bool IsCurrent { get; set; }

        [Required]
        [StringLength(32)]
        public string AccIDS { get; set; }
    }
}
