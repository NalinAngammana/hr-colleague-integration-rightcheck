using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_Stages_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "StageName" },
                values: new object[] { 6, "Read Data Failed" });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "StageName" },
                values: new object[] { 7, "HCM Update Failed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
