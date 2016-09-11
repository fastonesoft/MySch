using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class QGradeStudMap : EntityTypeConfiguration<QGradeStud>
    {
        public QGradeStudMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.GradeIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.StudIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.BanIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.OldBan)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ComeIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.GroupID)
                .HasMaxLength(32);

            this.Property(t => t.GradeName)
                .HasMaxLength(46);

            this.Property(t => t.StepEduName)
                .HasMaxLength(33);

            this.Property(t => t.StudName)
                .HasMaxLength(10);

            this.Property(t => t.StudSex)
                .HasMaxLength(2);

            this.Property(t => t.CID)
                .HasMaxLength(20);

            this.Property(t => t.ComeName)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("QGradeStud");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.GradeIDS).HasColumnName("GradeIDS");
            this.Property(t => t.StudIDS).HasColumnName("StudIDS");
            this.Property(t => t.BanIDS).HasColumnName("BanIDS");
            this.Property(t => t.OldBan).HasColumnName("OldBan");
            this.Property(t => t.Choose).HasColumnName("Choose");
            this.Property(t => t.ComeIDS).HasColumnName("ComeIDS");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.Fixed).HasColumnName("Fixed");
            this.Property(t => t.Score).HasColumnName("Score");
            this.Property(t => t.GradeName).HasColumnName("GradeName");
            this.Property(t => t.StepEduName).HasColumnName("StepEduName");
            this.Property(t => t.Graduated).HasColumnName("Graduated");
            this.Property(t => t.StudName).HasColumnName("StudName");
            this.Property(t => t.StudSex).HasColumnName("StudSex");
            this.Property(t => t.CID).HasColumnName("CID");
            this.Property(t => t.ComeName).HasColumnName("ComeName");
            this.Property(t => t.Checked).HasColumnName("Checked");
        }
    }
}
