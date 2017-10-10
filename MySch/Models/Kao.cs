namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kao")]
    public partial class Kao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kao()
        {
            KaoSubs = new HashSet<KaoSub>();
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

        [Required]
        [StringLength(32)]
        public string TermIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string KaoTypeIDS { get; set; }

        public DateTime CreateTime { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        public bool KaoCoded { get; set; }

        public virtual TAcc TAcc { get; set; }

        public virtual KaoType KaoType { get; set; }

        public virtual TTerm TTerm { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KaoSub> KaoSubs { get; set; }
    }
}
