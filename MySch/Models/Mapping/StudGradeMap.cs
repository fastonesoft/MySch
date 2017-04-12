using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class StudGradeMap : EntityTypeConfiguration<StudGrade>
    {
        public StudGradeMap()
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

            this.Property(t => t.BanIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.OldBan)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.StudIDS)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.StudCode)
                .HasMaxLength(20);

            this.Property(t => t.ComeIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.GroupID)
                .HasMaxLength(32);

            this.Property(t => t.OutIDS)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("StudGrade");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.GradeIDS).HasColumnName("GradeIDS");
            this.Property(t => t.BanIDS).HasColumnName("BanIDS");
            this.Property(t => t.OldBan).HasColumnName("OldBan");
            this.Property(t => t.StudIDS).HasColumnName("StudIDS");
            this.Property(t => t.StudCode).HasColumnName("StudCode");
            this.Property(t => t.Choose).HasColumnName("Choose");
            this.Property(t => t.ComeIDS).HasColumnName("ComeIDS");
            this.Property(t => t.ComeTime).HasColumnName("ComeTime");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.Fixed).HasColumnName("Fixed");
            this.Property(t => t.Score).HasColumnName("Score");
            this.Property(t => t.OutIDS).HasColumnName("OutIDS");
            this.Property(t => t.OutTime).HasColumnName("OutTime");
            this.Property(t => t.InSch).HasColumnName("InSch");
        }
    }
}
