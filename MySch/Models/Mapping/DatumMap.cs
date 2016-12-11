using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class DatumMap : EntityTypeConfiguration<Datum>
    {
        public DatumMap()
        {
            // Primary Key
            this.HasKey(t => t.IDS);

            // Properties
            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Command)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Datum");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Command).HasColumnName("Command");
        }
    }
}
