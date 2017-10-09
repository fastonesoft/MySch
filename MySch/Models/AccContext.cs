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

        public virtual DbSet<AdminDatum> AdminDatums { get; set; }
        public virtual DbSet<AdminPage> AdminPages { get; set; }
        public virtual DbSet<AdminTheme> AdminThemes { get; set; }
        public virtual DbSet<Kao> Kaos { get; set; }
        public virtual DbSet<KaoAnaType> KaoAnaTypes { get; set; }
        public virtual DbSet<KaoPlace> KaoPlaces { get; set; }
        public virtual DbSet<KaoScore> KaoScores { get; set; }
        public virtual DbSet<KaoScoreAna> KaoScoreAnas { get; set; }
        public virtual DbSet<KaoSub> KaoSubs { get; set; }
        public virtual DbSet<KaoType> KaoTypes { get; set; }
        public virtual DbSet<RoleAction> RoleActions { get; set; }
        public virtual DbSet<RoleGroup> RoleGroups { get; set; }
        public virtual DbSet<RoleType> RoleTypes { get; set; }
        public virtual DbSet<Stud> Studs { get; set; }
        public virtual DbSet<StudCome> StudComes { get; set; }
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
        public virtual DbSet<ViAccRoleGroup> ViAccRoleGroups { get; set; }
        public virtual DbSet<ViWxAccPrize> ViWxAccPrizes { get; set; }
        public virtual DbSet<ViWxAccSend> ViWxAccSends { get; set; }
        public virtual DbSet<ViWxStudUpload> ViWxStudUploads { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kao>()
                .HasMany(e => e.KaoSubs)
                .WithRequired(e => e.Kao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KaoScore>()
                .HasMany(e => e.KaoScoreAnas)
                .WithRequired(e => e.KaoScore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KaoSub>()
                .HasMany(e => e.KaoScores)
                .WithRequired(e => e.KaoSub)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KaoType>()
                .HasMany(e => e.Kaos)
                .WithRequired(e => e.KaoType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleGroup>()
                .HasMany(e => e.TAccs)
                .WithRequired(e => e.RoleGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleType>()
                .HasMany(e => e.RoleActions)
                .WithRequired(e => e.RoleType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stud>()
                .HasMany(e => e.StudGrades)
                .WithRequired(e => e.Stud)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudCome>()
                .HasMany(e => e.StudGrades)
                .WithRequired(e => e.StudCome)
                .HasForeignKey(e => e.ComeIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudGrade>()
                .HasMany(e => e.KaoScores)
                .WithRequired(e => e.StudGrade)
                .HasForeignKey(e => e.GradeStudIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.Kaos)
                .WithRequired(e => e.TAcc)
                .HasForeignKey(e => e.AccIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.KaoAnaTypes)
                .WithRequired(e => e.TAcc)
                .HasForeignKey(e => e.AccIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.KaoPlaces)
                .WithRequired(e => e.TAcc)
                .HasForeignKey(e => e.AccIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.KaoScoreAnas)
                .WithRequired(e => e.TAcc)
                .HasForeignKey(e => e.AccIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.KaoTypes)
                .WithRequired(e => e.TAcc)
                .HasForeignKey(e => e.AccIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.StudGradeMoves)
                .WithRequired(e => e.TAcc)
                .HasForeignKey(e => e.OwnerIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.TBans)
                .WithOptional(e => e.TAcc)
                .HasForeignKey(e => e.MasterIDS);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.TEdus)
                .WithRequired(e => e.TAcc)
                .HasForeignKey(e => e.AccIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.TGrades)
                .WithRequired(e => e.TAcc)
                .HasForeignKey(e => e.AccIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.TParts)
                .WithRequired(e => e.TAcc)
                .HasForeignKey(e => e.AccIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.TSteps)
                .WithRequired(e => e.TAcc)
                .HasForeignKey(e => e.AccIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.TSubs)
                .WithRequired(e => e.TAcc)
                .HasForeignKey(e => e.AccIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.TTerms)
                .WithRequired(e => e.TAcc)
                .HasForeignKey(e => e.AccIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAcc>()
                .HasMany(e => e.TYears)
                .WithRequired(e => e.TAcc)
                .HasForeignKey(e => e.AccIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBan>()
                .HasMany(e => e.StudGrades)
                .WithRequired(e => e.TBan)
                .HasForeignKey(e => e.BanIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBan>()
                .HasMany(e => e.StudGradeMoves)
                .WithRequired(e => e.TBan)
                .HasForeignKey(e => e.BanIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TEdu>()
                .HasMany(e => e.TGrades)
                .WithRequired(e => e.TEdu)
                .HasForeignKey(e => e.EduIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TGrade>()
                .HasMany(e => e.StudGrades)
                .WithRequired(e => e.TGrade)
                .HasForeignKey(e => e.GradeIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TGrade>()
                .HasMany(e => e.TBans)
                .WithRequired(e => e.TGrade)
                .HasForeignKey(e => e.GradeIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TPart>()
                .HasMany(e => e.TSteps)
                .WithRequired(e => e.TPart)
                .HasForeignKey(e => e.PartIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TSemester>()
                .HasMany(e => e.TTerms)
                .WithRequired(e => e.TSemester)
                .HasForeignKey(e => e.SemesterIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TStep>()
                .HasMany(e => e.Studs)
                .WithRequired(e => e.TStep)
                .HasForeignKey(e => e.StepIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TStep>()
                .HasMany(e => e.TGrades)
                .WithRequired(e => e.TStep)
                .HasForeignKey(e => e.StepIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TSub>()
                .HasMany(e => e.KaoSubs)
                .WithRequired(e => e.TSub)
                .HasForeignKey(e => e.SubIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TTerm>()
                .HasMany(e => e.Kaos)
                .WithRequired(e => e.TTerm)
                .HasForeignKey(e => e.TermIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TYear>()
                .HasMany(e => e.TGrades)
                .WithRequired(e => e.TYear)
                .HasForeignKey(e => e.YearIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TYear>()
                .HasMany(e => e.TTerms)
                .WithRequired(e => e.TYear)
                .HasForeignKey(e => e.YearIDS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WxAcc>()
                .HasMany(e => e.WxAccPrizes)
                .WithRequired(e => e.WxAcc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WxAcc>()
                .HasMany(e => e.WxAccSends)
                .WithRequired(e => e.WxAcc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WxAction>()
                .HasMany(e => e.WxAccPrizes)
                .WithRequired(e => e.WxAction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WxAction>()
                .HasMany(e => e.WxAccSends)
                .WithRequired(e => e.WxAction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WxPrize>()
                .HasMany(e => e.WxAccPrizes)
                .WithRequired(e => e.WxPrize)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ViWxStudUpload>()
                .Property(e => e.StudSex)
                .IsUnicode(false);
        }
    }
}
