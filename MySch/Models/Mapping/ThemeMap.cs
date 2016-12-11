using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class ThemeMap : EntityTypeConfiguration<Theme>
    {
        public ThemeMap()
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

            // Table & Column Mappings
            this.ToTable("Theme");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsCurrent).HasColumnName("IsCurrent");
        }
    }
}
