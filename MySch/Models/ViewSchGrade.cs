namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViewSchGrade")]
    public partial class ViewSchGrade
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
        public string StepIDS { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(32)]
        public string YearIDS { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(32)]
        public string EduIDS { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool CanFeng { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TakeNum { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool GoneModel { get; set; }

        public string GoneList { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(32)]
        public string PartIDS { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(20)]
        public string AccName { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(10)]
        public string PartName { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(20)]
        public string StepName { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(10)]
        public string EduName { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(33)]
        public string TreeName { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(46)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 15)]
        public bool CurrentYear { get; set; }
    }
}
