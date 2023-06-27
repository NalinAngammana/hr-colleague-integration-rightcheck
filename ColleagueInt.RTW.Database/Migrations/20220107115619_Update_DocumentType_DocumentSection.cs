using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_DocumentType_DocumentSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 10,
                column: "RTWDocumentName",
                value: "Birth or Adoption Certificate");

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 12,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 13,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 14,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 15,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 16,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 17,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 18,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 19,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 20,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 21,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 22,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 23,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 24,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 26,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 27,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 28,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 29,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 46,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 49,
                column: "DocumentSection",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 50,
                column: "DocumentSection",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 10,
                column: "RTWDocumentName",
                value: "Birth or Adoption Certificate (British)");

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 12,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 13,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 14,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 15,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 16,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 17,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 18,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 19,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 20,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 21,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 22,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 23,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 24,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 26,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 27,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 28,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 29,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 46,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 49,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 50,
                column: "DocumentSection",
                value: 5);

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[,]
                {
                    { 42, 5, "EU Pre-Settled Status", "Pre-settled Status Document" },
                    { 30, 5, "Other Legal Document", "Other" }
                });
        }
    }
}
