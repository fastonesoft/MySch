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
        public DbSet<TBan> TBans { get; set; }
        public DbSet<TBanFen> TBanFens { get; set; }
        public DbSet<TEdu> TEdus { get; set; }
        public DbSet<TGrade> TGrades { get; set; }
        public DbSet<TLog> TLogs { get; set; }
        public DbSet<TLogin> TLogins { get; set; }
        public DbSet<TPart> TParts { get; set; }
        public DbSet<TPartStep> TPartSteps { get; set; }
        public DbSet<TPrint> TPrints { get; set; }
        public DbSet<TSemester> TSemesters { get; set; }
        public DbSet<TStep> TSteps { get; set; }
        public DbSet<TStudent> TStudents { get; set; }
        public DbSet<TStudReg> TStudRegs { get; set; }
        public DbSet<TTerm> TTerms { get; set; }
        public DbSet<TYear> TYears { get; set; }
        public DbSet<QBan> QBans { get; set; }
        public DbSet<QBanStud> QBanStuds { get; set; }
        public DbSet<QGrade> QGrades { get; set; }
        public DbSet<QPartStep> QPartSteps { get; set; }
        public DbSet<QTerm> QTerms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TAccMap());
            modelBuilder.Configurations.Add(new TBanMap());
            modelBuilder.Configurations.Add(new TBanFenMap());
            modelBuilder.Configurations.Add(new TEduMap());
            modelBuilder.Configurations.Add(new TGradeMap());
            modelBuilder.Configurations.Add(new TLogMap());
            modelBuilder.Configurations.Add(new TLoginMap());
            modelBuilder.Configurations.Add(new TPartMap());
            modelBuilder.Configurations.Add(new TPartStepMap());
            modelBuilder.Configurations.Add(new TPrintMap());
            modelBuilder.Configurations.Add(new TSemesterMap());
            modelBuilder.Configurations.Add(new TStepMap());
            modelBuilder.Configurations.Add(new TStudentMap());
            modelBuilder.Configurations.Add(new TStudRegMap());
            modelBuilder.Configurations.Add(new TTermMap());
            modelBuilder.Configurations.Add(new TYearMap());
            modelBuilder.Configurations.Add(new QBanMap());
            modelBuilder.Configurations.Add(new QBanStudMap());
            modelBuilder.Configurations.Add(new QGradeMap());
            modelBuilder.Configurations.Add(new QPartStepMap());
            modelBuilder.Configurations.Add(new QTermMap());
        }
    }
}
