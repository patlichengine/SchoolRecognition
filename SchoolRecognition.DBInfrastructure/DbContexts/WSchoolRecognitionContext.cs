using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SchoolRecognition.DBInfrastructure.Entities;
using SchoolRecognition.DBInfrastructure.Helpers;

namespace SchoolRecognition.DBInfrastructure.DbContexts
{
    public partial class WSchoolRecognitionContext : DbContext
    {
        private readonly ConnectionString _connectionString;

        public WSchoolRecognitionContext()
        {
        }

        public WSchoolRecognitionContext(DbContextOptions<WSchoolRecognitionContext> options, ConnectionString connectionString)
            : base(options)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public virtual DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<ApplicationUserState> ApplicationUserStates { get; set; }
        public virtual DbSet<ApprovedCours> ApprovedCourses { get; set; }
        public virtual DbSet<ApprovedSchoolClass> ApprovedSchoolClasses { get; set; }
        public virtual DbSet<AuditTrail> AuditTrails { get; set; }
        public virtual DbSet<Centre> Centres { get; set; }
        public virtual DbSet<CentreSanction> CentreSanctions { get; set; }
        public virtual DbSet<Country> Countrys { get; set; }
        public virtual DbSet<CountrySetting> CountrySettings { get; set; }
        public virtual DbSet<Dblogger> Dbloggers { get; set; }
        public virtual DbSet<Degree> Degrees { get; set; }
        public virtual DbSet<DegreeType> DegreeTypes { get; set; }
        public virtual DbSet<FacilityItemSetting> FacilityItemSettings { get; set; }
        public virtual DbSet<FacilitySetting> FacilitySettings { get; set; }
        public virtual DbSet<FacilityType> FacilityTypes { get; set; }
        public virtual DbSet<ForgotPassword> ForgotPasswords { get; set; }
        public virtual DbSet<Lga> Lgas { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationCategory> LocationCategorys { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<OfficeDesignation> OfficeDesignations { get; set; }
        public virtual DbSet<OfficeLocalGovernment> OfficeLocalGovernments { get; set; }
        public virtual DbSet<OfficeRank> OfficeRanks { get; set; }
        public virtual DbSet<OfficeState> OfficeStates { get; set; }
        public virtual DbSet<PaymentPin> PaymentPins { get; set; }
        public virtual DbSet<PaymentPinSetting> PaymentPinSettings { get; set; }
        public virtual DbSet<PinHistory> PinHistorys { get; set; }
        public virtual DbSet<SanctionSetting> SanctionSettings { get; set; }
        public virtual DbSet<SchoolCategory> SchoolCategorys { get; set; }
        public virtual DbSet<SchoolDeficiency> SchoolDeficiencys { get; set; }
        public virtual DbSet<SchoolFacility> SchoolFacilitys { get; set; }
        public virtual DbSet<SchoolPayment> SchoolPayments { get; set; }
        public virtual DbSet<SchoolProfile> SchoolProfiles { get; set; }
        public virtual DbSet<SchoolRecognitionTrail> SchoolRecognitionTrails { get; set; }
        public virtual DbSet<SchoolRecognitionTrailRejection> SchoolRecognitionTrailRejections { get; set; }
        public virtual DbSet<SchoolRecognitionType> SchoolRecognitionTypes { get; set; }
        public virtual DbSet<SchoolStaffCategory> SchoolStaffCategorys { get; set; }
        public virtual DbSet<SchoolStaffDegree> SchoolStaffDegrees { get; set; }
        public virtual DbSet<SchoolStaffProfile> SchoolStaffProfiles { get; set; }
        public virtual DbSet<SecurityConfig> SecurityConfigs { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<UserSecureAccount> UserSecureAccounts { get; set; }
        public virtual DbSet<UserSecurityQuestion> UserSecurityQuestions { get; set; }
        public virtual DbSet<VwApplicationUser> VwApplicationUsers { get; set; }
        public virtual DbSet<VwApplicationUserGroup> VwApplicationUserGroups { get; set; }
        public virtual DbSet<VwApplicationUserSecurityQuestion> VwApplicationUserSecurityQuestions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(_connectionString.Value);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationSetting>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CountrySettingId).HasColumnName("CountrySettingID");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.Lpno)
                    .HasColumnName("LPNO")
                    .HasMaxLength(30);

                entity.Property(e => e.Othernames).HasMaxLength(50);

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.Property(e => e.RankId).HasColumnName("RankID");

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.Property(e => e.UserGroupId).HasColumnName("UserGroupID");

                entity.HasOne(d => d.CountrySetting)
                    .WithMany(p => p.ApplicationUsers)
                    .HasForeignKey(d => d.CountrySettingId)
                    .HasConstraintName("FK_ApplicationUsers_CountrySettings");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.ApplicationUsers)
                    .HasForeignKey(d => d.RankId)
                    .HasConstraintName("FK_ApplicationUsers_OfficeRanks");

