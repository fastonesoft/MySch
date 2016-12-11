using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TColumnMap : EntityTypeConfiguration<TColumn>
    {
        public TColumnMap()
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

            this.Property(t => t.Html)
                .IsRequired();

            this.Property(t => t.PageIDS)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("TColumn");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Html).HasColumnName("Html");
            this.Property(t => t.Fixed).HasColumnName("Fixed");
            this.Property(t => t.PageIDS).HasColumnName("PageIDS");

            // Relationships
            this.HasRequired(t => t.TPage)
                .WithMany(t => t.TColumns)
                .HasForeignKey(d => d.PageIDS);

        }
    }
}
