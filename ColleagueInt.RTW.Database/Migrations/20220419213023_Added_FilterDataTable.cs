using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Added_FilterDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LookupData");

            migrationBuilder.CreateTable(
                name: "FilterData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessUnitName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterData", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FilterData",
                columns: new[] { "Id", "BusinessUnitName", "Description" },
                values: new object[,]
                {
                    { 1, "Argos Customer Management Centres", "Include colleagues matching the filter criteria." },
                    { 19, "Property", "Include colleagues matching the filter criteria." },
                    { 18, "Non Food", "Include colleagues matching the filter criteria." },
                    { 17, "Nectar 360 Ltd", "Include colleagues matching the filter criteria." },
                    { 16, "Nectar", "Include colleagues matching the filter criteria." },
                    { 15, "Marketing", "Include colleagues matching the filter criteria." },
                    { 14, "Logistics", "Include colleagues matching the filter criteria." },
                    { 13, "Habitat", "Include colleagues matching the filter criteria." },
                    { 12, "Group HR", "Include colleagues matching the filter criteria." },
                    { 20, "Retail", "Include colleagues matching the filter criteria." },
                    { 11, "Food Commercial", "Include colleagues matching the filter criteria." },
                    { 9, "Executives", "Include colleagues matching the filter criteria." },
                    { 8, "Digital", "Include colleagues matching the filter criteria." },
                    { 7, "Corporate Affairs", "Include colleagues matching the filter criteria." },
                    { 6, "Corporate Affairs", "Include colleagues matching the filter criteria." },
                    { 5, "Argos ROI", "Include colleagues matching the filter criteria." },
                    { 4, "Argos Retail", "Include colleagues matching the filter criteria." },
                    { 3, "Argos Local Fulfilment Centres", "Include colleagues matching the filter criteria." },
                    { 2, "Argos Distribution", "Include colleagues matching the filter criteria." },
                    { 10, "Finance & Business Development", "Include colleagues matching the filter criteria." },
                    { 21, "Sainsbury's Tech", "Include colleagues matching the filter criteria." }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterData");

            migrationBuilder.CreateTable(
                name: "LookupData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalValue = table.Column<string>(type: "varchar(200)", nullable: false),
                    LookupType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupData", x => x.Id);
                });
        }
    }
}
