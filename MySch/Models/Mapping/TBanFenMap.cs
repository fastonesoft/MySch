using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TBanFenMap : EntityTypeConfiguration<TBanFen>
    {
        public TBanFenMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.IDS, t.GradeOldIDS, t.GradeNewIDS, t.BanOldIDS });

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.GradeOldIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.GradeNewIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.BanOldIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.BanNewIDS)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("TBanFen");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.GradeOldIDS).HasColumnName("GradeOldIDS");
            this.Property(t => t.GradeNewIDS).HasColumnName("GradeNewIDS");
            this.Property(t => t.BanOldIDS).HasColumnName("BanOldIDS");
            this.Property(t => t.BanNewIDS).HasColumnName("BanNewIDS");
            this.Property(t => t.Total).HasColumnName("Total");
        }
    }
}
