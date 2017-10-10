namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TGrade")]
    public partial class TGrade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TGrade()
        {
            StudGrades = new HashSet<StudGrade>();
            TBans = new HashSet<TBan>();
        }

        [Required]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [StringLength(32)]
        public string IDS { get; set; }

        [Required]
        [StringLength(32)]
        public string StepIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string YearIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string EduIDS { get; set; }

        public bool CanFeng { get; set; }

        public int TakeNum { get; set; }

        public bool GoneModel { get; set; }

        public string GoneList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudGrade> StudGrades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBan> TBans { get; set; }

        public virtual TEdu TEdu { get; set; }

        public virtual TStep TStep { get; set; }

        public virtual TYear TYear { get; set; }
    }
}
