using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SchoolRecognition.Entities;
using SchoolRecognition.Models;

namespace SchoolRecognition.DbContexts
{
    public partial class SchoolRecognitionContext : DbContext
    {
        public SchoolRecognitionContext()
        {
        }

        public SchoolRecognitionContext(DbContextOptions<SchoolRecognitionContext> options)
            : base(options)
        {
        }
        public virtual DbSet<ApplicationRoles> ApplicationRoles { get; set; }
        public virtual DbSet<ApplicationSettings> ApplicationSettings { get; set; }
        public virtual DbSet<ApplicationUserStates> ApplicationUserStates { get; set; }
        public virtual DbSet<ApplicationUsers> ApplicationUsers { get; set; }
        public virtual DbSet<ApprovedCourses> ApprovedCourses { get; set; }
        public virtual DbSet<AssessmentKeys> AssessmentKeys { get; set; }
        public virtual DbSet<AuditTrail> AuditTrail { get; set; }
        public virtual DbSet<CentreSanctions> CentreSanctions { get; set; }
        public virtual DbSet<CentreSubjectSanctions> CentreSubjectSanctions { get; set; }
        public virtual DbSet<Centres> Centres { get; set; }
        public virtual DbSet<ClassSettings> ClassSettings { get; set; }
        public virtual DbSet<Dblogger> Dblogger { get; set; }
        public virtual DbSet<DegreeTypes> DegreeTypes { get; set; }
        public virtual DbSet<Degrees> Degrees { get; set; }
        public virtual DbSet<FacilityItems> FacilityItems { get; set; }
        public virtual DbSet<FacilitySettings> FacilitySettings { get; set; }
        public virtual DbSet<FacilityTypes> FacilityTypes { get; set; }
        public virtual DbSet<LocalGovernments> LocalGovernments { get; set; }
        public virtual DbSet<LocationTypes> LocationTypes { get; set; }
        public virtual DbSet<OfficeLocalGovernments> OfficeLocalGovernments { get; set; }
        public virtual DbSet<OfficeStates> OfficeStates { get; set; }
        public virtual DbSet<OfficeTypes> OfficeTypes { get; set; }
        public virtual DbSet<Offices> Offices { get; set; }
        public virtual DbSet<PinHistories> PinHistories { get; set; }
        public virtual DbSet<Pins> Pins { get; set; }
        public virtual DbSet<Ranks> Ranks { get; set; }
        public virtual DbSet<RecognitionTypes> RecognitionTypes { get; set; }
        public virtual DbSet<SanctionSettings> SanctionSettings { get; set; }
        public virtual DbSet<SchoolCategories> SchoolCategories { get; set; }
        public virtual DbSet<SchoolClasses> SchoolClasses { get; set; }
        public virtual DbSet<SchoolDeficiencies> SchoolDeficiencies { get; set; }
        public virtual DbSet<SchoolFacilities> SchoolFacilities { get; set; }
        public virtual DbSet<SchoolPayments> SchoolPayments { get; set; }
        public virtual DbSet<SchoolStaffCategories> SchoolStaffCategories { get; set; }
        public virtual DbSet<SchoolStaffDegrees> SchoolStaffDegrees { get; set; }
        public virtual DbSet<SchoolStaffProfiles> SchoolStaffProfiles { get; set; }
        public virtual DbSet<SchoolStaffSubjects> SchoolStaffSubjects { get; set; }
        public virtual DbSet<SchoolSubjects> SchoolSubjects { get; set; }
        public virtual DbSet<Schools> Schools { get; set; }
        public virtual DbSet<SchoolsAssessment> SchoolsAssessment { get; set; }
        public virtual DbSet<SecurityConfig> SecurityConfig { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Titles> Titles { get; set; }
       
       
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=NORMAL-PC\\SQLEXPRESS;Database=SchoolRecognition;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationRoles>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UrlHome).HasMaxLength(256);
            });

            modelBuilder.Entity<ApplicationSettings>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<ApplicationUserStates>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ApplicationUserStatesCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_ApplicationUserStates_ApplicationUsers1");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.ApplicationUserStates)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_ApplicationUserStates_States");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ApplicationUserStatesUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ApplicationUserStates_ApplicationUsers");
            });

            modelBuilder.Entity<ApplicationUsers>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.Lpno)
                    .HasColumnName("LPNO")
                    .HasMaxLength(30);

                entity.Property(e => e.Othernames).HasMaxLength(50);

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.Property(e => e.RankId).HasColumnName("RankID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.ApplicationUsers)
                    .HasForeignKey(d => d.RankId)
                    .HasConstraintName("FK_User_Rank");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ApplicationUsers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<ApprovedCourses>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<AssessmentKeys>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Grade)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<AuditTrail>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuditTrail)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditTrail_Users");
            });

            modelBuilder.Entity<CentreSanctions>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CentreId).HasColumnName("CentreID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.NoOfYears).HasComment("0 for where no of year is not applied");

                entity.Property(e => e.SanctionSettingId).HasColumnName("SanctionSettingID");

                entity.HasOne(d => d.Centre)
                    .WithMany(p => p.CentreSanctions)
                    .HasForeignKey(d => d.CentreId)
                    .HasConstraintName("FK_CentreSanctions_Centres");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CentreSanctions)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_CentreSanctions_ApplicationUsers");

                entity.HasOne(d => d.SanctionSetting)
                    .WithMany(p => p.CentreSanctions)
                    .HasForeignKey(d => d.SanctionSettingId)
                    .HasConstraintName("FK_CentreSanctions_SanctionSettings");
            });

            modelBuilder.Entity<CentreSubjectSanctions>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CentreSanctionId).HasColumnName("CentreSanctionID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.HasOne(d => d.CentreSanction)
                    .WithMany(p => p.CentreSubjectSanctions)
                    .HasForeignKey(d => d.CentreSanctionId)
                    .HasConstraintName("FK_CentreSubjectSanctions_CentreSanctions");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.CentreSubjectSanctions)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_CentreSubjectSanctions_Subjects");
            });

            modelBuilder.Entity<Centres>(entity =>
            {
                entity.HasIndex(e => e.CentreNo)
                    .HasName("IX_Centres")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryCode).HasMaxLength(5);

                entity.Property(e => e.CentreName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CentreNo)
                    .IsRequired()
                    .HasMaxLength(7);

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.LocationDetails)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SchoolCategoryId).HasColumnName("SchoolCategoryID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Centres)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Centres_ApplicationUsers");

                entity.HasOne(d => d.SchoolCategory)
                    .WithMany(p => p.Centres)
                    .HasForeignKey(d => d.SchoolCategoryId)
                    .HasConstraintName("FK_Centres_SchoolCategories");
            });

            modelBuilder.Entity<ClassSettings>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Dblogger>(entity =>
            {
                entity.ToTable("DBLogger");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ErrorMsg).IsRequired();
            });

            modelBuilder.Entity<DegreeTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Degrees>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<FacilityItems>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    //.IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    //.IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<FacilitySettings>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.FacilityItemId).HasColumnName("FacilityItemID");

                entity.Property(e => e.FacilityTypeId).HasColumnName("FacilityTypeID");

                entity.Property(e => e.Specification).HasMaxLength(20);

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.FacilitySettings)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_FacilitySettings_ApplicationUsers");

                entity.HasOne(d => d.FacilityItem)
                    .WithMany(p => p.FacilitySettings)
                    .HasForeignKey(d => d.FacilityItemId)
                    .HasConstraintName("FK_FacilitySettings_FacilityItems");

                entity.HasOne(d => d.FacilityType)
                    .WithMany(p => p.FacilitySettings)
                    .HasForeignKey(d => d.FacilityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FacilitySettings_FacilityTypes");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.FacilitySettings)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_FacilitySettings_SchoolSubjects");
            });

            modelBuilder.Entity<FacilityTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<LocalGovernments>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(3);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.LocalGovernments)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_LocalGovernments_State");
            });

            modelBuilder.Entity<LocationTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<OfficeLocalGovernments>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LocalGovernmentId).HasColumnName("LocalGovernmentID");

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.HasOne(d => d.LocalGovernment)
                    .WithMany(p => p.OfficeLocalGovernments)
                    .HasForeignKey(d => d.LocalGovernmentId)
                    .HasConstraintName("FK_OfficeLocalGovernments_LocalGovernments");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.OfficeLocalGovernments)
                    .HasForeignKey(d => d.OfficeId)
                    .HasConstraintName("FK_OfficeLocalGovernments_Offices");
            });

            modelBuilder.Entity<OfficeStates>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.OfficeStates)
                    .HasForeignKey(d => d.OfficeId)
                    .HasConstraintName("FK_OfficeStates_Offices");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.OfficeStates)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_OfficeStates_States");
            });

            modelBuilder.Entity<OfficeTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Offices>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OfficeTypeId).HasColumnName("OfficeTypeID");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Offices_Users");

                entity.HasOne(d => d.OfficeType)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.OfficeTypeId)
                    .HasConstraintName("FK_Offices_OfficeTypes");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Offices_States");
            });

            modelBuilder.Entity<PinHistories>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateActive).HasColumnType("date");

                entity.Property(e => e.PinId).HasColumnName("PinID");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PinHistories)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_PinHistories_Users");

                entity.HasOne(d => d.Pin)
                    .WithMany(p => p.PinHistories)
                    .HasForeignKey(d => d.PinId)
                    .HasConstraintName("FK_PinHistory_PIN");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.PinHistories)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_PinHistory_School");
            });

            modelBuilder.Entity<Pins>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.RecognitionTypeId).HasColumnName("RecognitionTypeID");

                entity.Property(e => e.SerialPin).HasMaxLength(25);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Pins)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_PIN_User");

                entity.HasOne(d => d.RecognitionType)
                    .WithMany(p => p.Pins)
                    .HasForeignKey(d => d.RecognitionTypeId)
                    .HasConstraintName("FK_PIN_RecognitionType");
            });

            modelBuilder.Entity<Ranks>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<RecognitionTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<SanctionSettings>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<SchoolCategories>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(2);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<SchoolClasses>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SchoolClasses)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolClassAllocations_SchoolClasses");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolClasses)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolClassAllocations_Schools");
            });

            modelBuilder.Entity<SchoolDeficiencies>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.SchoolFacilityId).HasColumnName("SchoolFacilityID");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.HasOne(d => d.SchoolFacility)
                    .WithMany(p => p.SchoolDeficiencies)
                    .HasForeignKey(d => d.SchoolFacilityId)
                    .HasConstraintName("FK_SchoolDeficiencies_SchoolFacilities");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolDeficiencies)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_SchoolDeficiencies_Schools");
            });

            modelBuilder.Entity<SchoolFacilities>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FacilitySettingId).HasColumnName("FacilitySettingID");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.ValueApplied).HasMaxLength(50);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SchoolFacilities)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_FacilityInformation_Users");

                entity.HasOne(d => d.FacilitySetting)
                    .WithMany(p => p.SchoolFacilities)
                    .HasForeignKey(d => d.FacilitySettingId)
                    .HasConstraintName("FK_SchoolFacilities_FacilitySettings");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolFacilities)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_FacilityInformation_Schools");
            });

            modelBuilder.Entity<SchoolPayments>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.PinId).HasColumnName("PinID");

                entity.Property(e => e.ReceiptNo).HasMaxLength(50);

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SchoolPayments)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_SchoolPayment_User");

                entity.HasOne(d => d.Pin)
                    .WithMany(p => p.SchoolPayments)
                    .HasForeignKey(d => d.PinId)
                    .HasConstraintName("FK_SchoolPayment_PIN");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolPayments)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_SchoolPayment_School");
            });

            modelBuilder.Entity<SchoolStaffCategories>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RankOrder).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SchoolStaffDegrees>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.CredentialPath).HasMaxLength(256);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DegreeId).HasColumnName("DegreeID");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.SchoolStaffDegrees)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_SchoolStaffDegrees_ApprovedCourses");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SchoolStaffDegrees)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_SchoolStaffDegrees_Users");

                entity.HasOne(d => d.Degree)
                    .WithMany(p => p.SchoolStaffDegrees)
                    .HasForeignKey(d => d.DegreeId)
                    .HasConstraintName("FK_SchoolStaffDegrees_Degrees");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.SchoolStaffDegrees)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_SchoolStaffDegrees_SchoolStaffProfiles");
            });

            modelBuilder.Entity<SchoolStaffProfiles>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ContactAddress).HasMaxLength(150);

                entity.Property(e => e.DateEmployed).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(150);

                entity.Property(e => e.HasTrcn)
                    .HasColumnName("HasTRCN")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Othernames).HasMaxLength(100);

                entity.Property(e => e.PhoneNo).HasMaxLength(50);

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.Property(e => e.TitleId).HasColumnName("TitleID");

                entity.Property(e => e.Trcn)
                    .HasColumnName("TRCN")
                    .HasMaxLength(150);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SchoolStaffProfiles)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_SchoolStaffProfiles_SchoolStaffCategories");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolStaffProfiles)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_SchoolStaffProfiles_Schools");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.SchoolStaffProfiles)
                    .HasForeignKey(d => d.TitleId)
                    .HasConstraintName("FK_SchoolStaffProfiles_Titles");
            });

            modelBuilder.Entity<SchoolStaffSubjects>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SchoolStaffSubjects)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_SchoolStaffSubjects_SchoolClasses");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.SchoolStaffSubjects)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_SchoolStaffSubjects_SchoolStaffProfiles");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SchoolStaffSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_SchoolStaffSubjects_Subjects");
            });

            modelBuilder.Entity<SchoolSubjects>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SchoolSubjectsCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_SchoolSubjects_ApplicationUsers");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.SchoolSubjectsModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_SchoolSubjects_ApplicationUsers1");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolSubjects)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolSubjects_Schools");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SchoolSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolSubjects_Subjects");
            });

            modelBuilder.Entity<Schools>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CentreId).HasColumnName("CentreID");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.LgId).HasColumnName("LgID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Schools)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_School_SchoolCategory");

                entity.HasOne(d => d.Centre)
                    .WithMany(p => p.Schools)
                    .HasForeignKey(d => d.CentreId)
                    .HasConstraintName("FK_Schools_Centres");

                entity.HasOne(d => d.Lg)
                    .WithMany(p => p.Schools)
                    .HasForeignKey(d => d.LgId)
                    .HasConstraintName("FK_School_LocalGovernment");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Schools)
                    .HasForeignKey(d => d.OfficeId)
                    .HasConstraintName("FK_School_Office");
            });

            modelBuilder.Entity<SchoolsAssessment>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FacilityItemId).HasColumnName("FacilityItemID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_SchoolsAssessment_ApplicationUsers");

                entity.HasOne(d => d.FacilityItem)
                    .WithMany()
                    .HasForeignKey(d => d.FacilityItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolsAssessment_FacilityItems");

                entity.HasOne(d => d.School)
                    .WithMany()
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolsAssessment_Schools");
            });

            modelBuilder.Entity<SecurityConfig>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ClientAllowedIp)
                    .HasColumnName("ClientAllowedIP")
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.OrganisationName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SupportedBrowsers)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SupportedClientVersion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(3);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.HasIndex(e => e.SubjectCode)
                    .HasName("IX_Subjects")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.LongName).HasMaxLength(100);

                entity.Property(e => e.ShortName).HasMaxLength(50);

                entity.Property(e => e.SubjectCode).HasMaxLength(6);
            });

            modelBuilder.Entity<Titles>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        //public DbSet<SchoolRecognition.Models.FacilityItemsCreateDto> FacilityItemsCreateDto { get; set; }

        //public DbSet<SchoolRecognition.Models.FacilityItemsDto> FacilityItemsDto { get; set; }
    }
}
