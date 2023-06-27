using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Insert_IncidentDetails_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "IncidentDetails",
                columns: new[] { "Id", "Active", "AssignmentGroup", "BusinessService", "ConfigurationItem", "Description", "ErrorCode", "Impact", "Summary", "Type", "UndefinedCaller", "UndefinedLocation" },
                values: new object[,]
                {
                    { 1, true, "HR Engineering", "Application Platform", "Azure Cloud Platform", "Azure EventHub Error", "AZRCCP_RTW_HR_APP_101001", 3, "Azure resource error. Please follow the PDG AZRCCP_RTW_HR_APP_101001.", 2, "HR Engineering", "Application Platform" },
                    { 2, true, "HR Engineering", "Application Platform", "Azure Cloud Platform", "Azure Database Error", "AZRCCP_RTW_HR_APP_102001", 3, "Azure Database Error. Please follow the PDG AZRCCP_RTW_HR_APP_102001.", 2, "HR Engineering", "Application Platform" },
                    { 3, true, "HR Engineering", "Application Platform", "Azure Cloud Platform", "Azure KeyVault Error", "AZRCCP_RTW_HR_APP_103001", 3, "Azure KeyVault Error. Please follow the PDG AZRCCP_RTW_HR_APP_103001.", 2, "HR Engineering", "Application Platform" },
                    { 4, true, "HR Engineering", "Application Platform", "Azure Cloud Platform", "RTW Outbount API Error", "AZRCCP_RTW_HR_APP_104001", 3, "RTW New starter posting data API Error. Please follow the PDG AZRCCP_RTW_HR_APP_104001.", 1, "HR Engineering", "Application Platform" },
                    { 5, true, "HR Engineering", "Application Platform", "Azure Cloud Platform", "RTW Inbount API Error", "AZRCCP_RTW_HR_APP_105001", 3, "RTW Read data API Error. Please follow the PDG AZRCCP_RTW_HR_APP_105001.", 1, "HR Engineering", "Application Platform" },
                    { 6, true, "HR Engineering", "Application Platform", "Azure Cloud Platform", "RTW Data Error", "AZRCCP_HR_APP_106001", 3, "Update HCM Data Error. Please follow the PDG AZRCCP_HR_APP_106001.", 1, "HR Engineering", "Application Platform" },
                    { 7, true, "HR Engineering", "Application Platform", "Azure Cloud Platform", "Generic Exception", "AZRCCP_RTW_HR_APP_110001", 3, "Generic exception. Please follow the PDG AZRCCP_RTW_HR_APP_110001.", 2, "HR Engineering", "Application Platform" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "IncidentDetails",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
