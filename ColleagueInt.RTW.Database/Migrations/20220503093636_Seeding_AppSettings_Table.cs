using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Seeding_AppSettings_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataType",
                table: "AppSettings",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppSettings",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "Id", "Description", "Key", "Type", "Value" },
                values: new object[,]
                {
                    { 1, "Flag to determine whether there is a downtime for outbound service to RTW", "HCM-To-RTW-OutboundFlow", "bool", "TRUE" },
                    { 2, "Outbound service downtime starts from this timestamp if the downtime flag is true", "HCM-To-RTW-OutboundFlow-DownTime-Start", "dateTime", "2022-05-02 21:30:00" },
                    { 3, "Outbound service downtime ends from this timestamp if the downtime flag is true", "HCM-To-RTW-OutboundFlow-DownTime-End", "dateTime", "2022-05-02 21:35:00" },
                    { 4, "Flag to determine whether there is a downtime for inbound service to HCM", "RTW-To-HCM-InboundFlow", "bool", "TRUE" },
                    { 5, "Inbound service downtime starts from this timestamp if the downtime flag is true", "RTW-To-HCM-InboundFlow-DownTime-Start", "dateTime", "2022-05-02 22:00:00" },
                    { 6, "Inbound service downtime ends from this timestamp if the downtime flag is true", "RTW-To-HCM-InboundFlow-DownTime-End", "dateTime", "2022-05-02 22:10:00" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppSettings");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "AppSettings",
                newName: "DataType");
        }
    }
}
