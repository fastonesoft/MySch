namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudGrade")]
    public partial class StudGrade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudGrade()
        {
            KaoScores = new HashSet<KaoScore>();
        }

        [Required]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [StringLength(32)]
        public string IDS { get; set; }

        [Required]
        [StringLength(32)]
        public string GradeIDS { get; set; }

        [Required]
        [StringLength(20)]
        public string BanIDS { get; set; }

        [Required]
        [StringLength(10)]
        public string OldBan { get; set; }

        [Required]
        [StringLength(32)]
        public string StudIDS { get; set; }

        [StringLength(20)]
        public string StudCode { get; set; }

        public bool Choose { get; set; }

        [Required]
        [StringLength(20)]
        public string ComeIDS { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ComeTime { get; set; }

        [StringLength(32)]
        public string GroupID { get; set; }

        public bool Fixed { get; set; }

        public int? Score { get; set; }

        [StringLength(20)]
        public string OutIDS { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OutTime { get; set; }

        public bool InSch { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KaoScore> KaoScores { get; set; }

        public virtual Stud Stud { get; set; }

        public virtual StudCome StudCome { get; set; }

        public virtual TBan TBan { get; set; }

        public virtual TGrade TGrade { get; set; }
    }
}
