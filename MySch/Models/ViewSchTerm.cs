namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViewSchTerm")]
    public partial class ViewSchTerm
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
        public string YearIDS { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(32)]
        public string TermTypeIDS { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool CurrentTerm { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Name { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool CurrentYear { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(32)]
        public string AccIDS { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(20)]
        public string TypeName { get; set; }
    }
}
