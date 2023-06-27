using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_DocumentTypeTable_DocumentSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentSection",
                table: "DocumentTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { 2, "Birth or Adoption Certificate", "Birth or Adoption Certificate" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { 5, "Frontier Worker Permit", "Frontier Worker Permit" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { 4, "Share Code Evidence", "Share Code Evidence" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "DocumentSection",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { 5, "National Identity card - back", "National Identity card - back" });

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[,]
                {
                    { 6, 5, "National Identity card - front", "National Identity card - front" },
                    { 7, 2, "National Insurance Document", "National Insurance Document" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "DocumentSection",
                table: "DocumentTypes");

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { "Accession Work Card", "Accession Work Card" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { "Accession Work Card cover", "Accession Work Card cover" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { "Application Registration Card - back", "Application Registration Card - back" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { "National Insurance Document", "National Insurance Document" });
        }
    }
}
