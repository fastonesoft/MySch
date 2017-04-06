using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class StudGradeFieldMap : EntityTypeConfiguration<StudGradeField>
    {
        public StudGradeFieldMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.IDS, t.TableIDS, t.FieldName });

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.TableIDS)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.FieldName)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("StudGradeField");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.TableIDS).HasColumnName("TableIDS");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
        }
    }
}
