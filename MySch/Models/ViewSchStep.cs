namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViewSchStep")]
    public partial class ViewSchStep
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(32)]
        public string IDS { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(32)]
        public string PartIDS { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(20)]
        public string Value { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool Graduated { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool CanRecruit { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(32)]
        public string AccIDS { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(10)]
        public string PartName { get; set; }
    }
}
