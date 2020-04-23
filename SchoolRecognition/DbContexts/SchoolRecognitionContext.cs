using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using SchoolRecognition.Entities;

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

        public virtual DbSet<AuditTrail> AuditTrail { get; set; }
        public virtual DbSet<Centres> Centres { get; set; }
        public virtual DbSet<Dblogger> Dblogger { get; set; }
        public virtual DbSet<LocalGovernments> LocalGovernments { get; set; }
        public virtual DbSet<LocationTypes> LocationTypes { get; set; }
        public virtual DbSet<OfficeTypes> OfficeTypes { get; set; }
        public virtual DbSet<Offices> Offices { get; set; }
        public virtual DbSet<PinHistories> PinHistories { get; set; }
        public virtual DbSet<Pins> Pins { get; set; }
        public virtual DbSet<Ranks> Ranks { get; set; }
        public virtual DbSet<RecognitionTypes> RecognitionTypes { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SchoolCategories> SchoolCategories { get; set; }
        public virtual DbSet<SchoolPayments> SchoolPayments { get; set; }
        public virtual DbSet<Schools> Schools { get; set; }
        public virtual DbSet<SecurityConfig> SecurityConfig { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<Titles> Titles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VwUsers> VwUsers { get; set; }


        public IConfiguration Configuration { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                var connection = Configuration.GetConnectionString("SchoolRecognitionConnection");
                optionsBuilder.UseSqlServer(connection);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            });

            modelBuilder.Entity<Centres>(entity =>
            {
                entity.HasIndex(e => e.CentreNo)
                    .HasName("IX_Centres")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CentreName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CentreNo)
                    .IsRequired()
                    .HasMaxLength(7);

                entity.Property(e => e.SchoolCategoryId).HasColumnName("SchoolCategoryID");

                entity.HasOne(d => d.SchoolCategory)
                    .WithMany(p => p.Centres)
                    .HasForeignKey(d => d.SchoolCategoryId)
                    .HasConstraintName("FK_Centres_SchoolCategories");
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

                entity.HasOne(d => d.OfficeType)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.OfficeTypeId)
                    .HasConstraintName("FK_Offices_OfficeTypes");
            });

            modelBuilder.Entity<PinHistories>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateActive).HasColumnType("date");

                entity.Property(e => e.PinId).HasColumnName("PinID");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

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

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

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

            modelBuilder.Entity<Schools>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.LgId).HasColumnName("LgID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Schools)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_School_SchoolCategory");

                entity.HasOne(d => d.Lg)
                    .WithMany(p => p.Schools)
                    .HasForeignKey(d => d.LgId)
                    .HasConstraintName("FK_School_LocalGovernment");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Schools)
                    .HasForeignKey(d => d.OfficeId)
                    .HasConstraintName("FK_School_Office");
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
                entity.HasIndex(e => e.Code)
                    .HasName("IX_State")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(3);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Titles>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
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
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RankId)
                    .HasConstraintName("FK_User_Rank");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<VwUsers>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwUsers");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Lpno)
                    .HasColumnName("LPNO")
                    .HasMaxLength(30);

                entity.Property(e => e.Othernames).HasMaxLength(50);

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.Property(e => e.RankId).HasColumnName("RankID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
