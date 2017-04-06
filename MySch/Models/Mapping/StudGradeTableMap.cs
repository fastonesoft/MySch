using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class StudGradeTableMap : EntityTypeConfiguration<StudGradeTable>
    {
        public StudGradeTableMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.IDS, t.GradeIDS, t.TableName, t.TypeIDS });

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

            this.Property(t => t.TableName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.TypeIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Memo)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("StudGradeTable");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.GradeIDS).HasColumnName("GradeIDS");
            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.TypeIDS).HasColumnName("TypeIDS");
            this.Property(t => t.Memo).HasColumnName("Memo");
        }
    }
}
