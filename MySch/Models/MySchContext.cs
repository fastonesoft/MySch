using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MySch.Models.Mapping;

namespace MySch.Models
{
    public partial class MySchContext : DbContext
    {
        static MySchContext()
        {
            Database.SetInitializer<MySchContext>(null);
        }

        public MySchContext()
            : base("Name=MySchContext")
        {
        }

        public DbSet<TAcc> TAccs { get; set; }
        public DbSet<TColumn> TColumns { get; set; }
        public DbSet<TFileInfor> TFileInfors { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<TKey> TKeys { get; set; }
        public DbSet<TKeyClass> TKeyClasses { get; set; }
        public DbSet<TLog> TLogs { get; set; }
        public DbSet<TLogin> TLogins { get; set; }
        public DbSet<TPage> TPages { get; set; }
        public DbSet<TStudReg> TStudRegs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TAccMap());
            modelBuilder.Configurations.Add(new TColumnMap());
            modelBuilder.Configurations.Add(new TFileInforMap());
            modelBuilder.Configurations.Add(new ThemeMap());
            modelBuilder.Configurations.Add(new TKeyMap());
            modelBuilder.Configurations.Add(new TKeyClassMap());
            modelBuilder.Configurations.Add(new TLogMap());
            modelBuilder.Configurations.Add(new TLoginMap());
            modelBuilder.Configurations.Add(new TPageMap());
            modelBuilder.Configurations.Add(new TStudRegMap());
        }
    }
}
