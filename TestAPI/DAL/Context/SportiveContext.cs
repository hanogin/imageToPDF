using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DAL.Entities;

namespace DAL.Context
{
    public partial class SportiveContext : DbContext
    {
        public SportiveContext()
        {
        }

        public SportiveContext(DbContextOptions<SportiveContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppAccount> AppAccounts { get; set; } = null!;
        public virtual DbSet<AppClient> AppClients { get; set; } = null!;
        public virtual DbSet<AppClientFile> AppClientFiles { get; set; } = null!;
        public virtual DbSet<AppClientInLesson> AppClientInLessons { get; set; } = null!;
        public virtual DbSet<AppInstructor> AppInstructors { get; set; } = null!;
        public virtual DbSet<AppLesson> AppLessons { get; set; } = null!;
        public virtual DbSet<AppMaccabyReportHistory> AppMaccabyReportHistories { get; set; } = null!;
        public virtual DbSet<AppProgram> AppPrograms { get; set; } = null!;
        public virtual DbSet<AppSubscription> AppSubscriptions { get; set; } = null!;
        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;
        public virtual DbSet<AppVisit> AppVisits { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<TClientFileType> TClientFileTypes { get; set; } = null!;
        public virtual DbSet<TClientType> TClientTypes { get; set; } = null!;
        public virtual DbSet<TDay> TDays { get; set; } = null!;
        public virtual DbSet<TKupatHolim> TKupatHolims { get; set; } = null!;
        public virtual DbSet<TPaymentType> TPaymentTypes { get; set; } = null!;
        public virtual DbSet<TUserRole> TUserRoles { get; set; } = null!;
        public virtual DbSet<TempImpoirtClientTable> TempImpoirtClientTables { get; set; } = null!;
        public virtual DbSet<VClientVisit> VClientVisits { get; set; } = null!;
        public virtual DbSet<VClientWithSub> VClientWithSubs { get; set; } = null!;
        public virtual DbSet<VSubscription> VSubscriptions { get; set; } = null!;
        public virtual DbSet<VSubscriptionsStatus> VSubscriptionsStatuses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.ToTable("App_Account");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.AppAccounts)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_Account_App_Client");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.AppAccounts)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .HasConstraintName("FK_App_Account_T_PaymentType");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.AppAccounts)
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("FK_App_Account_App_Subscription");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AppAccounts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_App_Account_App_User");
            });

