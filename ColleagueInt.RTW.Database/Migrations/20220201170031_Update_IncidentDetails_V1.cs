using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_IncidentDetails_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ErrorCode", "Summary" },
                values: new object[] { "AZRCCP_RTW_HR_APP_106001", "Update HCM Data Error. Please follow the PDG AZRCCP_RTW_HR_APP_106001." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ErrorCode", "Summary" },
                values: new object[] { "AZRCCP_HR_APP_106001", "Update HCM Data Error. Please follow the PDG AZRCCP_HR_APP_106001." });
        }
    }
}
