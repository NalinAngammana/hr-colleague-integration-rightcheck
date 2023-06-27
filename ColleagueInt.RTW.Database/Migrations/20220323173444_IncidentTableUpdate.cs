using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class IncidentTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "RTW Outbound API Error");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "RTW Inbound API Error");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "RTW Outbount API Error");

            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "RTW Inbount API Error");
        }
    }
}
