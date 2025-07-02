// ===================================================================
// ALISTO APP - ENTITY FRAMEWORK DB CONTEXT
// ===================================================================
// Database context configuration for Entity Framework
// ===================================================================

using Microsoft.EntityFrameworkCore;
using Alisto.Api.Models;
using System.Text.Json;
using Alisto.Api.Enums;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace Alisto.Api.Data
{
    public class AlistoDbContext : DbContext
    {
        public AlistoDbContext(DbContextOptions<AlistoDbContext> options) : base(options)
        {

        }

        // ===================================================================
        // DB SETS (TABLES)
        // ===================================================================

        // User Management
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }

        // News and Announcements
        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        // Services and Appointments
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<CityService> CityServices { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        // Service-specific requests
        public DbSet<CivilRegistryRequest> CivilRegistryRequests { get; set; }
        public DbSet<BusinessPermitRequest> BusinessPermitRequests { get; set; }
        public DbSet<HealthServiceRequest> HealthServiceRequests { get; set; }
        public DbSet<EducationServiceRequest> EducationServiceRequests { get; set; }
        public DbSet<SocialServiceRequest> SocialServiceRequests { get; set; }
        public DbSet<TaxServiceRequest> TaxServiceRequests { get; set; }

        // Issue Reporting
        public DbSet<IssueReport> IssueReports { get; set; }
        public DbSet<IssuePhoto> IssuePhotos { get; set; }
        public DbSet<IssueUpdate> IssueUpdates { get; set; }

        // Tourism and Transparency
        public DbSet<TouristSpot> TouristSpots { get; set; }
        public DbSet<PublicProject> PublicProjects { get; set; }

        // Emergency and Hotlines
        public DbSet<EmergencyHotline> EmergencyHotlines { get; set; }

        // System and Configuration
        public DbSet<SystemConfiguration> SystemConfigurations { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        // Notifications and Files
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }

        // Statistics and Feedback
        public DbSet<AppUsageStatistics> AppUsageStatistics { get; set; }
        public DbSet<ServiceStatistics> ServiceStatistics { get; set; }
        public DbSet<ServiceFeedback> ServiceFeedbacks { get; set; }
        public DbSet<AppFeedback> AppFeedbacks { get; set; }

        // ===================================================================
        // MODEL CONFIGURATION
        // ===================================================================

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===================================================================
            // INDEXES FOR PERFORMANCE
            // ===================================================================

            // User indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber);

            // News indexes
            modelBuilder.Entity<NewsArticle>()
                .HasIndex(n => n.Category);

            modelBuilder.Entity<NewsArticle>()
                .HasIndex(n => n.PublishedDate);

            modelBuilder.Entity<NewsArticle>()
                .HasIndex(n => new { n.IsFeatured, n.IsTrending });

            // Appointment indexes
            modelBuilder.Entity<Appointment>()
                .HasIndex(a => a.ReferenceNumber)
                .IsUnique();

            modelBuilder.Entity<Appointment>()
                .HasIndex(a => a.Status);

            modelBuilder.Entity<Appointment>()
                .HasIndex(a => a.AppointmentDate);

            modelBuilder.Entity<Appointment>()
                .HasIndex(a => new { a.UserId, a.Status });

            // Issue Report indexes
            modelBuilder.Entity<IssueReport>()
                .HasIndex(i => i.ReferenceNumber)
                .IsUnique();

            modelBuilder.Entity<IssueReport>()
                .HasIndex(i => i.Status);

            modelBuilder.Entity<IssueReport>()
                .HasIndex(i => i.Category);

            modelBuilder.Entity<IssueReport>()
                .HasIndex(i => i.UrgencyLevel);

            // Tourist Spot indexes
            modelBuilder.Entity<TouristSpot>()
                .HasIndex(t => t.IsActive);

            modelBuilder.Entity<TouristSpot>()
                .HasIndex(t => t.Rating);

            // Public Project indexes
            modelBuilder.Entity<PublicProject>()
                .HasIndex(p => p.Status);

            modelBuilder.Entity<PublicProject>()
                .HasIndex(p => p.IsPublic);

            // System Configuration indexes
            modelBuilder.Entity<SystemConfiguration>()
                .HasIndex(s => s.Key)
                .IsUnique();

            // Notification indexes
            modelBuilder.Entity<Notification>()
                .HasIndex(n => new { n.UserId, n.IsRead });

            modelBuilder.Entity<Notification>()
                .HasIndex(n => n.CreatedAt);

            // ===================================================================
            // RELATIONSHIPS AND CONSTRAINTS
            // ===================================================================

            // User Sessions
            modelBuilder.Entity<UserSession>()
                .HasOne(us => us.User)
                .WithMany(u => u.Sessions)
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Appointments
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.User)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Service-specific requests (One-to-One with Appointment)
            modelBuilder.Entity<CivilRegistryRequest>()
                .HasOne(cr => cr.Appointment)
                .WithOne(a => a.CivilRegistryRequest)
                .HasForeignKey<CivilRegistryRequest>(cr => cr.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BusinessPermitRequest>()
                .HasOne(br => br.Appointment)
                .WithOne(a => a.BusinessPermitRequest)
                .HasForeignKey<BusinessPermitRequest>(br => br.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HealthServiceRequest>()
                .HasOne(hr => hr.Appointment)
                .WithOne(a => a.HealthServiceRequest)
                .HasForeignKey<HealthServiceRequest>(hr => hr.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EducationServiceRequest>()
                .HasOne(er => er.Appointment)
                .WithOne(a => a.EducationServiceRequest)
                .HasForeignKey<EducationServiceRequest>(er => er.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SocialServiceRequest>()
                .HasOne(sr => sr.Appointment)
                .WithOne(a => a.SocialServiceRequest)
                .HasForeignKey<SocialServiceRequest>(sr => sr.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaxServiceRequest>()
                .HasOne(tr => tr.Appointment)
                .WithOne(a => a.TaxServiceRequest)
                .HasForeignKey<TaxServiceRequest>(tr => tr.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // City Services
            modelBuilder.Entity<CityService>()
                .HasOne(cs => cs.Category)
                .WithMany(sc => sc.Services)
                .HasForeignKey(cs => cs.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Issue Reports
            modelBuilder.Entity<IssueReport>()
                .HasOne(ir => ir.User)
                .WithMany(u => u.IssueReports)
                .HasForeignKey(ir => ir.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<IssuePhoto>()
                .HasOne(ip => ip.IssueReport)
                .WithMany(ir => ir.Photos)
                .HasForeignKey(ip => ip.IssueReportId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IssueUpdate>()
                .HasOne(iu => iu.IssueReport)
                .WithMany(ir => ir.Updates)
                .HasForeignKey(iu => iu.IssueReportId)
                .OnDelete(DeleteBehavior.Cascade);

            // Notifications
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Service Feedback
            modelBuilder.Entity<ServiceFeedback>()
                .HasOne(sf => sf.Appointment)
                .WithMany()
                .HasForeignKey(sf => sf.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceFeedback>()
                .HasOne(sf => sf.User)
                .WithMany()
                .HasForeignKey(sf => sf.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceFeedback>()
                .HasOne(sf => sf.Service)
                .WithMany()
                .HasForeignKey(sf => sf.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // App Feedback
            modelBuilder.Entity<AppFeedback>()
                .HasOne(af => af.User)
                .WithMany()
                .HasForeignKey(af => af.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // ===================================================================
            // ENUM CONVERSIONS
            // ===================================================================

            // Convert enums to strings for better database storage
            modelBuilder.Entity<NewsArticle>()
                .Property(e => e.Category)
                .HasConversion<string>();

            modelBuilder.Entity<NewsArticle>()
                .Property(e => e.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Announcement>()
                .Property(e => e.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Announcement>()
                .Property(e => e.Priority)
                .HasConversion<string>();

            modelBuilder.Entity<Appointment>()
                .Property(e => e.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Appointment>()
                .Property(e => e.PaymentStatus)
                .HasConversion<string>();

            modelBuilder.Entity<IssueReport>()
                .Property(e => e.Category)
                .HasConversion<string>();

            modelBuilder.Entity<IssueReport>()
                .Property(e => e.UrgencyLevel)
                .HasConversion<string>();

            modelBuilder.Entity<IssueReport>()
                .Property(e => e.Status)
                .HasConversion<string>();

            modelBuilder.Entity<IssueReport>()
                .Property(e => e.Priority)
                .HasConversion<string>();

            modelBuilder.Entity<PublicProject>()
                .Property(e => e.Status)
                .HasConversion<string>();

            modelBuilder.Entity<SystemConfiguration>()
                .Property(e => e.DataType)
                .HasConversion<string>();

            modelBuilder.Entity<Notification>()
                .Property(e => e.Type)
                .HasConversion<string>();

            modelBuilder.Entity<AppFeedback>()
                .Property(e => e.Type)
                .HasConversion<string>();

            modelBuilder.Entity<AppFeedback>()
                .Property(e => e.Status)
                .HasConversion<string>();

            // Service-specific enums
            modelBuilder.Entity<CivilRegistryRequest>()
                .Property(e => e.DocumentType)
                .HasConversion<string>();

            modelBuilder.Entity<BusinessPermitRequest>()
                .Property(e => e.ServiceType)
                .HasConversion<string>();

            modelBuilder.Entity<HealthServiceRequest>()
                .Property(e => e.ServiceType)
                .HasConversion<string>();

            modelBuilder.Entity<EducationServiceRequest>()
                .Property(e => e.ServiceType)
                .HasConversion<string>();

            modelBuilder.Entity<SocialServiceRequest>()
                .Property(e => e.ServiceType)
                .HasConversion<string>();

            modelBuilder.Entity<TaxServiceRequest>()
                .Property(e => e.ServiceType)
                .HasConversion<string>();

            modelBuilder.Entity<IssueUpdate>()
                .Property(e => e.UpdateType)
                .HasConversion<string>();

            modelBuilder.Entity<ServiceStatistics>()
                .Property(e => e.Period)
                .HasConversion<string>();

            // ===================================================================
            // DEFAULT VALUES
            // ===================================================================

            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<NewsArticle>()
                .Property(n => n.ViewCount)
                .HasDefaultValue(0);

            modelBuilder.Entity<TouristSpot>()
                .Property(t => t.ViewCount)
                .HasDefaultValue(0);

            modelBuilder.Entity<TouristSpot>()
                .Property(t => t.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<PublicProject>()
                .Property(p => p.IsPublic)
                .HasDefaultValue(true);

            modelBuilder.Entity<Notification>()
                .Property(n => n.IsRead)
                .HasDefaultValue(false);

            // ===================================================================
            // SEED DATA (OPTIONAL)
            // ===================================================================

            SeedData(modelBuilder);
        }
       
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Service Categories
            modelBuilder.Entity<ServiceCategory>().HasData(
                new ServiceCategory { Id = 1, Name = "Civil Registry", Description = "Birth certificates, marriage certificates, death certificates", IconName = "FileText", IsActive = true, SortOrder = 1 },
                new ServiceCategory { Id = 2, Name = "Business Permits", Description = "New business registration, permit renewal, business closure", IconName = "Building", IsActive = true, SortOrder = 2 },
                new ServiceCategory { Id = 3, Name = "Social Services", Description = "Senior citizen ID, PWD ID, indigency certificates", IconName = "Users", IsActive = true, SortOrder = 3 },
                new ServiceCategory { Id = 4, Name = "Tax Services", Description = "Real property tax, business tax, tax clearance", IconName = "CreditCard", IsActive = true, SortOrder = 4 },
                new ServiceCategory { Id = 5, Name = "Health Services", Description = "Health certificates, vaccination records, medical assistance", IconName = "Heart", IsActive = true, SortOrder = 5 },
                new ServiceCategory { Id = 6, Name = "Education Services", Description = "School enrollment, scholarship applications, educational assistance", IconName = "GraduationCap", IsActive = true, SortOrder = 6 }
            );

            // Seed Emergency Hotlines
            modelBuilder.Entity<EmergencyHotline>().HasData(
                new EmergencyHotline { Id = 1, Title = "Emergency Hotline", PhoneNumber = "911", Description = "For all emergency situations", IsEmergency = true, Department = "Emergency Response", OperatingHours = "24/7", IsActive = true, SortOrder = 1 },
                new EmergencyHotline { Id = 2, Title = "Fire Department", PhoneNumber = "(077) 770-1234", Description = "Fire emergencies and rescue operations", IsEmergency = true, Department = "Fire Department", OperatingHours = "24/7", IsActive = true, SortOrder = 2 },
                new EmergencyHotline { Id = 3, Title = "Police Station", PhoneNumber = "(077) 770-5678", Description = "Crime reporting and police assistance", IsEmergency = true, Department = "Police Department", OperatingHours = "24/7", IsActive = true, SortOrder = 3 },
                new EmergencyHotline { Id = 4, Title = "City Hall Main", PhoneNumber = "(077) 770-9000", Description = "General inquiries and information", IsEmergency = false, Department = "City Hall", OperatingHours = "8:00 AM - 5:00 PM", IsActive = true, SortOrder = 4 }
            );

            // Seed System Configurations
            modelBuilder.Entity<SystemConfiguration>().HasData(
                new SystemConfiguration { Id = Guid.NewGuid().ToString(), Key = "APP_NAME", Value = "Alisto", Description = "Application name", DataType = ConfigDataType.String, IsPublic = true },
                new SystemConfiguration { Id = Guid.NewGuid().ToString(), Key = "APP_VERSION", Value = "1.0.0", Description = "Application version", DataType = ConfigDataType.String, IsPublic = true },
                new SystemConfiguration { Id = Guid.NewGuid().ToString(), Key = "MAINTENANCE_MODE", Value = "false", Description = "Enable maintenance mode", DataType = ConfigDataType.Boolean, IsPublic = false },
                new SystemConfiguration { Id = Guid.NewGuid().ToString(), Key = "MAX_FILE_SIZE_MB", Value = "10", Description = "Maximum file upload size in MB", DataType = ConfigDataType.Number, IsPublic = false }
            );
        }

        // ===================================================================
        // SAVE CHANGES OVERRIDE (FOR AUDIT TRAIL)
        // ===================================================================

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Update timestamps
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is User || e.Entity is NewsArticle || e.Entity is Appointment || e.Entity is IssueReport)
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Property("CreatedAt") != null)
                        entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }

                if (entry.Property("UpdatedAt") != null)
                    entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}