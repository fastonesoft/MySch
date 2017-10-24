namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViewSchPart")]
    public partial class ViewSchPart
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
        public string AccIDS { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(2)]
        public string Value { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool Fixed { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(20)]
        public string AccName { get; set; }
    }
}
