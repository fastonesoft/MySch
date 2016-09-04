using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class QBanStudMap : EntityTypeConfiguration<QBanStud>
    {
        public QBanStudMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.IDS, t.BanIDS, t.StudIDS, t.Graduated });

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.BanIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.StudIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.GradeIDS)
                .HasMaxLength(20);

            this.Property(t => t.GradeName)
                .HasMaxLength(38);

            this.Property(t => t.StudID)
                .HasMaxLength(32);

            this.Property(t => t.StudName)
                .HasMaxLength(20);

            this.Property(t => t.StudNo)
                .HasMaxLength(32);

            this.Property(t => t.StudSex)
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("QBanStud");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.BanIDS).HasColumnName("BanIDS");
            this.Property(t => t.StudIDS).HasColumnName("StudIDS");
            this.Property(t => t.GradeIDS).HasColumnName("GradeIDS");
            this.Property(t => t.GradeName).HasColumnName("GradeName");
            this.Property(t => t.Graduated).HasColumnName("Graduated");
            this.Property(t => t.StudID).HasColumnName("StudID");
            this.Property(t => t.StudName).HasColumnName("StudName");
            this.Property(t => t.StudNo).HasColumnName("StudNo");
            this.Property(t => t.StudSex).HasColumnName("StudSex");
        }
    }
}
