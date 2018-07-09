using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace app.Models
{
    public partial class FusionAppContext : DbContext
    {
        public FusionAppContext()
        {
        }

    // The following constructor will allow configuration to be passed into
    // the context by dependency injection
    public FusionAppContext(DbContextOptions<FusionAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agencies> Agencies { get; set; }
        public virtual DbSet<AgencyProfiles> AgencyProfiles { get; set; }
        public virtual DbSet<CitizenProfiles> CitizenProfiles { get; set; }
        public virtual DbSet<Citizens> Citizens { get; set; }
        public virtual DbSet<DimDate> DimDate { get; set; }
        public virtual DbSet<DimTime> DimTime { get; set; }
        public virtual DbSet<FakeTesters> FakeTesters { get; set; }
        public virtual DbSet<TestEnrollment> TestEnrollment { get; set; }
        public virtual DbSet<TestOpportunities> TestOpportunities { get; set; }
        public virtual DbSet<TestOutcomes> TestOutcomes { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agencies>(entity =>
            {
                entity.HasKey(e => e.AgencyId);

                entity.Property(e => e.AgencyName).HasMaxLength(250);

                entity.Property(e => e.AgencyUrl).HasMaxLength(500);
            });

            modelBuilder.Entity<AgencyProfiles>(entity =>
            {
                entity.HasKey(e => e.AgencyProfileId);

                entity.Property(e => e.AgencySection).HasMaxLength(150);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.State).HasMaxLength(100);

                entity.Property(e => e.ZipCode).HasMaxLength(20);
            });

            modelBuilder.Entity<CitizenProfiles>(entity =>
            {
                entity.HasKey(e => e.CitizenProfileId);

                entity.Property(e => e.Age).HasMaxLength(10);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.State).HasMaxLength(100);

                entity.Property(e => e.ZipCode).HasMaxLength(20);

                entity.HasOne(d => d.Citizen)
                    .WithMany(p => p.CitizenProfiles)
                    .HasForeignKey(d => d.CitizenId)
                    .HasConstraintName("FK_dbo.TesterProfiles_dbo.Testers_TesterId");
            });

            modelBuilder.Entity<Citizens>(entity =>
            {
                entity.HasKey(e => e.CitizenId);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.IsConfirmed)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('No')");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<DimDate>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.DayOfWeek)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.DaySuffix)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.DowinMonth).HasColumnName("DOWInMonth");

                entity.Property(e => e.HolidayText)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Month)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.MonthName)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.QuarterName)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.StandardDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DimTime>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AmPm)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Hour)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.MilitaryHour)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Minute)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Second)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StandardTime)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FakeTesters>(entity =>
            {
                entity.Property(e => e.Age)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsConfirmed)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NoOfTests)
                    .HasColumnName("No  of Tests")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PointsEarned)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode)
                    .HasColumnName("ZIPCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TestEnrollment>(entity =>
            {
                entity.HasKey(e => e.TestId);

                entity.Property(e => e.CompletedTest)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(N'No')");

                entity.HasOne(d => d.TestOpportunity)
                    .WithMany(p => p.TestEnrollment)
                    .HasForeignKey(d => d.TestOpportunityId)
                    .HasConstraintName("FK_TestEnrollment_TestOpportunities");

                entity.HasOne(d => d.Tester)
                    .WithMany(p => p.TestEnrollment)
                    .HasForeignKey(d => d.TesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestEnrollment_Testers");
            });

            modelBuilder.Entity<TestOpportunities>(entity =>
            {
                entity.HasKey(e => e.TestOpportunityId);

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TestUrl).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.TestOpportunities)
                    .HasForeignKey(d => d.AgencyId)
                    .HasConstraintName("FK_dbo.TestOpportunities_dbo.Organizations_OrganizationId");
            });

            modelBuilder.Entity<TestOutcomes>(entity =>
            {
                entity.HasKey(e => e.TestOutcomeId);

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TestResultUrl).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.HasOne(d => d.Citizen)
                    .WithMany(p => p.TestOutcomes)
                    .HasForeignKey(d => d.CitizenId)
                    .HasConstraintName("FK_dbo.TestOutcomes_dbo.Testers_TesterId");
            });
        }
    }
}
