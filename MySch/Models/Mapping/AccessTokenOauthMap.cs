using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class AccessTokenOauthMap : EntityTypeConfiguration<AccessTokenOauth>
    {
        public AccessTokenOauthMap()
        {
            // Primary Key
            this.HasKey(t => t.openid);

            // Properties
            this.Property(t => t.openid)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.access_token)
                .IsRequired()
                .HasMaxLength(900);

            this.Property(t => t.refresh_token)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.scope)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("AccessTokenOauth");
            this.Property(t => t.openid).HasColumnName("openid");
            this.Property(t => t.access_token).HasColumnName("access_token");
            this.Property(t => t.expires_in).HasColumnName("expires_in");
            this.Property(t => t.refresh_token).HasColumnName("refresh_token");
            this.Property(t => t.scope).HasColumnName("scope");
            this.Property(t => t.create_time).HasColumnName("create_time");
        }
    }
}
