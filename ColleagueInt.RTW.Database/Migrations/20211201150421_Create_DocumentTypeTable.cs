using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Create_DocumentTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RTWDocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HCMDocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "HCMDocumentName", "RTWDocumentName" },
                values: new object[,]
                {
                    { 1, "Accession Work Card", "Accession Work Card" },
                    { 2, "Accession Work Card cover", "Accession Work Card cover" },
                    { 3, "Application Registration Card - back", "Application Registration Card - back" },
                    { 4, "Passport", "Passport" },
                    { 5, "National Insurance Document", "National Insurance Document" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentTypes");
        }
    }
}
