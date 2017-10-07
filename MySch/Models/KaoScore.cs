namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KaoScore")]
    public partial class KaoScore
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KaoScore()
        {
            KaoScoreAnas = new HashSet<KaoScoreAna>();
        }

        [Required]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [StringLength(32)]
        public string IDS { get; set; }

        [Required]
        [StringLength(32)]
        public string KaoSubIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string GradeStudIDS { get; set; }

        public double? Value { get; set; }

        public virtual StudGrade StudGrade { get; set; }

        public virtual KaoSub KaoSub { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KaoScoreAna> KaoScoreAnas { get; set; }
    }
}
