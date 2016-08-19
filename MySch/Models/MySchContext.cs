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
        public DbSet<TEducation> TEducations { get; set; }
        public DbSet<TLog> TLogs { get; set; }
        public DbSet<TLogin> TLogins { get; set; }
        public DbSet<TPrint> TPrints { get; set; }
        public DbSet<TStudReg> TStudRegs { get; set; }
        public DbSet<TYear> TYears { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TAccMap());
            modelBuilder.Configurations.Add(new TEducationMap());
            modelBuilder.Configurations.Add(new TLogMap());
            modelBuilder.Configurations.Add(new TLoginMap());
            modelBuilder.Configurations.Add(new TPrintMap());
            modelBuilder.Configurations.Add(new TStudRegMap());
            modelBuilder.Configurations.Add(new TYearMap());
        }
    }
}
