using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_AppSettings_Table_VisaReadDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "Id", "Description", "Key", "Type", "Value" },
                values: new object[] { 7, "Set a specific time to read visa approved colleagues from HCM or set it to blank to stop the service", "Refresh-HCM-Visa-Approved-Colleagues-Daily-Time", "string", "20" });

            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "Id", "Description", "Key", "Type", "Value" },
                values: new object[] { 8, "Last visa approved colleagues details read from HCM at", "Last-Time-HCM-Visa-Approved-Colleagues-Refreshed-At", "dateTime", "2022-05-24 21:30:00" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
