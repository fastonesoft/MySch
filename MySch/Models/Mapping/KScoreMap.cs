using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class KScoreMap : EntityTypeConfiguration<KScore>
    {
        public KScoreMap()
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

            this.Property(t => t.KStudIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.KaoIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.SubIDS)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("KScore");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.KStudIDS).HasColumnName("KStudIDS");
            this.Property(t => t.KaoIDS).HasColumnName("KaoIDS");
            this.Property(t => t.SubIDS).HasColumnName("SubIDS");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.BanIndex).HasColumnName("BanIndex");
            this.Property(t => t.GradeIndex).HasColumnName("GradeIndex");
            this.Property(t => t.GroupIndex).HasColumnName("GroupIndex");
            this.Property(t => t.TotalIndex).HasColumnName("TotalIndex");
        }
    }
}
