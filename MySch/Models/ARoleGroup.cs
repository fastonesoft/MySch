namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ARoleGroup")]
    public partial class ARoleGroup
    {
        [StringLength(32)]
        public string ID { get; set; }

        public int IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public string GroupRole { get; set; }
    }
}
