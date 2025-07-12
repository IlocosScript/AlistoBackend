using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Alisto.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTouristSpotRequiredFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "TravelTime",
                table: "TouristSpots",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "OpeningHours",
                table: "TouristSpots",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "EntryFee",
                table: "TouristSpots",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Coordinates",
                table: "TouristSpots",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 12, 8, 59, 28, 438, DateTimeKind.Utc).AddTicks(5734), new DateTime(2025, 7, 12, 8, 59, 28, 438, DateTimeKind.Utc).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 12, 8, 59, 28, 438, DateTimeKind.Utc).AddTicks(8153), new DateTime(2025, 7, 12, 8, 59, 28, 438, DateTimeKind.Utc).AddTicks(8156) });

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 12, 8, 59, 28, 438, DateTimeKind.Utc).AddTicks(8162), new DateTime(2025, 7, 12, 8, 59, 28, 438, DateTimeKind.Utc).AddTicks(8163) });

            migrationBuilder.UpdateData(
                table: "EmergencyHotlines",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 12, 8, 59, 28, 438, DateTimeKind.Utc).AddTicks(8167), new DateTime(2025, 7, 12, 8, 59, 28, 438, DateTimeKind.Utc).AddTicks(8167) });

            migrationBuilder.InsertData(
                table: "SystemConfigurations",
                columns: new[] { "Id", "CreatedAt", "DataType", "Description", "IsPublic", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { "0587786f-05b8-4d01-9ea5-dc87330a9bc3", new DateTime(2025, 7, 12, 8, 59, 28, 440, DateTimeKind.Utc).AddTicks(9693), "Boolean", "Enable maintenance mode", false, "MAINTENANCE_MODE", new DateTime(2025, 7, 12, 8, 59, 28, 440, DateTimeKind.Utc).AddTicks(9694), "false" },
                    { "1719bf1a-1ceb-429f-bbba-341ae5a5d080", new DateTime(2025, 7, 12, 8, 59, 28, 440, DateTimeKind.Utc).AddTicks(6934), "String", "Application name", true, "APP_NAME", new DateTime(2025, 7, 12, 8, 59, 28, 440, DateTimeKind.Utc).AddTicks(6940), "Alisto" },
                    { "25a01b24-c556-4296-9a03-8ec3b789b893", new DateTime(2025, 7, 12, 8, 59, 28, 440, DateTimeKind.Utc).AddTicks(9709), "Number", "Maximum file upload size in MB", false, "MAX_FILE_SIZE_MB", new DateTime(2025, 7, 12, 8, 59, 28, 440, DateTimeKind.Utc).AddTicks(9710), "10" },
                    { "ddb6ab85-d70a-467f-813d-51d9dff61955", new DateTime(2025, 7, 12, 8, 59, 28, 440, DateTimeKind.Utc).AddTicks(9667), "String", "Application version", true, "APP_VERSION", new DateTime(2025, 7, 12, 8, 59, 28, 440, DateTimeKind.Utc).AddTicks(9672), "1.0.0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "0587786f-05b8-4d01-9ea5-dc87330a9bc3");

            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "1719bf1a-1ceb-429f-bbba-341ae5a5d080");

            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "25a01b24-c556-4296-9a03-8ec3b789b893");

            migrationBuilder.DeleteData(
                table: "SystemConfigurations",
                keyColumn: "Id",
                keyValue: "ddb6ab85-d70a-467f-813d-51d9dff61955");

            migrationBuilder.AlterColumn<string>(
                name: "TravelTime",
                table: "TouristSpots",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OpeningHours",
                table: "TouristSpots",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EntryFee",
                table: "TouristSpots",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Coordinates",
                table: "TouristSpots",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

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
    }
}
