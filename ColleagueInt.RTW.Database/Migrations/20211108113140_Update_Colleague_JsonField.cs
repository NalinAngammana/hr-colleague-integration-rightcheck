using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_Colleague_JsonField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JsonData",
                table: "Colleagues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 1,
                column: "StageName",
                value: "Initial Stage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonData",
                table: "Colleagues");

            migrationBuilder.UpdateData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 1,
                column: "StageName",
                value: "Inital Stage");
        }
    }
}
