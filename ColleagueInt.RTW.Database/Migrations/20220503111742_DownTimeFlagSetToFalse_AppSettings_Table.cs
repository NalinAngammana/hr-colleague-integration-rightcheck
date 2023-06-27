using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class DownTimeFlagSetToFalse_AppSettings_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Value",
                value: "FALSE");

            migrationBuilder.UpdateData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 4,
                column: "Value",
                value: "FALSE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Value",
                value: "TRUE");

            migrationBuilder.UpdateData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 4,
                column: "Value",
                value: "TRUE");
        }
    }
}
