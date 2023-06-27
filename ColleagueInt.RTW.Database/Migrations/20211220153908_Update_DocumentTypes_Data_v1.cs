using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_DocumentTypes_Data_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { 5, "Other Legal Document", "Accession Work Card" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { "Other Legal Document", "Accession Work Card cover" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { 5, "Other Legal Document", "Application Registration Card - back" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { 5, "Other Legal Document", "Application Registration Card - front" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { "Biometric Residence Permit current (not Tier 4)", "Biometric Residence card - back" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { "Biometric Residence Permit current (not Tier 4)", "Biometric Residence card - front" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { 5, "Biometric Residence Permit current (not Tier 4)", "Biometric Residence card - Indefinite Leave - front" });

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[,]
                {
                    { 49, 5, "Other Legal Document", "Yellow Registration Certificate" },
                    { 34, 5, "Passport Document", "Passport Stamp - Indefinite Leave to Enter or Remain" },
                    { 35, 5, "Passport Document", "Passport Stamp - Leave to Enter or Remain" },
                    { 36, 5, "Passport Document", "Passport Stamp - Permanent Residence Card EEA Family Member" },
                    { 37, 5, "Passport Document", "Passport Stamp - Residence Card EEA Family Member" },
                    { 38, 5, "Passport Document", "Passport Stamp - Right of Abode" },
                    { 39, 5, "Passport Document", "Passport Stamp - Tier 4 (student)" },
                    { 40, 5, "Passport Document", "Passport Stamp - Tier 4 (student)" },
                    { 41, 5, "Passport Document", "Passport Stamp - Work Permit" },
                    { 42, 5, "EU Pre-Settled Status", "Pre-settled Status Document" },
                    { 43, 5, "EU Settled Status", "Settled Status Document" },
                    { 33, 1, "Passport of British or EEA citizen", "Passport cover" },
                    { 45, 4, "Other Legal Document", "Share Code Evidence" },
                    { 46, 5, "Other Legal Document", "Student Term Time Evidence" },
                    { 47, 5, "Work", "Work Permit letter - back" },
                    { 48, 5, "Work", "Work Permit letter - front" },
                    { 50, 5, "Other Legal Document", "Yellow Registration Certificate cover" },
                    { 44, 4, "Other Legal Document", "Share Code Evidence" },
                    { 32, 1, "Passport of British or EEA citizen", "Passport Additional Page" },
                    { 30, 5, "Other Legal Document", "Other" },
                    { 9, 5, "Biometric Residence Permit current (Tier 4) plus study & term time evidence", "Biometric Residence Card - Tier 4 (student) - front" },
                    { 10, 2, "Birth certificate (British citizen)", "Birth or Adoption Certificate (British)" },
                    { 11, 2, "Birth certificate", "Birth, Adoption or Naturalisation Certificate" },
                    { 12, 5, "Other Legal Document", "Certificate of Application Family Member - back" },
                    { 13, 5, "Other Legal Document", "Certificate of Application Family Member - front" },
                    { 14, 5, "Other Legal Document", "ECS Positive Verification Notice" },
                    { 15, 5, "Other Legal Document", "ECS Positive Verification Notice - Asylum" },
                    { 16, 5, "Other Legal Document", "ECS Positive Verification Notice - Family" },
                    { 17, 5, "Other Legal Document", "EEA & Swiss Residents Permit or Registration Certificate" },
                    { 18, 5, "Other Legal Document", "EEA & Swiss Residents Permit or Registration Certificate cover" },
                    { 19, 5, "Other Legal Document", "Frontier Worker Permit" },
                    { 20, 5, "Immigration Status Document plus NI No", "Immigration Status Document - Indefinite Leave to Enter or Remain" },
                    { 21, 5, "Immigration Status Document plus NI No", "Immigration Status Document - Leave to Enter or Remain" },
                    { 22, 5, "Immigration Status Document plus NI No", "Immigration Status Document - Permanent Residence Card EEA Family Member" },
                    { 23, 5, "Immigration Status Document plus NI No", "Immigration Status Document - Residence Card EEA Family Member" }
                });

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[,]
                {
                    { 24, 5, "Immigration Status Document plus NI No", "Immigration Status Document - Work Permit" },
                    { 25, 2, "Marriage Certificate", "Marriage certificate, divorce decree absolute, a deed poll or statutory declaration" },
                    { 26, 5, "ID Card", "National Identity card - back" },
                    { 27, 5, "ID Card", "National Identity card - front" },
                    { 28, 5, "ID Card", "National Insurance Document" },
                    { 29, 5, "Other Legal Document", "Naturalisation Certificate" },
                    { 31, 1, "Passport of British or EEA citizen", "Passport" },
                    { 8, 5, "Biometric Residence Permit current (not Tier 4)", "Biometric Residence card - Permanent Residence (EEA) - front" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 50);

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
                columns: new[] { "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { "Frontier Worker Permit", "Frontier Worker Permit" });

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
                columns: new[] { "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { 1, "Passport", "Passport" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { "National Identity card - back", "National Identity card - back" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { "National Identity card - front", "National Identity card - front" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DocumentSection", "HCMDocumentName", "RTWDocumentName" },
                values: new object[] { 2, "National Insurance Document", "National Insurance Document" });
        }
    }
}
