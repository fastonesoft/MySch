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

        public DbSet<ADatum> ADatums { get; set; }
        public DbSet<AEdu> AEdus { get; set; }
        public DbSet<APage> APages { get; set; }
        public DbSet<ATheme> AThemes { get; set; }
        public DbSet<Kao> Kaos { get; set; }
        public DbSet<KRoom> KRooms { get; set; }
        public DbSet<KRoomType> KRoomTypes { get; set; }
        public DbSet<KScore> KScores { get; set; }
        public DbSet<KScoreDetail> KScoreDetails { get; set; }
        public DbSet<KStudent> KStudents { get; set; }
        public DbSet<KSubBan> KSubBans { get; set; }
        public DbSet<KSubGrade> KSubGrades { get; set; }
        public DbSet<KSubTest> KSubTests { get; set; }
        public DbSet<SCome> SComes { get; set; }
        public DbSet<SOut> SOuts { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudGrade> StudGrades { get; set; }
        public DbSet<TAcc> TAccs { get; set; }
        public DbSet<TBan> TBans { get; set; }
        public DbSet<TGrade> TGrades { get; set; }
        public DbSet<TLog> TLogs { get; set; }
        public DbSet<TLogin> TLogins { get; set; }
        public DbSet<TPart> TParts { get; set; }
        public DbSet<TPrint> TPrints { get; set; }
        public DbSet<TSemester> TSemesters { get; set; }
        public DbSet<TStep> TSteps { get; set; }
        public DbSet<TStudReg> TStudRegs { get; set; }
        public DbSet<TSub> TSubs { get; set; }
        public DbSet<TTerm> TTerms { get; set; }
        public DbSet<TYear> TYears { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ADatumMap());
            modelBuilder.Configurations.Add(new AEduMap());
            modelBuilder.Configurations.Add(new APageMap());
            modelBuilder.Configurations.Add(new AThemeMap());
            modelBuilder.Configurations.Add(new KaoMap());
            modelBuilder.Configurations.Add(new KRoomMap());
            modelBuilder.Configurations.Add(new KRoomTypeMap());
            modelBuilder.Configurations.Add(new KScoreMap());
            modelBuilder.Configurations.Add(new KScoreDetailMap());
            modelBuilder.Configurations.Add(new KStudentMap());
            modelBuilder.Configurations.Add(new KSubBanMap());
            modelBuilder.Configurations.Add(new KSubGradeMap());
            modelBuilder.Configurations.Add(new KSubTestMap());
            modelBuilder.Configurations.Add(new SComeMap());
            modelBuilder.Configurations.Add(new SOutMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new StudGradeMap());
            modelBuilder.Configurations.Add(new TAccMap());
            modelBuilder.Configurations.Add(new TBanMap());
            modelBuilder.Configurations.Add(new TGradeMap());
            modelBuilder.Configurations.Add(new TLogMap());
            modelBuilder.Configurations.Add(new TLoginMap());
            modelBuilder.Configurations.Add(new TPartMap());
            modelBuilder.Configurations.Add(new TPrintMap());
            modelBuilder.Configurations.Add(new TSemesterMap());
            modelBuilder.Configurations.Add(new TStepMap());
            modelBuilder.Configurations.Add(new TStudRegMap());
            modelBuilder.Configurations.Add(new TSubMap());
            modelBuilder.Configurations.Add(new TTermMap());
            modelBuilder.Configurations.Add(new TYearMap());
        }
    }
}
