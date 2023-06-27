using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_DocumentTypes_Data_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "DocumentSection", "HCMDocumentName", "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription", "RTWDocumentName" },
                values: new object[,]
                {
                    { 1, 2, "Home Office application/referral evidence", null, null, "Application Registration Card - back" },
                    { 28, 5, "Visa current within valid passport (not Tier 4)", "MHR_TIER_1_GENERAL_VISA", "Tier 1 General Visa", "Passport Stamp - Work Permit" },
                    { 27, 5, "Visa current within valid passport (Tier 4) plus study & term time evidence", "MHR_STUDENT_VISA", "Student Visa", "Passport Stamp - Tier 4 (student)" },
                    { 26, 5, "Visa current within valid passport (not Tier 4)", "MHR_RIGHT_TO_ABODE", "Right to Abode (Current)", "Passport Stamp - Right of Abode" },
                    { 25, 5, "Visa current within valid passport (not Tier 4)", "MHR_IMMIGRATION_STATUS_DOC_ILR", "Immigration Status Document - Indefinite Leave to Remain + NI Number", "Passport Stamp - Leave to Enter or Remain" },
                    { 24, 5, "Visa current within valid passport (not Tier 4)", "MHR_IMMIGRATION_STATUS_DOC_ILR", "Immigration Status Document - Indefinite Leave to Remain + NI Number", "Passport Stamp - Indefinite Leave to Enter or Remain" },
                    { 23, 2, "Passport of British or EEA citizen", null, null, "Passport Additional Page" },
                    { 22, 1, "Passport of British or EEA citizen", null, null, "Passport" },
                    { 21, 5, "Certificate of Registration or Naturalisation plus NI No.", "MHR_CERT_REGISTRATION_NI", "Certificate of Registration or Naturalisation as a British Citizen + NI Number", "Naturalisation Certificate" },
                    { 20, 2, "Birth Certificate British (full) plus NI No.", null, null, "National Insurance Document" },
                    { 19, 2, "Marriage Certificate", null, null, "Marriage certificate, divorce decree absolute, a deed poll or statutory declaration" },
                    { 18, 5, "Immigration Status Document plus NI No.", "MHR_IMMIGRATION_STATUS_DOC_ILR", "Immigration Status Document - Indefinite Leave to Remain + NI Number", "Immigration Status Document - Work Permit" },
                    { 17, 5, "Immigration Status Document plus NI No.", "MHR_IMMIGRATION_STATUS_DOC_ILR", "Immigration Status Document - Indefinite Leave to Remain + NI Number", "Immigration Status Document - Leave to Enter or Remain" },
                    { 16, 5, "Immigration Status Document plus NI No.", "MHR_IMMIGRATION_STATUS_DOC_ILR", "Immigration Status Document - Indefinite Leave to Remain + NI Number", "Immigration Status Document - Indefinite Leave to Enter or Remain" },
                    { 15, 2, "Other Legal Document", null, null, "Frontier Worker Permit" },
                    { 14, 5, "Home Office application/referral evidence", "MHR_COA_HOME_OFF_APPLICATION", "Certificate of Application (Home Office Application)", "ECS Positive Verification Notice - Family" },
                    { 13, 5, "Home Office application/referral evidence", "MHR_COA_HOME_OFF_APPLICATION", "Certificate of Application (Home Office Application)", "ECS Positive Verification Notice - Asylum" },
                    { 12, 5, "Home Office application/referral evidence", "MHR_COA_HOME_OFF_APPLICATION", "Certificate of Application (Home Office Application)", "ECS Positive Verification Notice" },
                    { 11, 5, "Home Office application/referral evidence", "MHR_COA_HOME_OFF_APPLICATION", "Certificate of Application (Home Office Application)", "Certificate of Application Family Member - front" },
                    { 10, 2, "Home Office application/referral evidence", null, null, "Certificate of Application Family Member - back" },
                    { 9, 5, "Birth Certificate British (full) plus NI No.", "MHR_BIRTH_ADOPTION_CI_IOM_NI", "Birth/Adoption Certificate issued in UK, Channel Islands, Isle of Man + NI Numbe", "Birth, Adoption or Naturalisation Certificate" },
                    { 8, 5, "Birth Certificate British (full) plus NI No.", "MHR_BIRTH_ADOPTION_CI_IOM_NI", "Birth/Adoption Certificate issued in UK, Channel Islands, Isle of Man + NI Numbe", "Birth or Adoption Certificate" },
                    { 7, 5, "Biometric Residence Permit current (Tier 4) plus study & term time evidence", "MHR_STUDENT_VISA", "Student Visa", "Biometric Residence Card - Tier 4 (student) - front" },
                    { 6, 2, "Other Legal Document", null, null, "Biometric Residence card - Permanent Residence (EEA) - front" },
                    { 5, 5, "Biometric Residence Permit current (not Tier 4)", "MHR_BRP_ILR", "Biometric Residence Permit (Current) - Indefinite Leave to Remain", "Biometric Residence card - Indefinite Leave - front" },
                    { 4, 2, "Biometric Residence Permit current (not Tier 4)", null, null, "Biometric Residence card - front" },
                    { 3, 5, "Biometric Residence Permit current (not Tier 4)", "MHR_BRP_TIME_LTD_LTR", "Biometric Residence Permit (Current) – Time Limited Leave to Remain", "Biometric Residence card - back" },
                    { 2, 5, "Home Office application/referral evidence", "MHR_ARC_CARD_EMP_PERMIT_ECS", "ARC Card with Employment Permitted + Positive ECS", "Application Registration Card - front" },
                    { 29, 4, "Other Legal Document", null, null, "Share Code Evidence" },
                    { 30, 2, "Biometric Residence Permit current (Tier 4) plus study & term time evidence", null, null, "Student Term Time Evidence" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 30);
        }
    }
}
