namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KaoSub")]
    public partial class KaoSub
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KaoSub()
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
        public string KaoIDS { get; set; }

        [Required]
        [StringLength(20)]
        public string SubIDS { get; set; }

        public bool AddTotal { get; set; }

        public int MaxScore { get; set; }

        public bool CanSum { get; set; }

        public bool Fixed { get; set; }

        public virtual Kao Kao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KaoScore> KaoScores { get; set; }

        public virtual TSub TSub { get; set; }
    }
}
