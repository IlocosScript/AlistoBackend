using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Alisto.Api.Migrations
{
    /// <inheritdoc />
    public partial class change1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSessions");

            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "1f8c44b3-6d74-4981-a460-6c795d773cb0");

            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "3669cded-8fd1-4c2e-96ad-3cd0d8313417");

            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "64f1cee1-c3fc-4afb-aea9-ebadca6a0f5f");

            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "9aff5ab5-5443-4f96-b3ed-257694a27974");

            migrationBuilder.AddColumn<string>(
                name: "AuthProvider",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "IssuePhotos",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "IssuePhotos",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 11, 1, 33, 9, 32, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 7, 11, 1, 33, 9, 32, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 11, 1, 33, 9, 32, DateTimeKind.Utc).AddTicks(6270), new DateTime(2025, 7, 11, 1, 33, 9, 32, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 11, 1, 33, 9, 32, DateTimeKind.Utc).AddTicks(6270), new DateTime(2025, 7, 11, 1, 33, 9, 32, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 11, 1, 33, 9, 32, DateTimeKind.Utc).AddTicks(6270), new DateTime(2025, 7, 11, 1, 33, 9, 32, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.InsertData(
                table: "SystemConfigurations",
                columns: new[] { "Id", "CreatedAt", "DataType", "Description", "IsPublic", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { "540362bb-cfc9-4e49-a9c0-640593156fd1", new DateTime(2025, 7, 11, 1, 33, 9, 33, DateTimeKind.Utc).AddTicks(2830), "Boolean", "Enable maintenance mode", false, "MAINTENANCE_MODE", new DateTime(2025, 7, 11, 1, 33, 9, 33, DateTimeKind.Utc).AddTicks(2830), "false" },
                    { "79f92b46-5a2b-46e0-968f-25b0d501eeb7", new DateTime(2025, 7, 11, 1, 33, 9, 33, DateTimeKind.Utc).AddTicks(2820), "String", "Application version", true, "APP_VERSION", new DateTime(2025, 7, 11, 1, 33, 9, 33, DateTimeKind.Utc).AddTicks(2830), "1.0.0" },
                    { "b410a422-0280-40b6-a52d-e34ad1b0923a", new DateTime(2025, 7, 11, 1, 33, 9, 33, DateTimeKind.Utc).AddTicks(2840), "Number", "Maximum file upload size in MB", false, "MAX_FILE_SIZE_MB", new DateTime(2025, 7, 11, 1, 33, 9, 33, DateTimeKind.Utc).AddTicks(2840), "10" },
                    { "ca1fb49f-130d-4ae2-b810-2275f0f9bf76", new DateTime(2025, 7, 11, 1, 33, 9, 33, DateTimeKind.Utc).AddTicks(2080), "String", "Application name", true, "APP_NAME", new DateTime(2025, 7, 11, 1, 33, 9, 33, DateTimeKind.Utc).AddTicks(2080), "Alisto" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "540362bb-cfc9-4e49-a9c0-640593156fd1");

            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "79f92b46-5a2b-46e0-968f-25b0d501eeb7");

            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "b410a422-0280-40b6-a52d-e34ad1b0923a");

            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "ca1fb49f-130d-4ae2-b810-2275f0f9bf76");

            migrationBuilder.DropColumn(
                name: "AuthProvider",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Caption",
                table: "IssuePhotos");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "IssuePhotos");

            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeviceInfo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpAddress = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    RefreshToken = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
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

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(960), new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(960) });

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 7, 2, 8, 56, 17, 244, DateTimeKind.Utc).AddTicks(2060) });

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
                name: "IX_UserSessions_UserId",
                table: "UserSessions",
                column: "UserId");
        }
    }
}
