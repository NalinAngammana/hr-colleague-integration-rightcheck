using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_AppSettings_Table_VisaReadDetails_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Key", "Value" },
                values: new object[] { "Set a specific time in hours to read visa approved colleagues from HCM or set it to blank to stop the service", "Refresh-HCM-Visa-Approved-Colleagues-Daily-Time-Hours", "22" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Key", "Value" },
                values: new object[] { "Set a specific time to read visa approved colleagues from HCM or set it to blank to stop the service", "Refresh-HCM-Visa-Approved-Colleagues-Daily-Time", "20" });
        }
    }
}
