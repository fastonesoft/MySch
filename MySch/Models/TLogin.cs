namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TLogin")]
    public partial class TLogin
    {
        [Required]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [StringLength(32)]
        public string IDS { get; set; }

        [Required]
        [StringLength(32)]
        public string IP { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string Pwd { get; set; }

        [Required]
        [StringLength(32)]
        public string Brower { get; set; }

        [Required]
        [StringLength(32)]
        public string LoginMsg { get; set; }

        public DateTime LoginTime { get; set; }
    }
}
