using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_DocumentTypes_Data_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 7);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "HCMCountryCode", "HCMCountryName", "RTWCountryCode", "RTWCountryName" },
                values: new object[,]
                {
                    { 229, "GB", "British", "GBN", "British National (Overseas)" },
                    { 230, "GB", "British", "GBO", "British Overseas Citizen" },
                    { 231, "GB", "British", "GBD", "British Overseas Territories Citizen" },
                    { 232, "GB", "British", "GBP", "British Protected Person" },
                    { 233, "GB", "British", "GBS", "British Subject" }
                });

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "DocumentSection", "HCMDocumentName", "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription", "RTWDocumentName" },
                values: new object[,]
                {
                    { 26, 2, "ID Card", null, null, "National Identity card - back" },
                    { 27, 2, "ID Card", null, null, "National Identity card - front" },
                    { 28, 2, "ID Card", null, null, "National Insurance Document" },
                    { 29, 2, "Other Legal Document", null, null, "Naturalisation Certificate" },
                    { 31, 1, "Passport of British or EEA citizen", null, null, "Passport" },
                    { 32, 2, "Passport of British or EEA citizen", null, null, "Passport Additional Page" },
                    { 33, 2, "Passport of British or EEA citizen", null, null, "Passport cover" },
                    { 34, 5, "Passport Document", "MHR_IMMIGRATION_STATUS_DOC_ILR", "Immigration Status Document - Indefinite Leave to Remain + NI Number", "Passport Stamp - Indefinite Leave to Enter or Remain" },
                    { 35, 5, "Passport Document", "MHR_IMMIGRATION_STATUS_DOC_ILR", "Immigration Status Document - Indefinite Leave to Remain + NI Number", "Passport Stamp - Leave to Enter or Remain" },
                    { 36, 5, "Passport Document", "MHR_IMMIGRATION_STATUS_DOC_ILR", "Immigration Status Document - Indefinite Leave to Remain + NI Number", "Passport Stamp - Permanent Residence Card EEA Family Member" },
                    { 39, 5, "Passport Document", "MHR_STUDENT_VISA", "Student Visa", "Passport Stamp - Tier 4 (student)" },
                    { 38, 5, "Passport Document", "MHR_RIGHT_TO_ABODE", "Right to Abode (Current)", "Passport Stamp - Right of Abode" },
                    { 25, 2, "Marriage Certificate", null, null, "Marriage certificate, divorce decree absolute, a deed poll or statutory declaration" },
                    { 40, 5, "Passport Document", "MHR_STUDENT_VISA", "Student Visa", "Passport Stamp - Tier 4 (student)" },
                    { 41, 5, "Passport Document", "MHR_TIER_1_GENERAL_VISA", "Tier 1 General Visa", "Passport Stamp - Work Permit" },
                    { 43, 5, "EU Settled Status", "MHR_CURR_IMMIGRATION_NI", "Current Immigration Status Document + NI Number", "Settled Status Document" },
                    { 44, 4, "Other Legal Document", null, null, "Share Code Evidence" },
                    { 45, 4, "Other Legal Document", null, null, "Share Code Evidence" },
                    { 46, 2, "Other Legal Document", null, null, "Student Term Time Evidence" },
                    { 47, 5, "Work", "MHR_CURR_IMMIGRATION_NI", "Current Immigration Status Document + NI Number", "Work Permit letter - back" },
                    { 48, 5, "Work", "MHR_CURR_IMMIGRATION_NI", "Current Immigration Status Document + NI Number", "Work Permit letter - front" },
                    { 37, 5, "Passport Document", "MHR_IMMIGRATION_STATUS_DOC_ILR", "Immigration Status Document - Indefinite Leave to Remain + NI Number", "Passport Stamp - Residence Card EEA Family Member" },
                    { 24, 2, "Immigration Status Document plus NI No", null, null, "Immigration Status Document - Work Permit" },
                    { 22, 2, "Immigration Status Document plus NI No", null, null, "Immigration Status Document - Permanent Residence Card EEA Family Member" },
                    { 49, 2, "Other Legal Document", null, null, "Yellow Registration Certificate" },
                    { 1, 2, "Other Legal Document", null, null, "Accession Work Card" },
                    { 2, 2, "Other Legal Document", null, null, "Accession Work Card cover" },
                    { 3, 2, "Other Legal Document", null, null, "Application Registration Card - back" },
                    { 4, 2, "Other Legal Document", null, null, "Application Registration Card - front" },
                    { 5, 5, "Biometric Residence Permit current (not Tier 4)", "MHR_BRP_ILR", "Biometric Residence Permit (Current) - Indefinite Leave to Remain", "Biometric Residence card - back" },
                    { 6, 5, "Biometric Residence Permit current (not Tier 4)", "MHR_BRP_ILR", "Biometric Residence Permit (Current) - Indefinite Leave to Remain", "Biometric Residence card - front" },
                    { 7, 5, "Biometric Residence Permit current (not Tier 4)", "MHR_BRP_ILR", "Biometric Residence Permit (Current) - Indefinite Leave to Remain", "Biometric Residence card - Indefinite Leave - front" },
                    { 8, 5, "Biometric Residence Permit current (not Tier 4)", "MHR_BRP_ILR", "Biometric Residence Permit (Current) - Indefinite Leave to Remain", "Biometric Residence card - Permanent Residence (EEA) - front" },
                    { 9, 5, "Biometric Residence Permit current (Tier 4) plus study & term time evidence", "MHR_BRP_TIME_LTD_LTR", "Biometric Residence Permit (Current) – Time Limited Leave to Remain", "Biometric Residence Card - Tier 4 (student) - front" },
                    { 10, 2, "Birth certificate (British citizen)", null, null, "Birth or Adoption Certificate" },
                    { 11, 2, "Birth certificate", null, null, "Birth, Adoption or Naturalisation Certificate" },
                    { 12, 2, "Other Legal Document", null, null, "Certificate of Application Family Member - back" }
                });

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "DocumentSection", "HCMDocumentName", "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription", "RTWDocumentName" },
                values: new object[,]
                {
                    { 13, 2, "Other Legal Document", null, null, "Certificate of Application Family Member - front" },
                    { 14, 2, "Other Legal Document", null, null, "ECS Positive Verification Notice" },
                    { 15, 2, "Other Legal Document", null, null, "ECS Positive Verification Notice - Asylum" },
                    { 16, 2, "Other Legal Document", null, null, "ECS Positive Verification Notice - Family" },
                    { 17, 2, "Other Legal Document", null, null, "EEA & Swiss Residents Permit or Registration Certificate" },
                    { 18, 2, "Other Legal Document", null, null, "EEA & Swiss Residents Permit or Registration Certificate cover" },
                    { 19, 2, "Other Legal Document", null, null, "Frontier Worker Permit" },
                    { 20, 2, "Immigration Status Document plus NI No", null, null, "Immigration Status Document - Indefinite Leave to Enter or Remain" },
                    { 21, 2, "Immigration Status Document plus NI No", null, null, "Immigration Status Document - Leave to Enter or Remain" },
                    { 23, 2, "Immigration Status Document plus NI No", null, null, "Immigration Status Document - Residence Card EEA Family Member" },
                    { 50, 2, "Other Legal Document", null, null, "Yellow Registration Certificate cover" }
                });
        }
    }
}
