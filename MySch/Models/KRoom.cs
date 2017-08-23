namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KRoom")]
    public partial class KRoom
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
        [StringLength(5)]
        public string Value { get; set; }

        public int Hold { get; set; }

        public int BeginNum { get; set; }

        [Required]
        [StringLength(20)]
        public string TypeIDS { get; set; }
    }
}
