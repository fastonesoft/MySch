using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class WxUploadFileMap : EntityTypeConfiguration<WxUploadFile>
    {
        public WxUploadFileMap()
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

            this.Property(t => t.FileType)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.UploadType)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Author)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Memo)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("WxUploadFile");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.FileType).HasColumnName("FileType");
            this.Property(t => t.UploadType).HasColumnName("UploadType");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.Author).HasColumnName("Author");
            this.Property(t => t.Memo).HasColumnName("Memo");
        }
    }
}
