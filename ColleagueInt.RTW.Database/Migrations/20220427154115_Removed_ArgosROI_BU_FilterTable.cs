using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Removed_ArgosROI_BU_FilterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.UpdateData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 5,
                column: "BusinessUnitName",
                value: "Retail");

            migrationBuilder.UpdateData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 20,
                column: "BusinessUnitName",
                value: "Sainsbury's Tech");

            migrationBuilder.UpdateData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 21,
                column: "BusinessUnitName",
                value: "GM&C");

            migrationBuilder.UpdateData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 22,
                column: "BusinessUnitName",
                value: "HR");

            migrationBuilder.UpdateData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 23,
                column: "BusinessUnitName",
                value: "CFO Division");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 5,
                column: "BusinessUnitName",
                value: "Argos ROI");

            migrationBuilder.UpdateData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 20,
                column: "BusinessUnitName",
                value: "Retail");

            migrationBuilder.UpdateData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 21,
                column: "BusinessUnitName",
                value: "Sainsbury's Tech");

            migrationBuilder.UpdateData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 22,
                column: "BusinessUnitName",
                value: "GM&C");

            migrationBuilder.UpdateData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 23,
                column: "BusinessUnitName",
                value: "HR");

            migrationBuilder.InsertData(
                table: "FilterData",
                columns: new[] { "Id", "BusinessUnitName", "Description" },
                values: new object[] { 24, "CFO Division", "Include colleagues matching the filter criteria." });
        }
    }
}
