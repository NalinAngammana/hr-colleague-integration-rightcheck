using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_DocumentTypeValue_PassportBritish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 22,
                column: "HCMDocumentName",
                value: "Passport (British)");

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 23,
                column: "HCMDocumentName",
                value: "Passport (British)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 22,
                column: "HCMDocumentName",
                value: "Passport of British or EEA citizen");

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 23,
                column: "HCMDocumentName",
                value: "Passport of British or EEA citizen");
        }
    }
}
