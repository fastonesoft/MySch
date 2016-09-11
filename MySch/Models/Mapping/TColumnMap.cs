using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TColumnMap : EntityTypeConfiguration<TColumn>
    {
        public TColumnMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.GD, t.Name, t.Html, t.Txt, t.Fixed, t.PageID });

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.GD)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Html)
                .IsRequired();

            this.Property(t => t.Txt)
                .IsRequired();

            this.Property(t => t.PageID)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("TColumn");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.GD).HasColumnName("GD");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Html).HasColumnName("Html");
            this.Property(t => t.Txt).HasColumnName("Txt");
            this.Property(t => t.Fixed).HasColumnName("Fixed");
            this.Property(t => t.PageID).HasColumnName("PageID");
        }
    }
}