                entity.HasOne(d => d.UserGroup)
                    .WithMany(p => p.ApplicationUsers)
                    .HasForeignKey(d => d.UserGroupId)
                    .HasConstraintName("FK_ApplicationUsers_UserGroups");
            });

            modelBuilder.Entity<ApplicationUserState>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StateCode).HasMaxLength(2);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.StateCodeNavigation)
                    .WithMany(p => p.ApplicationUserStates)
                    .HasForeignKey(d => d.StateCode)
                    .HasConstraintName("FK_ApplicationUserStates_States");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ApplicationUserStates)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ApplicationUserStates_ApplicationUsers");
            });

            modelBuilder.Entity<ApprovedCours>(entity =>
            {
                entity.HasComment("List of courses approved for subject teachers ");

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

            modelBuilder.Entity<ApprovedSchoolClass>(entity =>
            {
                entity.HasKey(e => e.ClassCode);

                entity.Property(e => e.ClassCode)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(200);
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

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuditTrails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditTrail_Users");
            });

            modelBuilder.Entity<Centre>(entity =>
            {
                entity.HasKey(e => e.CentreNo);

                entity.HasIndex(e => e.CentreNo)
                    .HasName("IX_Centred")
                    .IsUnique();

                entity.Property(e => e.CentreNo).HasMaxLength(7);

                entity.Property(e => e.CentreName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CentreSanction>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateSanctionStarted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.NoOfYears).HasComment("0 for where no of year is not applied");

                entity.Property(e => e.SanctionSettingId).HasColumnName("SanctionSettingID");

                entity.Property(e => e.SchoolProfileId).HasColumnName("SchoolProfileID");

                entity.Property(e => e.SubjectCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CentreSanctions)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_CentreSanctions_ApplicationUsers");

                entity.HasOne(d => d.SanctionSetting)
                    .WithMany(p => p.CentreSanctions)
                    .HasForeignKey(d => d.SanctionSettingId)
                    .HasConstraintName("FK_CentreSanctions_SanctionSettings");

                entity.HasOne(d => d.SchoolProfile)
                    .WithMany(p => p.CentreSanctions)
                    .HasForeignKey(d => d.SchoolProfileId)
                    .HasConstraintName("FK_CentreSanctions_SchoolProfiles");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryCode)
                    .HasName("PK_CountryTbl");

                entity.Property(e => e.CountryCode).ValueGeneratedNever();

                entity.Property(e => e.AddedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Casscycle).HasColumnName("CASSCycle");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.FederalUnit).HasMaxLength(50);

                entity.Property(e => e.IsAvailable)
                    .IsRequired()
                    .HasColumnName("isAvailable")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LocalUnit).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedIp).HasMaxLength(50);

                entity.Property(e => e.PhoneCode).HasMaxLength(4);

                entity.Property(e => e.RecType)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('CO')");
            });

            modelBuilder.Entity<CountrySetting>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ApplicationSettingId).HasColumnName("ApplicationSettingID");

                entity.Property(e => e.CoreSubjects).HasMaxLength(200);

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.CountrySettings)
                    .HasForeignKey(d => d.CountryCode)
                    .HasConstraintName("FK_CountrySettings_Countrys");
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

            modelBuilder.Entity<Degree>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DegreeName).HasMaxLength(500);

                entity.Property(e => e.DegreeTypeId).HasColumnName("DegreeTypeID");

                entity.HasOne(d => d.DegreeType)
                    .WithMany(p => p.Degrees)
                    .HasForeignKey(d => d.DegreeTypeId)
                    .HasConstraintName("FK_Degrees_Degrees");
            });

            modelBuilder.Entity<DegreeType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FacilityItemSetting>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<FacilitySetting>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.FacilityItemSettingsId).HasColumnName("FacilityItemSettingsID");

                entity.Property(e => e.FacilityTypeId).HasColumnName("FacilityTypeID");

                entity.Property(e => e.Specification).HasMaxLength(1000);

                entity.Property(e => e.SubjectCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.FacilitySettings)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_FacilitySettings_ApplicationUsers");

                entity.HasOne(d => d.FacilityItemSettings)
                    .WithMany(p => p.FacilitySettings)
                    .HasForeignKey(d => d.FacilityItemSettingsId)
                    .HasConstraintName("FK_FacilitySettings_FacilityItemSettings");

                entity.HasOne(d => d.FacilityType)
                    .WithMany(p => p.FacilitySettings)
                    .HasForeignKey(d => d.FacilityTypeId)
                    .HasConstraintName("FK_FacilitySettings_FacilityTypes");

                entity.HasOne(d => d.SubjectCodeNavigation)
                    .WithMany(p => p.FacilitySettings)
                    .HasForeignKey(d => d.SubjectCode)
                    .HasConstraintName("FK_FacilitySettings_Subjects");
            });

            modelBuilder.Entity<FacilityType>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<ForgotPassword>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Lpno)
                    .HasColumnName("LPNO")
                    .HasMaxLength(50);

                entity.Property(e => e.OtherNames).HasMaxLength(100);

                entity.Property(e => e.Surname).HasMaxLength(50);
            });

            modelBuilder.Entity<Lga>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LgaName).HasMaxLength(50);

                entity.Property(e => e.LocalCode).HasMaxLength(2);

                entity.Property(e => e.StateCode).HasMaxLength(2);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CentreAddress)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.CentreDirections).IsUnicode(false);

                entity.Property(e => e.CentreName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CentreNo)
                    .IsRequired()
                    .HasMaxLength(7);

                entity.Property(e => e.CentreStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CentreType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gpslat)
                    .HasColumnName("GPSLat")
                    .HasColumnType("decimal(20, 17)");

                entity.Property(e => e.Gpslong)
                    .HasColumnName("GPSLong")
                    .HasColumnType("decimal(20, 17)");

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Landmark).IsUnicode(false);

                entity.Property(e => e.LocationCategoryId).HasDefaultValueSql("((1))");

                entity.Property(e => e.StateCode).HasMaxLength(2);

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.LocationApprovedByNavigations)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_Locations_ApplicationUsers1");

                entity.HasOne(d => d.CapturedByNavigation)
                    .WithMany(p => p.LocationCapturedByNavigations)
                    .HasForeignKey(d => d.CapturedBy)
                    .HasConstraintName("FK_Locations_ApplicationUsers");

                entity.HasOne(d => d.CentreNoNavigation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CentreNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Locations_Centres");

                entity.HasOne(d => d.LocationCategory)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.LocationCategoryId)
                    .HasConstraintName("FK_Locations_LocationCategorys");
            });

            modelBuilder.Entity<LocationCategory>(entity =>
            {
                entity.HasIndex(e => e.Description)
                    .HasName("UNQ_Category_description")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress).HasMaxLength(150);

                entity.Property(e => e.OfficeTitle).HasMaxLength(150);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.ShortName).HasMaxLength(50);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Offices_Offices1");
            });

            modelBuilder.Entity<OfficeDesignation>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DesignationTitle).HasMaxLength(50);
            });

            modelBuilder.Entity<OfficeLocalGovernment>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCaptured).HasColumnType("datetime");

                entity.Property(e => e.LgaId).HasColumnName("LgaID");

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.HasOne(d => d.Lga)
                    .WithMany(p => p.OfficeLocalGovernments)
                    .HasForeignKey(d => d.LgaId)
                    .HasConstraintName("FK_OfficeLocalGovernments_Lgas");
            });

            modelBuilder.Entity<OfficeRank>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RankShortName).HasMaxLength(50);

                entity.Property(e => e.RankTitle).HasMaxLength(200);
            });

            modelBuilder.Entity<OfficeState>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCaptured).HasColumnType("datetime");

                entity.Property(e => e.OfficeId)
                    .HasColumnName("OfficeID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.StateCode).HasMaxLength(2);

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.OfficeStates)
                    .HasForeignKey(d => d.OfficeId)
                    .HasConstraintName("FK_OfficeStates_Offices");

                entity.HasOne(d => d.StateCodeNavigation)
                    .WithMany(p => p.OfficeStates)
                    .HasForeignKey(d => d.StateCode)
                    .HasConstraintName("FK_OfficeStates_States");
            });

            modelBuilder.Entity<PaymentPin>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentPinSettingId).HasColumnName("PaymentPinSettingID");

                entity.Property(e => e.SerialPin).HasMaxLength(100);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PaymentPins)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_PaymentPins_ApplicationUsers");

                entity.HasOne(d => d.PaymentPinSetting)
                    .WithMany(p => p.PaymentPins)
                    .HasForeignKey(d => d.PaymentPinSettingId)
                    .HasConstraintName("FK_PaymentPins_PaymentPinSettings");
            });

            modelBuilder.Entity<PaymentPinSetting>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(800);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PaymentPinSettings)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_PaymentPinSettings_ApplicationUsers");

                entity.HasOne(d => d.SchoolRecognitionTypeNavigation)
                    .WithMany(p => p.PaymentPinSettings)
                    .HasForeignKey(d => d.SchoolRecognitionType)
                    .HasConstraintName("FK_PaymentPinSettings_SchoolRecognitionType");
            });

            modelBuilder.Entity<PinHistory>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateActive).HasColumnType("datetime");

                entity.Property(e => e.PinId).HasColumnName("PinID");

                entity.Property(e => e.SchoolProfileId).HasColumnName("SchoolProfileID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PinHistories)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_PinHistories_Users");

                entity.HasOne(d => d.Pin)
                    .WithMany(p => p.PinHistories)
                    .HasForeignKey(d => d.PinId)
                    .HasConstraintName("FK_PinHistory_PIN");

                entity.HasOne(d => d.SchoolProfile)
                    .WithMany(p => p.PinHistories)
                    .HasForeignKey(d => d.SchoolProfileId)
                    .HasConstraintName("FK_PinHistorys_SchoolProfiles");
            });

            modelBuilder.Entity<SanctionSetting>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SanctionName).HasMaxLength(500);
            });

            modelBuilder.Entity<SchoolCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryCode)
                    .HasName("PK_SchoolCategories");

                entity.Property(e => e.CategoryCode).HasMaxLength(2);

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<SchoolDeficiency>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdditionalInfo).HasMaxLength(500);

                entity.Property(e => e.FacilitySettingId).HasColumnName("FacilitySettingID");

                entity.Property(e => e.SchoolRecognitionTrailId)
                    .HasColumnName("SchoolRecognitionTrailID")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<SchoolFacility>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FacilitySettingId).HasColumnName("FacilitySettingID");

                entity.Property(e => e.SchoolRecognitionTrailId).HasColumnName("SchoolRecognitionTrailID");

                entity.Property(e => e.ValueApplied).HasMaxLength(1000);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SchoolFacilities)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_FacilityInformation_Users");

                entity.HasOne(d => d.FacilitySetting)
                    .WithMany(p => p.SchoolFacilities)
                    .HasForeignKey(d => d.FacilitySettingId)
                    .HasConstraintName("FK_SchoolFacilities_FacilitySettings");
            });

            modelBuilder.Entity<SchoolPayment>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.PinId).HasColumnName("PinID");

                entity.Property(e => e.ReceiptNo).HasMaxLength(50);

                entity.Property(e => e.SchoolProfileId).HasColumnName("SchoolProfileID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SchoolPayments)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_SchoolPayment_User");

                entity.HasOne(d => d.Pin)
                    .WithMany(p => p.SchoolPayments)
                    .HasForeignKey(d => d.PinId)
                    .HasConstraintName("FK_SchoolPayment_PIN");

                entity.HasOne(d => d.SchoolProfile)
                    .WithMany(p => p.SchoolPayments)
                    .HasForeignKey(d => d.SchoolProfileId)
                    .HasConstraintName("FK_SchoolPayments_SchoolProfiles");
            });

            modelBuilder.Entity<SchoolProfile>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CategoryCode).HasMaxLength(2);

                entity.Property(e => e.CentreNo).HasMaxLength(7);

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.LgaId).HasColumnName("LgaID");

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.Property(e => e.SchoolAddress).HasMaxLength(500);

                entity.Property(e => e.SchoolName).HasMaxLength(200);

                entity.HasOne(d => d.CategoryCodeNavigation)
                    .WithMany(p => p.SchoolProfiles)
                    .HasForeignKey(d => d.CategoryCode)
                    .HasConstraintName("FK_SchoolProfiles_SchoolCategorys");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.SchoolProfiles)
                    .HasForeignKey(d => d.CountryCode)
                    .HasConstraintName("FK_SchoolProfiles_Countrys");

                entity.HasOne(d => d.Lga)
                    .WithMany(p => p.SchoolProfiles)
                    .HasForeignKey(d => d.LgaId)
                    .HasConstraintName("FK_SchoolProfiles_Lgas");
            });

            modelBuilder.Entity<SchoolRecognitionTrail>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.DateInspected).HasColumnType("datetime");

                entity.Property(e => e.DateRequested)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateReviewed).HasColumnType("datetime");

                entity.Property(e => e.DateSupervised).HasColumnType("datetime");

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.OriginalName).HasMaxLength(200);

                entity.Property(e => e.PreferredName).HasMaxLength(200);

                entity.Property(e => e.SchoolPaymentId).HasColumnName("SchoolPaymentID");
            });

            modelBuilder.Entity<SchoolRecognitionTrailRejection>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateRejected)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<SchoolRecognitionType>(entity =>
            {
                entity.ToTable("SchoolRecognitionType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountCode).HasMaxLength(10);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RecognitionCode).HasMaxLength(2);

                entity.Property(e => e.TypeOfRecognition).HasMaxLength(50);
            });

            modelBuilder.Entity<SchoolStaffCategory>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RankOrder).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SchoolStaffDegree>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ApprovedCourseId).HasColumnName("ApprovedCourseID");

                entity.Property(e => e.CourseTitle).HasMaxLength(400);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DegreeId).HasColumnName("DegreeID");

                entity.Property(e => e.DocumentPath).HasMaxLength(500);

                entity.Property(e => e.FileExtension).HasMaxLength(50);

                entity.Property(e => e.FileName).HasMaxLength(800);

                entity.Property(e => e.FileSize).HasDefaultValueSql("((1))");

                entity.Property(e => e.FileType).HasMaxLength(500);

                entity.Property(e => e.SchoolStaffProfileId).HasColumnName("SchoolStaffProfileID");

                entity.HasOne(d => d.ApprovedCourse)
                    .WithMany(p => p.SchoolStaffDegrees)
                    .HasForeignKey(d => d.ApprovedCourseId)
                    .HasConstraintName("FK_SchoolStaffDegrees_ApprovedCourses");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SchoolStaffDegrees)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_SchoolStaffDegrees_Users");

                entity.HasOne(d => d.SchoolStaffProfile)
                    .WithMany(p => p.SchoolStaffDegrees)
                    .HasForeignKey(d => d.SchoolStaffProfileId)
                    .HasConstraintName("FK_SchoolStaffDegrees_SchoolStaffProfiles");
            });

            modelBuilder.Entity<SchoolStaffProfile>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ContactAddress).HasMaxLength(150);

                entity.Property(e => e.DateEmployed).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(150);

                entity.Property(e => e.HasTrcn)
                    .HasColumnName("HasTRCN")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Othernames).HasMaxLength(100);

                entity.Property(e => e.PhoneNo).HasMaxLength(50);

                entity.Property(e => e.SchoolProfileId).HasColumnName("SchoolProfileID");

                entity.Property(e => e.SchoolStaffCategoryId).HasColumnName("SchoolStaffCategoryID");

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.Property(e => e.TitleId).HasColumnName("TitleID");

                entity.Property(e => e.Trcn)
                    .HasColumnName("TRCN")
                    .HasMaxLength(150);

                entity.HasOne(d => d.SchoolProfile)
                    .WithMany(p => p.SchoolStaffProfiles)
                    .HasForeignKey(d => d.SchoolProfileId)
                    .HasConstraintName("FK_SchoolStaffProfiles_SchoolProfiles");

                entity.HasOne(d => d.SchoolStaffCategory)
                    .WithMany(p => p.SchoolStaffProfiles)
                    .HasForeignKey(d => d.SchoolStaffCategoryId)
                    .HasConstraintName("FK_SchoolStaffProfiles_SchoolStaffCategories");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.SchoolStaffProfiles)
                    .HasForeignKey(d => d.TitleId)
                    .HasConstraintName("FK_SchoolStaffProfiles_Titles");
            });

            modelBuilder.Entity<SecurityConfig>(entity =>
            {
                entity.ToTable("SecurityConfig");

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

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.StateCode);

                entity.Property(e => e.StateCode).HasMaxLength(2);

                entity.Property(e => e.StateName).HasMaxLength(50);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.SubjectCode)
                    .HasName("PK_Subject");

                entity.Property(e => e.SubjectCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LongCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TitleName).HasMaxLength(200);
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.GroupTitle).HasMaxLength(50);

                entity.Property(e => e.UrlHome).HasMaxLength(256);
            });

            modelBuilder.Entity<UserSecureAccount>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SecurityQuestionId).HasColumnName("SecurityQuestionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserSecurityQuestion>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SecurityQuestion).HasMaxLength(300);
            });

            modelBuilder.Entity<VwApplicationUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwApplicationUsers");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.GroupTitle).HasMaxLength(50);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Lpno)
                    .HasColumnName("LPNO")
                    .HasMaxLength(30);

                entity.Property(e => e.Othernames).HasMaxLength(50);

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.Property(e => e.RankId).HasColumnName("RankID");

                entity.Property(e => e.RankShortName).HasMaxLength(50);

                entity.Property(e => e.RankTitle).HasMaxLength(200);

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.Property(e => e.UrlHome).HasMaxLength(256);

                entity.Property(e => e.UserGroupId).HasColumnName("UserGroupID");
            });

            modelBuilder.Entity<VwApplicationUserGroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwApplicationUserGroups");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.GroupTitle).HasMaxLength(50);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UrlHome).HasMaxLength(256);
            });

            modelBuilder.Entity<VwApplicationUserSecurityQuestion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwApplicationUserSecurityQuestions");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SecurityQuestion).HasMaxLength(300);

                entity.Property(e => e.SecurityQuestionId).HasColumnName("SecurityQuestionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
