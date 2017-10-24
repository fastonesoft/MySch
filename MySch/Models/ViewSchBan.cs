namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViewSchBan")]
    public partial class ViewSchBan
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string IDS { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string Num { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(32)]
        public string GradeIDS { get; set; }

        [StringLength(32)]
        public string MasterIDS { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool NotFeng { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool OnlyFixed { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChangeNum { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Differ { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool IsAbs { get; set; }

        [Key]
        [Column(Order = 9)]
        public bool SameSex { get; set; }

        [StringLength(20)]
        public string MasterName { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(46)]
        public string GradeName { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TakeNum { get; set; }

        [Key]
        [Column(Order = 12)]
        public bool CurrentYear { get; set; }

        [Key]
        [Column(Order = 13)]
        public bool Graduated { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(32)]
        public string AccIDS { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(32)]
        public string PartIDS { get; set; }
    }
}