            modelBuilder.Entity<AppClient>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.ToTable("App_Client");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.IdentityId).HasMaxLength(9);

                entity.Property(e => e.JoinDate).HasColumnType("date");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Phone1).HasMaxLength(10);

                entity.Property(e => e.Phone2).HasMaxLength(10);

                entity.HasOne(d => d.ClientType)
                    .WithMany(p => p.AppClients)
                    .HasForeignKey(d => d.ClientTypeId)
                    .HasConstraintName("FK_App_Client_T_ClientType");

                entity.HasOne(d => d.KupatHolim)
                    .WithMany(p => p.AppClients)
                    .HasForeignKey(d => d.KupatHolimId)
                    .HasConstraintName("FK_App_Client_T_KupatHolim");
            });

            modelBuilder.Entity<AppClientFile>(entity =>
            {
                entity.HasKey(e => e.ClientFileId);

                entity.ToTable("App_ClientFile");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.File).HasColumnName("FIle");

                entity.Property(e => e.FileName).HasMaxLength(255);

                entity.Property(e => e.FileType).HasMaxLength(255);

                entity.HasOne(d => d.ClientFileType)
                    .WithMany(p => p.AppClientFiles)
                    .HasForeignKey(d => d.ClientFileTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_ClientFile_App_Client");
            });

            modelBuilder.Entity<AppClientInLesson>(entity =>
            {
                entity.HasKey(e => e.ClientInLessonId);

                entity.ToTable("App_ClientInLesson");

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.AppClientInLessons)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_ClientInLesson_App_Client");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.AppClientInLessons)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_ClientInLesson_App_Lesson");
            });

            modelBuilder.Entity<AppInstructor>(entity =>
            {
                entity.HasKey(e => e.InstructorId);

                entity.ToTable("App_Instructor");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Cell).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(10);
            });

            modelBuilder.Entity<AppLesson>(entity =>
            {
                entity.HasKey(e => e.LessonId);

                entity.ToTable("App_Lesson");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Day)
                    .WithMany(p => p.AppLessons)
                    .HasForeignKey(d => d.DayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_Lesson_T_Days");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.AppLessons)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_Lesson_App_Instructor");
            });

            modelBuilder.Entity<AppMaccabyReportHistory>(entity =>
            {
                entity.HasKey(e => e.MaccabyReportHistoryId)
                    .HasName("PK_App_Maccaby_Report");

                entity.ToTable("App_Maccaby_ReportHistory");

                entity.Property(e => e.ReportMonth).HasColumnType("date");
            });

            modelBuilder.Entity<AppProgram>(entity =>
            {
                entity.HasKey(e => e.ProgramId);

                entity.ToTable("App_Program");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<AppSubscription>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId);

                entity.ToTable("App_Subscription");

                entity.Property(e => e.ActionDate).HasColumnType("datetime");

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.AppSubscriptions)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_Subscription_App_Client");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.AppSubscriptions)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_Subscription_App_Program");
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("App_User");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_User_T_UserRole");
            });

            modelBuilder.Entity<AppVisit>(entity =>
            {
                entity.HasKey(e => e.VisitId);

                entity.ToTable("App_Visit");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.AppVisits)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_Visit_App_Client");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.AppVisits)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_Visit_App_Instructor");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.AppVisits)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_Visit_App_Lesson");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.AppVisits)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_Visit_App_Subscription");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Log");

                entity.Property(e => e.ClientIp)
                    .HasMaxLength(100)
                    .HasColumnName("ClientIP");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.LogLevel).HasMaxLength(128);

                entity.Property(e => e.UserName).HasMaxLength(200);
            });

            modelBuilder.Entity<TClientFileType>(entity =>
            {
                entity.HasKey(e => e.ClientFileTypeId);

                entity.ToTable("T_ClientFileType");

                entity.Property(e => e.ClientFileTypeId).ValueGeneratedNever();

                entity.Property(e => e.ClientFileTypeDesc).HasMaxLength(200);
            });

            modelBuilder.Entity<TClientType>(entity =>
            {
                entity.HasKey(e => e.ClientTypeId);

                entity.ToTable("T_ClientType");

                entity.Property(e => e.ClientTypeId).ValueGeneratedNever();

                entity.Property(e => e.ClientTypeDesc).HasMaxLength(100);
            });

            modelBuilder.Entity<TDay>(entity =>
            {
                entity.HasKey(e => e.DaysId);

                entity.ToTable("T_Days");

                entity.Property(e => e.DaysId).ValueGeneratedNever();

                entity.Property(e => e.DaysDesc).HasMaxLength(15);
            });

            modelBuilder.Entity<TKupatHolim>(entity =>
            {
                entity.HasKey(e => e.KupatHolimId);

                entity.ToTable("T_KupatHolim");

                entity.Property(e => e.KupatHolimId).ValueGeneratedNever();

                entity.Property(e => e.KupatHolimDesc).HasMaxLength(50);
            });

            modelBuilder.Entity<TPaymentType>(entity =>
            {
                entity.HasKey(e => e.PaymentTypeId);

                entity.ToTable("T_PaymentType");

                entity.Property(e => e.PaymentTypeId).ValueGeneratedNever();

                entity.Property(e => e.PaymentTypeDesc).HasMaxLength(20);
            });

            modelBuilder.Entity<TUserRole>(entity =>
            {
                entity.HasKey(e => e.UserRoleId);

                entity.ToTable("T_UserRole");

                entity.Property(e => e.UserRoleId).ValueGeneratedNever();

                entity.Property(e => e.UserRoleDesc).HasMaxLength(100);
            });

            modelBuilder.Entity<TempImpoirtClientTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Temp_ImpoirtClientTable");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.CalientTypeDesc).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.IdentityId).HasMaxLength(100);

                entity.Property(e => e.KupatHolimDesc).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Phone1).HasMaxLength(100);

                entity.Property(e => e.Phone2).HasMaxLength(100);
            });

            modelBuilder.Entity<VClientVisit>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Client_Visit");

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Day).HasMaxLength(15);

                entity.Property(e => e.InstructorFullName).HasMaxLength(101);

                entity.Property(e => e.LessonName).HasMaxLength(50);

                entity.Property(e => e.ProgramName).HasMaxLength(50);
            });

            modelBuilder.Entity<VClientWithSub>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_ClientWithSub");

                entity.Property(e => e.ClientFullName).HasMaxLength(101);

                entity.Property(e => e.ClientId).HasColumnName("clientId");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.FullNameWithPhone1).HasMaxLength(114);

                entity.Property(e => e.Phone1).HasMaxLength(10);

                entity.Property(e => e.Phone2).HasMaxLength(10);

                entity.Property(e => e.ProgramName).HasMaxLength(50);

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");
            });

            modelBuilder.Entity<VSubscription>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Subscriptions");

                entity.Property(e => e.ProgramName).HasMaxLength(50);
            });

            modelBuilder.Entity<VSubscriptionsStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_SubscriptionsStatus");

                entity.Property(e => e.ClientId).HasColumnName("clientId");

                entity.Property(e => e.SubScriptionId).ValueGeneratedOnAdd();

                entity.Property(e => e.SubsStatusLabel)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SubsStatusStyle)
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
