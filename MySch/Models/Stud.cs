namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stud")]
    public partial class Stud
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stud()
        {
            StudGrades = new HashSet<StudGrade>();
        }

        [Required]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [StringLength(32)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string IDC { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string StepIDS { get; set; }

        [StringLength(64)]
        public string FromSch { get; set; }

        public bool SchChoose { get; set; }

        [StringLength(10)]
        public string RegNo { get; set; }

        [StringLength(32)]
        public string RegUID { get; set; }

        public bool Examed { get; set; }

        [StringLength(32)]
        public string ExamUID { get; set; }

        [StringLength(32)]
        public string ExamUIDe { get; set; }

        [StringLength(20)]
        public string Mobil1 { get; set; }

        [StringLength(20)]
        public string Mobil2 { get; set; }

        [StringLength(20)]
        public string Name1 { get; set; }

        [StringLength(20)]
        public string Name2 { get; set; }

        [StringLength(50)]
        public string Home { get; set; }

        [StringLength(50)]
        public string Birth { get; set; }

        public bool Fixed { get; set; }

        [StringLength(50)]
        public string Memo { get; set; }

        public virtual TStep TStep { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudGrade> StudGrades { get; set; }
    }
}
