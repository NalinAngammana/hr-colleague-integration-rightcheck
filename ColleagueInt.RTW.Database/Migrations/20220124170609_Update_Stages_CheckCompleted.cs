using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_Stages_CheckCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsValidCheckExist",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsValidCheckExist",
                value: true);
        }
    }
}
