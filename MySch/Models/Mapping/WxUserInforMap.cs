using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class WxUserInforMap : EntityTypeConfiguration<WxUserInfor>
    {
        public WxUserInforMap()
        {
            // Primary Key
            this.HasKey(t => t.openid);

            // Properties
            this.Property(t => t.openid)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.nickname)
                .HasMaxLength(32);

            this.Property(t => t.sex)
                .HasMaxLength(5);

            this.Property(t => t.province)
                .HasMaxLength(32);

            this.Property(t => t.city)
                .HasMaxLength(32);

            this.Property(t => t.country)
                .HasMaxLength(32);

            this.Property(t => t.headimgurl)
                .HasMaxLength(100);

            this.Property(t => t.unionid)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("WxUserInfor");
            this.Property(t => t.openid).HasColumnName("openid");
            this.Property(t => t.nickname).HasColumnName("nickname");
            this.Property(t => t.sex).HasColumnName("sex");
            this.Property(t => t.province).HasColumnName("province");
            this.Property(t => t.city).HasColumnName("city");
            this.Property(t => t.country).HasColumnName("country");
            this.Property(t => t.headimgurl).HasColumnName("headimgurl");
            this.Property(t => t.unionid).HasColumnName("unionid");
        }
    }
}
