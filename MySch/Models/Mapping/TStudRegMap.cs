using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TStudRegMap : EntityTypeConfiguration<TStudReg>
    {
        public TStudRegMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.GD)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.fromSch)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.fromPhoto)
                .HasMaxLength(50);

            this.Property(t => t.Memo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TStudReg");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.GD).HasColumnName("GD");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.fromSch).HasColumnName("fromSch");
            this.Property(t => t.fromClass).HasColumnName("fromClass");
            this.Property(t => t.fromPhoto).HasColumnName("fromPhoto");
            this.Property(t => t.schChoose).HasColumnName("schChoose");
            this.Property(t => t.Memo).HasColumnName("Memo");
        }
    }
}
