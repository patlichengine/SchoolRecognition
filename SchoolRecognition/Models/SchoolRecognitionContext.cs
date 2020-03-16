using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SchoolRecognition.Models
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

        public virtual DbSet<LocalGovernments> LocalGovernments { get; set; }
        public virtual DbSet<Offices> Offices { get; set; }
        public virtual DbSet<PinHistories> PinHistories { get; set; }
        public virtual DbSet<Pins> Pins { get; set; }
        public virtual DbSet<Ranks> Ranks { get; set; }
        public virtual DbSet<RecognitionTypes> RecognitionTypes { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SchoolCategories> SchoolCategories { get; set; }
        public virtual DbSet<SchoolPayments> SchoolPayments { get; set; }
        public virtual DbSet<Schools> Schools { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<Titles> Titles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=SchoolRecognition;User id=sa;Password=19911023;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalGovernments>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(3);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.LocalGovernments)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_LocalGovernments_State");
            });

            modelBuilder.Entity<Offices>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StateId).HasColumnName("StateID");
            });

            modelBuilder.Entity<PinHistories>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

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
                entity.ToTable("PINs");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.RecognitionTypeId).HasColumnName("RecognitionTypeID");

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
                    .ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<RecognitionTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<SchoolCategories>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(2);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<SchoolPayments>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

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
                    .ValueGeneratedNever();

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

            modelBuilder.Entity<States>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("IX_State")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

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
                    .ValueGeneratedNever();

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.Lpno)
                    .HasColumnName("LPNO")
                    .HasMaxLength(30);

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.Property(e => e.RankId).HasColumnName("RankID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RankId)
                    .HasConstraintName("FK_User_Rank");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
