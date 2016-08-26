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
        public DbSet<TEdu> TEdus { get; set; }
        public DbSet<TGrade> TGrades { get; set; }
        public DbSet<TLog> TLogs { get; set; }
        public DbSet<TLogin> TLogins { get; set; }
        public DbSet<TPart> TParts { get; set; }
        public DbSet<TPartStep> TPartSteps { get; set; }
        public DbSet<TPrint> TPrints { get; set; }
        public DbSet<TStep> TSteps { get; set; }
        public DbSet<TStudReg> TStudRegs { get; set; }
        public DbSet<TTerm> TTerms { get; set; }
        public DbSet<TYear> TYears { get; set; }
        public DbSet<QPartStep> QPartSteps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TAccMap());
            modelBuilder.Configurations.Add(new TEduMap());
            modelBuilder.Configurations.Add(new TGradeMap());
            modelBuilder.Configurations.Add(new TLogMap());
            modelBuilder.Configurations.Add(new TLoginMap());
            modelBuilder.Configurations.Add(new TPartMap());
            modelBuilder.Configurations.Add(new TPartStepMap());
            modelBuilder.Configurations.Add(new TPrintMap());
            modelBuilder.Configurations.Add(new TStepMap());
            modelBuilder.Configurations.Add(new TStudRegMap());
            modelBuilder.Configurations.Add(new TTermMap());
            modelBuilder.Configurations.Add(new TYearMap());
            modelBuilder.Configurations.Add(new QPartStepMap());
        }
    }
}
