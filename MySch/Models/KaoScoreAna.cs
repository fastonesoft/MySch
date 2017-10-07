namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KaoScoreAna")]
    public partial class KaoScoreAna
    {
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
        public string KaoAnaIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string KaoScoreIDS { get; set; }

        public int AnaResult { get; set; }

        public virtual KaoScore KaoScore { get; set; }

        public virtual TAcc TAcc { get; set; }
    }
}
