namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TYear")]
    public partial class TYear
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TYear()
        {
            TGrades = new HashSet<TGrade>();
            TTerms = new HashSet<TTerm>();
        }

        [Required]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [StringLength(32)]
        public string IDS { get; set; }

        [Required]
        [StringLength(32)]
        public string AccIDS { get; set; }

        public int Name { get; set; }

        public bool IsCurrent { get; set; }

        public virtual TAcc TAcc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TGrade> TGrades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TTerm> TTerms { get; set; }
    }
}
