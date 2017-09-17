namespace MySch.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AccContext : DbContext
    {
        public AccContext()
            : base("name=AccContext")
        {
        }

        public virtual DbSet<ADatum> ADatums { get; set; }
        public virtual DbSet<APage> APages { get; set; }
        public virtual DbSet<ARoleAction> ARoleActions { get; set; }
        public virtual DbSet<ARoleGroup> ARoleGroups { get; set; }
        public virtual DbSet<ARoleType> ARoleTypes { get; set; }
        public virtual DbSet<ATheme> AThemes { get; set; }
        public virtual DbSet<Kao> Kaos { get; set; }
        public virtual DbSet<KRoom> KRooms { get; set; }
        public virtual DbSet<KRoomType> KRoomTypes { get; set; }
        public virtual DbSet<KScore> KScores { get; set; }
        public virtual DbSet<KScoreDetail> KScoreDetails { get; set; }
        public virtual DbSet<KStud> KStuds { get; set; }
        public virtual DbSet<KSubBan> KSubBans { get; set; }
        public virtual DbSet<KSubGrade> KSubGrades { get; set; }
        public virtual DbSet<KSubTest> KSubTests { get; set; }
        public virtual DbSet<StudCome> StudComes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudGrade> StudGrades { get; set; }
        public virtual DbSet<StudGradeMove> StudGradeMoves { get; set; }
        public virtual DbSet<StudOut> StudOuts { get; set; }
        public virtual DbSet<TAcc> TAccs { get; set; }
        public virtual DbSet<TBan> TBans { get; set; }
        public virtual DbSet<TEdu> TEdus { get; set; }
        public virtual DbSet<TGrade> TGrades { get; set; }
        public virtual DbSet<TLog> TLogs { get; set; }
        public virtual DbSet<TLogin> TLogins { get; set; }
        public virtual DbSet<TPart> TParts { get; set; }
        public virtual DbSet<TPrint> TPrints { get; set; }
        public virtual DbSet<TSemester> TSemesters { get; set; }
        public virtual DbSet<TStep> TSteps { get; set; }
        public virtual DbSet<TSub> TSubs { get; set; }
        public virtual DbSet<TTerm> TTerms { get; set; }
        public virtual DbSet<TYear> TYears { get; set; }
        public virtual DbSet<WxAcc> WxAccs { get; set; }
        public virtual DbSet<WxAccFilter> WxAccFilters { get; set; }
        public virtual DbSet<WxAccPrize> WxAccPrizes { get; set; }
        public virtual DbSet<WxAccSend> WxAccSends { get; set; }
        public virtual DbSet<WxAction> WxActions { get; set; }
        public virtual DbSet<WxPrize> WxPrizes { get; set; }
        public virtual DbSet<WxUploadFile> WxUploadFiles { get; set; }
        public virtual DbSet<StudGradeField> StudGradeFields { get; set; }
        public virtual DbSet<StudGradeTable> StudGradeTables { get; set; }
        public virtual DbSet<StudGradeType> StudGradeTypes { get; set; }
        public virtual DbSet<QrAccRoleGroup> QrAccRoleGroups { get; set; }
        public virtual DbSet<QrWxAccPrize> QrWxAccPrizes { get; set; }
        public virtual DbSet<QrWxAccSend> QrWxAccSends { get; set; }
        public virtual DbSet<QrWxStudentUpload> QrWxStudentUploads { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.TBans)
                .WithOptional(e => e.TAcc)
                .HasForeignKey(e => e.MasterIDS);

            modelBuilder.Entity<QrWxStudentUpload>()
                .Property(e => e.StudSex)
                .IsUnicode(false);
        }
    }
}
