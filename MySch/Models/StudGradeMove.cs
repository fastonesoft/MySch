namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudGradeMove")]
    public partial class StudGradeMove
    {
        [Required]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [StringLength(32)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string BanIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string OwnerIDS { get; set; }

        public virtual TBan TBan { get; set; }

        public virtual TAcc TAcc { get; set; }
    }
}
