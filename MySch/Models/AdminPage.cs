namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminPage")]
    public partial class AdminPage
    {
        [Required]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [StringLength(20)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public bool Bootup { get; set; }

        [Required]
        public string Html { get; set; }

        public string Script { get; set; }

        public bool Fixed { get; set; }

        [Required]
        [StringLength(32)]
        public string ParentID { get; set; }
    }
}
