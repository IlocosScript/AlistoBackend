using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Alisto.Api.Migrations
{
    /// <inheritdoc />
    public partial class initals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TargetAudience = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsageStatistics",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalUsers = table.Column<int>(type: "integer", nullable: false),
                    ActiveUsers = table.Column<int>(type: "integer", nullable: false),
                    NewRegistrations = table.Column<int>(type: "integer", nullable: false),
                    AppointmentsBooked = table.Column<int>(type: "integer", nullable: false),
                    IssuesReported = table.Column<int>(type: "integer", nullable: false),
                    NewsViews = table.Column<int>(type: "integer", nullable: false),
                    MostUsedService = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PeakUsageHour = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsageStatistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Action = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntityType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OldValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    UserAgent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyHotlines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsEmergency = table.Column<bool>(type: "boolean", nullable: false),
                    Department = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OperatingHours = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyHotlines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileUploads",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    OriginalFileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    MimeType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UploadedBy = table.Column<string>(type: "text", nullable: true),
                    EntityType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EntityId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsTemporary = table.Column<bool>(type: "boolean", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUploads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsArticles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Summary = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FullContent = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PublishedTime = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ExpectedAttendees = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Author = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Tags = table.Column<string>(type: "json", nullable: false),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: false),
                    IsTrending = table.Column<bool>(type: "boolean", nullable: false),
                    ViewCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsArticles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Cost = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    Contractor = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Progress = table.Column<int>(type: "integer", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpectedEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActualEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProjectType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FundingSource = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IconName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceStatistics",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    ServiceName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TotalAppointments = table.Column<int>(type: "integer", nullable: false),
                    CompletedAppointments = table.Column<int>(type: "integer", nullable: false),
                    CancelledAppointments = table.Column<int>(type: "integer", nullable: false),
                    AverageProcessingTime = table.Column<int>(type: "integer", nullable: false),
                    CustomerSatisfactionRating = table.Column<decimal>(type: "numeric(3,2)", nullable: true),
                    Period = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceStatistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfigurations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DataType = table.Column<string>(type: "text", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TouristSpots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Rating = table.Column<decimal>(type: "numeric(3,2)", nullable: false),
                    Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Coordinates = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OpeningHours = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntryFee = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Highlights = table.Column<string>(type: "json", nullable: false),
                    TravelTime = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ViewCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristSpots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProfileImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    EmergencyContactName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EmergencyContactNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CityServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Fee = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    ProcessingTime = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RequiredDocuments = table.Column<string>(type: "json", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    OfficeLocation = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ContactNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    OperatingHours = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityServices_ServiceCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ServiceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppFeedbacks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Subject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: true),
                    ContactEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Response = table.Column<string>(type: "text", nullable: true),
                    RespondedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    RespondedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFeedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "IssueReports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    UrgencyLevel = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Coordinates = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ContactInfo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<string>(type: "text", nullable: false),
                    AssignedDepartment = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AssignedTo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EstimatedResolution = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActualResolution = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ResolutionNotes = table.Column<string>(type: "text", nullable: true),
                    PubliclyVisible = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueReports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ActionUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ActionData = table.Column<string>(type: "text", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    RefreshToken = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeviceInfo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IpAddress = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AppointmentTime = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    TotalFee = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    ApplicantFirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ApplicantLastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ApplicantMiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ApplicantContactNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ApplicantEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ApplicantAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ServiceSpecificData = table.Column<string>(type: "json", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CancellationReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_CityServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "CityServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IssuePhotos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    IssueReportId = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    MimeType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssuePhotos_IssueReports_IssueReportId",
                        column: x => x.IssueReportId,
                        principalTable: "IssueReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueUpdates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    IssueReportId = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UpdateType = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueUpdates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueUpdates_IssueReports_IssueReportId",
                        column: x => x.IssueReportId,
                        principalTable: "IssueReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessPermitRequests",
                columns: table => new
                {
                    AppointmentId = table.Column<string>(type: "text", nullable: false),
                    ServiceType = table.Column<string>(type: "text", nullable: false),
                    BusinessName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    BusinessType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BusinessAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OwnerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TinNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CapitalInvestment = table.Column<decimal>(type: "numeric(15,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessPermitRequests", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_BusinessPermitRequests_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CivilRegistryRequests",
                columns: table => new
                {
                    AppointmentId = table.Column<string>(type: "text", nullable: false),
                    DocumentType = table.Column<string>(type: "text", nullable: false),
                    Purpose = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NumberOfCopies = table.Column<int>(type: "integer", nullable: false),
                    RegistryNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RegistryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RegistryPlace = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CivilRegistryRequests", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_CivilRegistryRequests_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationServiceRequests",
                columns: table => new
                {
                    AppointmentId = table.Column<string>(type: "text", nullable: false),
                    ServiceType = table.Column<string>(type: "text", nullable: false),
                    StudentFirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StudentLastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StudentMiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    StudentBirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StudentAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    GradeLevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SchoolName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Gpa = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    ScholarshipType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    VocationalCourse = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ParentName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ParentOccupation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MonthlyIncome = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    FamilySize = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationServiceRequests", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_EducationServiceRequests_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthServiceRequests",
                columns: table => new
                {
                    AppointmentId = table.Column<string>(type: "text", nullable: false),
                    ServiceType = table.Column<string>(type: "text", nullable: false),
                    CertificatePurpose = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AssistanceType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MedicalHistory = table.Column<string>(type: "text", nullable: true),
                    CurrentMedications = table.Column<string>(type: "text", nullable: true),
                    Allergies = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Symptoms = table.Column<string>(type: "text", nullable: true),
                    PreferredDoctor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthServiceRequests", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_HealthServiceRequests_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceFeedbacks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AppointmentId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    IsAnonymous = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceFeedbacks_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceFeedbacks_CityServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "CityServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceFeedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialServiceRequests",
                columns: table => new
                {
                    AppointmentId = table.Column<string>(type: "text", nullable: false),
                    ServiceType = table.Column<string>(type: "text", nullable: false),
                    DisabilityType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AssistanceType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MonthlyIncome = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    FamilySize = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialServiceRequests", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_SocialServiceRequests_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxServiceRequests",
                columns: table => new
                {
                    AppointmentId = table.Column<string>(type: "text", nullable: false),
                    ServiceType = table.Column<string>(type: "text", nullable: false),
                    TaxYear = table.Column<int>(type: "integer", nullable: false),
                    PropertyType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PropertyAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PropertyTDN = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    BusinessName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    BusinessTIN = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    AssessedValue = table.Column<decimal>(type: "numeric(15,2)", nullable: true),
                    ExemptionType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxServiceRequests", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_TaxServiceRequests_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmergencyHotlines",
                columns: new[] { "Id", "CreatedAt", "Department", "Description", "IsActive", "IsEmergency", "OperatingHours", "PhoneNumber", "SortOrder", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(960), "Emergency Response", "For all emergency situations", true, true, "24/7", "911", 1, "Emergency Hotline", new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(960) },
                    { 2, new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(2060), "Fire Department", "Fire emergencies and rescue operations", true, true, "24/7", "(077) 770-1234", 2, "Fire Department", new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(2060) },
                    { 3, new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(2060), "Police Department", "Crime reporting and police assistance", true, true, "24/7", "(077) 770-5678", 3, "Police Station", new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(2060) },
                    { 4, new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(2060), "City Hall", "General inquiries and information", true, false, "8:00 AM - 5:00 PM", "(077) 770-9000", 4, "City Hall Main", new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(2060) }
                });

            migrationBuilder.InsertData(
                table: "ServiceCategories",
                columns: new[] { "Id", "Description", "IconName", "IsActive", "Name", "SortOrder" },
                values: new object[,]
                {
                    { 1, "Birth certificates, marriage certificates, death certificates", "FileText", true, "Civil Registry", 1 },
                    { 2, "New business registration, permit renewal, business closure", "Building", true, "Business Permits", 2 },
                    { 3, "Senior citizen ID, PWD ID, indigency certificates", "Users", true, "Social Services", 3 },
                    { 4, "Real property tax, business tax, tax clearance", "CreditCard", true, "Tax Services", 4 },
                    { 5, "Health certificates, vaccination records, medical assistance", "Heart", true, "Health Services", 5 },
                    { 6, "School enrollment, scholarship applications, educational assistance", "GraduationCap", true, "Education Services", 6 }
                });

            migrationBuilder.InsertData(
                table: "SystemConfigurations",
                columns: new[] { "Id", "CreatedAt", "DataType", "Description", "IsPublic", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { "1f8c44b3-6d74-4981-a460-6c795d773cb0", new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(8000), "String", "Application name", true, "APP_NAME", new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(8000), "Alisto" },
                    { "3669cded-8fd1-4c2e-96ad-3cd0d8313417", new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(8800), "Number", "Maximum file upload size in MB", false, "MAX_FILE_SIZE_MB", new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(8800), "10" },
                    { "64f1cee1-c3fc-4afb-aea9-ebadca6a0f5f", new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(8790), "Boolean", "Enable maintenance mode", false, "MAINTENANCE_MODE", new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(8790), "false" },
                    { "9aff5ab5-5443-4f96-b3ed-257694a27974", new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(8780), "String", "Application version", true, "APP_VERSION", new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(8780), "1.0.0" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppFeedbacks_UserId",
                table: "AppFeedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentDate",
                table: "Appointments",
                column: "AppointmentDate");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ReferenceNumber",
                table: "Appointments",
                column: "ReferenceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Status",
                table: "Appointments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_UserId_Status",
                table: "Appointments",
                columns: new[] { "UserId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_CityServices_CategoryId",
                table: "CityServices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IssuePhotos_IssueReportId",
                table: "IssuePhotos",
                column: "IssueReportId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReports_Category",
                table: "IssueReports",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReports_ReferenceNumber",
                table: "IssueReports",
                column: "ReferenceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssueReports_Status",
                table: "IssueReports",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReports_UrgencyLevel",
                table: "IssueReports",
                column: "UrgencyLevel");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReports_UserId",
                table: "IssueReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueUpdates_IssueReportId",
                table: "IssueUpdates",
                column: "IssueReportId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticles_Category",
                table: "NewsArticles",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticles_IsFeatured_IsTrending",
                table: "NewsArticles",
                columns: new[] { "IsFeatured", "IsTrending" });

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticles_PublishedDate",
                table: "NewsArticles",
                column: "PublishedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatedAt",
                table: "Notifications",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_IsRead",
                table: "Notifications",
                columns: new[] { "UserId", "IsRead" });

            migrationBuilder.CreateIndex(
                name: "IX_PublicProjects_IsPublic",
                table: "PublicProjects",
                column: "IsPublic");

            migrationBuilder.CreateIndex(
                name: "IX_PublicProjects_Status",
                table: "PublicProjects",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFeedbacks_AppointmentId",
                table: "ServiceFeedbacks",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFeedbacks_ServiceId",
                table: "ServiceFeedbacks",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFeedbacks_UserId",
                table: "ServiceFeedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemConfigurations_Key",
                table: "SystemConfigurations",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TouristSpots_IsActive",
                table: "TouristSpots",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_TouristSpots_Rating",
                table: "TouristSpots",
                column: "Rating");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_UserId",
                table: "UserSessions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "AppFeedbacks");

            migrationBuilder.DropTable(
                name: "AppUsageStatistics");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BusinessPermitRequests");

            migrationBuilder.DropTable(
                name: "CivilRegistryRequests");

            migrationBuilder.DropTable(
                name: "EducationServiceRequests");

            migrationBuilder.DropTable(
                name: "EmergencyHotlines");

            migrationBuilder.DropTable(
                name: "FileUploads");

            migrationBuilder.DropTable(
                name: "HealthServiceRequests");

            migrationBuilder.DropTable(
                name: "IssuePhotos");

            migrationBuilder.DropTable(
                name: "IssueUpdates");

            migrationBuilder.DropTable(
                name: "NewsArticles");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PublicProjects");

            migrationBuilder.DropTable(
                name: "ServiceFeedbacks");

            migrationBuilder.DropTable(
                name: "ServiceStatistics");

            migrationBuilder.DropTable(
                name: "SocialServiceRequests");

            migrationBuilder.DropTable(
                name: "SystemConfigurations");

            migrationBuilder.DropTable(
                name: "TaxServiceRequests");

            migrationBuilder.DropTable(
                name: "TouristSpots");

            migrationBuilder.DropTable(
                name: "UserSessions");

            migrationBuilder.DropTable(
                name: "IssueReports");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "CityServices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ServiceCategories");
        }
    }
}
