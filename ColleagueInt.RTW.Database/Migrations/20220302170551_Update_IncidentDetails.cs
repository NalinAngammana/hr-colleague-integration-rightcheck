using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_IncidentDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConfigurationItem",
                value: "Cloud Colleague Publisher");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConfigurationItem",
                value: "Cloud Colleague Publisher");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConfigurationItem",
                value: "Cloud Colleague Publisher");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConfigurationItem",
                value: "Cloud Colleague Publisher");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConfigurationItem",
                value: "Cloud Colleague Publisher");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConfigurationItem",
                value: "Cloud Colleague Publisher");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConfigurationItem",
                value: "Cloud Colleague Publisher");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConfigurationItem",
                value: "Azure Cloud Platform");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConfigurationItem",
                value: "Azure Cloud Platform");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConfigurationItem",
                value: "Azure Cloud Platform");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConfigurationItem",
                value: "Azure Cloud Platform");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConfigurationItem",
                value: "Azure Cloud Platform");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConfigurationItem",
                value: "Azure Cloud Platform");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConfigurationItem",
                value: "Azure Cloud Platform");
        }
    }
}
