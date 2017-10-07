namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TTerm")]
    public partial class TTerm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TTerm()
        {
            Kaos = new HashSet<Kao>();
        }

        [Required]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [StringLength(20)]
        public string IDS { get; set; }

        public bool IsCurrent { get; set; }

        [Required]
        [StringLength(20)]
        public string YearIDS { get; set; }

        [Required]
        [StringLength(20)]
        public string SemesterIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string AccIDS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kao> Kaos { get; set; }

        public virtual TAcc TAcc { get; set; }

        public virtual TSemester TSemester { get; set; }

        public virtual TYear TYear { get; set; }
    }
}
