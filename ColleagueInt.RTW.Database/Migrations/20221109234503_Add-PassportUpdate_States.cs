using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class AddPassportUpdate_States : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "Id", "Description", "Key", "Type", "Value" },
                values: new object[,]
                {
                    { 9, "Update NFC chip passport images active TRUE/FALSE", "RTW-To-HCM-InboundPastportUpdate", "bool", "TRUE" },
                    { 10, "Passport image update cut off date and time", "RTW-To-HCM-InboundPastportUpdateCutOffTime", "dateTime", "2022-10-19 11:15:00" }
                });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "StageName" },
                values: new object[,]
                {
                    { 10, "Passport Patch Failed" },
                    { 11, "Passport Patch Ignored" },
                    { 12, "Passport Patch Completed" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
