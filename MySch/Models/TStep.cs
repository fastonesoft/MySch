namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TStep")]
    public partial class TStep
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(20)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Value { get; set; }

        public bool Graduated { get; set; }

        public bool CanRecruit { get; set; }

        [Required]
        [StringLength(20)]
        public string PartIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string AccIDS { get; set; }
    }
}
