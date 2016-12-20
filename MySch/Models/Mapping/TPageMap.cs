using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TPageMap : EntityTypeConfiguration<TPage>
    {
        public TPageMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Html)
                .IsRequired();

            this.Property(t => t.ParentIDS)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("TPage");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Bootup).HasColumnName("Bootup");
            this.Property(t => t.Html).HasColumnName("Html");
            this.Property(t => t.Fixed).HasColumnName("Fixed");
            this.Property(t => t.ParentIDS).HasColumnName("ParentIDS");
        }
    }
}
