using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Add_SainsburysBank_BU_FilterData_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FilterData",
                columns: new[] { "Id", "BusinessUnitName", "Description" },
                values: new object[] { 24, "Sainsbury's Bank", "Include colleagues matching the filter criteria." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 24);
        }
    }
}
