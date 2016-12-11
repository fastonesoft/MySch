using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TPageMap : EntityTypeConfiguration<TPage>
    {
        public TPageMap()
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

            this.Property(t => t.ThemeIDS)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("TPage");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Bootup).HasColumnName("Bootup");
            this.Property(t => t.ThemeIDS).HasColumnName("ThemeIDS");

            // Relationships
            this.HasRequired(t => t.Theme)
                .WithMany(t => t.TPages)
                .HasForeignKey(d => d.ThemeIDS);

        }
    }
}
