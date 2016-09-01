using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class QBanMap : EntityTypeConfiguration<QBan>
    {
        public QBanMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.IDS, t.Name, t.GradeIDS, t.AccIDS });

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Name)
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

            this.Property(t => t.PartIDS)
                .HasMaxLength(20);

            this.Property(t => t.StepIDS)
                .HasMaxLength(20);

            this.Property(t => t.YearIDS)
                .HasMaxLength(20);

            this.Property(t => t.EduIDS)
                .HasMaxLength(20);

            this.Property(t => t.BanName)
                .HasMaxLength(84);

            this.Property(t => t.GradeName)
                .HasMaxLength(58);

            this.Property(t => t.PartStepName)
                .HasMaxLength(35);

            this.Property(t => t.PartName)
                .HasMaxLength(20);

            this.Property(t => t.StepName)
                .HasMaxLength(10);

            this.Property(t => t.EduName)
                .HasMaxLength(20);

            this.Property(t => t.MasterName)
                .HasMaxLength(20);

            this.Property(t => t.GroupName)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("QBan");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.GradeIDS).HasColumnName("GradeIDS");
            this.Property(t => t.MasterIDS).HasColumnName("MasterIDS");
            this.Property(t => t.GroupIDS).HasColumnName("GroupIDS");
            this.Property(t => t.AccIDS).HasColumnName("AccIDS");
            this.Property(t => t.PartStepIDS).HasColumnName("PartStepIDS");
            this.Property(t => t.PartIDS).HasColumnName("PartIDS");
            this.Property(t => t.StepIDS).HasColumnName("StepIDS");
            this.Property(t => t.YearIDS).HasColumnName("YearIDS");
            this.Property(t => t.EduIDS).HasColumnName("EduIDS");
            this.Property(t => t.BanName).HasColumnName("BanName");
            this.Property(t => t.GradeName).HasColumnName("GradeName");
            this.Property(t => t.PartStepName).HasColumnName("PartStepName");
            this.Property(t => t.PartName).HasColumnName("PartName");
            this.Property(t => t.StepName).HasColumnName("StepName");
            this.Property(t => t.EduName).HasColumnName("EduName");
            this.Property(t => t.MasterName).HasColumnName("MasterName");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
        }
    }
}
