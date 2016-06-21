using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TFileInforMap : EntityTypeConfiguration<TFileInfor>
    {
        public TFileInforMap()
        {
            // Primary Key
            this.HasKey(t => t.Name);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.fileClass)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.fileAuthor)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("TFileInfor");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.fileClass).HasColumnName("fileClass");
            this.Property(t => t.fileAuthor).HasColumnName("fileAuthor");
            this.Property(t => t.updateTime).HasColumnName("updateTime");
        }
    }
}
