using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_Colleague_ErrorLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErrorLog",
                table: "Colleagues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "StageName" },
                values: new object[] { 5, "Check Request Failed" });

            migrationBuilder.CreateIndex(
                name: "IX_Colleagues_StageId",
                table: "Colleagues",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colleagues_Stages_StageId",
                table: "Colleagues",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colleagues_Stages_StageId",
                table: "Colleagues");

            migrationBuilder.DropIndex(
                name: "IX_Colleagues_StageId",
                table: "Colleagues");

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "ErrorLog",
                table: "Colleagues");
        }
    }
}
