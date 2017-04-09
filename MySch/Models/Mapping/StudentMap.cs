using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class StudentMap : EntityTypeConfiguration<Student>
    {
        public StudentMap()
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
                .HasMaxLength(10);

            this.Property(t => t.IDC)
                .HasMaxLength(20);

            this.Property(t => t.FromSch)
                .HasMaxLength(32);

            this.Property(t => t.RegNo)
                .HasMaxLength(32);

            this.Property(t => t.OpenID)
                .HasMaxLength(32);

            this.Property(t => t.StepIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Mobil1)
                .HasMaxLength(20);

            this.Property(t => t.Mobil2)
                .HasMaxLength(20);

            this.Property(t => t.Name1)
                .HasMaxLength(20);

            this.Property(t => t.Name2)
                .HasMaxLength(20);

            this.Property(t => t.Home)
                .HasMaxLength(50);

            this.Property(t => t.Birth)
                .HasMaxLength(50);

            this.Property(t => t.Memo)
                .HasMaxLength(50);

            this.Property(t => t.AccIDS)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Student");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IDC).HasColumnName("IDC");
            this.Property(t => t.FromSch).HasColumnName("FromSch");
            this.Property(t => t.SchChoose).HasColumnName("SchChoose");
            this.Property(t => t.RegNo).HasColumnName("RegNo");
            this.Property(t => t.Reged).HasColumnName("Reged");
            this.Property(t => t.OpenID).HasColumnName("OpenID");
            this.Property(t => t.StepIDS).HasColumnName("StepIDS");
            this.Property(t => t.Mobil1).HasColumnName("Mobil1");
            this.Property(t => t.Mobil2).HasColumnName("Mobil2");
            this.Property(t => t.Name1).HasColumnName("Name1");
            this.Property(t => t.Name2).HasColumnName("Name2");
            this.Property(t => t.Home).HasColumnName("Home");
            this.Property(t => t.Birth).HasColumnName("Birth");
            this.Property(t => t.Checked).HasColumnName("Checked");
            this.Property(t => t.Fixed).HasColumnName("Fixed");
            this.Property(t => t.Memo).HasColumnName("Memo");
            this.Property(t => t.AccIDS).HasColumnName("AccIDS");
        }
    }
}
