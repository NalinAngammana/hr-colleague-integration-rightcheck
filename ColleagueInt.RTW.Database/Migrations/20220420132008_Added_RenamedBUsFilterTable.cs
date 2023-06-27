using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Added_RenamedBUsFilterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 7,
                column: "BusinessUnitName",
                value: "Corporate Services");

            migrationBuilder.InsertData(
                table: "FilterData",
                columns: new[] { "Id", "BusinessUnitName", "Description" },
                values: new object[,]
                {
                    { 22, "GM&C", "Include colleagues matching the filter criteria." },
                    { 23, "HR", "Include colleagues matching the filter criteria." },
                    { 24, "CFO Division", "Include colleagues matching the filter criteria." }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.UpdateData(
                table: "FilterData",
                keyColumn: "Id",
                keyValue: 7,
                column: "BusinessUnitName",
                value: "Corporate Affairs");
        }
    }
}
