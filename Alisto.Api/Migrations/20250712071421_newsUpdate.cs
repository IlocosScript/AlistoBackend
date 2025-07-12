using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Alisto.Api.Migrations
{
    /// <inheritdoc />
    public partial class newsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "NewsArticles",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "PublishedTime",
                table: "NewsArticles",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "NewsArticles",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "NewsArticles",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 12, 7, 14, 20, 874, DateTimeKind.Utc).AddTicks(9487), new DateTime(2025, 7, 12, 7, 14, 20, 874, DateTimeKind.Utc).AddTicks(9493) });

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 12, 7, 14, 20, 875, DateTimeKind.Utc).AddTicks(886), new DateTime(2025, 7, 12, 7, 14, 20, 875, DateTimeKind.Utc).AddTicks(886) });

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 12, 7, 14, 20, 875, DateTimeKind.Utc).AddTicks(890), new DateTime(2025, 7, 12, 7, 14, 20, 875, DateTimeKind.Utc).AddTicks(891) });

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 12, 7, 14, 20, 875, DateTimeKind.Utc).AddTicks(893), new DateTime(2025, 7, 12, 7, 14, 20, 875, DateTimeKind.Utc).AddTicks(894) });

            migrationBuilder.InsertData(
                table: "SystemConfigurations",
                columns: new[] { "Id", "CreatedAt", "DataType", "Description", "IsPublic", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { "809cd448-4cde-4466-b007-91b7d7e06365", new DateTime(2025, 7, 12, 7, 14, 20, 876, DateTimeKind.Utc).AddTicks(4177), "Boolean", "Enable maintenance mode", false, "MAINTENANCE_MODE", new DateTime(2025, 7, 12, 7, 14, 20, 876, DateTimeKind.Utc).AddTicks(4177), "false" },
                    { "8404dd69-28a9-48b1-8b24-fe1b394c7bed", new DateTime(2025, 7, 12, 7, 14, 20, 876, DateTimeKind.Utc).AddTicks(4211), "Number", "Maximum file upload size in MB", false, "MAX_FILE_SIZE_MB", new DateTime(2025, 7, 12, 7, 14, 20, 876, DateTimeKind.Utc).AddTicks(4212), "10" },
                    { "db726ab0-2c0d-4750-94c6-2850031d5cec", new DateTime(2025, 7, 12, 7, 14, 20, 876, DateTimeKind.Utc).AddTicks(2884), "String", "Application name", true, "APP_NAME", new DateTime(2025, 7, 12, 7, 14, 20, 876, DateTimeKind.Utc).AddTicks(2887), "Alisto" },
                    { "e4446bdc-98d3-4a1f-9e45-99785756ce90", new DateTime(2025, 7, 12, 7, 14, 20, 876, DateTimeKind.Utc).AddTicks(4159), "String", "Application version", true, "APP_VERSION", new DateTime(2025, 7, 12, 7, 14, 20, 876, DateTimeKind.Utc).AddTicks(4159), "1.0.0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "809cd448-4cde-4466-b007-91b7d7e06365");

            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "8404dd69-28a9-48b1-8b24-fe1b394c7bed");

            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "db726ab0-2c0d-4750-94c6-2850031d5cec");

            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "e4446bdc-98d3-4a1f-9e45-99785756ce90");

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "NewsArticles",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PublishedTime",
                table: "NewsArticles",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "NewsArticles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "NewsArticles",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

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
    }
}
