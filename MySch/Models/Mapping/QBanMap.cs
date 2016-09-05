using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class QBanMap : EntityTypeConfiguration<QBan>
    {
        public QBanMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.IDS, t.Num, t.GradeIDS, t.AccIDS, t.Graduated });

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Num)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.GradeIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.MasterIDS)
                .HasMaxLength(20);

            this.Property(t => t.GroupIDS)
                .HasMaxLength(20);

            this.Property(t => t.AccIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PartStepIDS)
                .HasMaxLength(20);

            this.Property(t => t.YearIDS)
                .HasMaxLength(20);

            this.Property(t => t.EduIDS)
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .HasMaxLength(49);

            this.Property(t => t.GradeName)
                .HasMaxLength(38);

            this.Property(t => t.MasterName)
                .HasMaxLength(20);

            this.Property(t => t.GroupName)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("QBan");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.Num).HasColumnName("Num");
            this.Property(t => t.GradeIDS).HasColumnName("GradeIDS");
            this.Property(t => t.MasterIDS).HasColumnName("MasterIDS");
            this.Property(t => t.GroupIDS).HasColumnName("GroupIDS");
            this.Property(t => t.AccIDS).HasColumnName("AccIDS");
            this.Property(t => t.PartStepIDS).HasColumnName("PartStepIDS");
            this.Property(t => t.YearIDS).HasColumnName("YearIDS");
            this.Property(t => t.EduIDS).HasColumnName("EduIDS");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.GradeName).HasColumnName("GradeName");
            this.Property(t => t.MasterName).HasColumnName("MasterName");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.Graduated).HasColumnName("Graduated");
        }
    }
}
