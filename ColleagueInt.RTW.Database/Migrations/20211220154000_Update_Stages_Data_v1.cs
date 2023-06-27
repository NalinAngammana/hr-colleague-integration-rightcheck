using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_Stages_Data_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "IsValidCheckExist", "StageName" },
                values: new object[] { 8, true, "HCM Documents Uploaded" });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "IsValidCheckExist", "StageName" },
                values: new object[] { 9, true, "HCM Documents Upload Failed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
