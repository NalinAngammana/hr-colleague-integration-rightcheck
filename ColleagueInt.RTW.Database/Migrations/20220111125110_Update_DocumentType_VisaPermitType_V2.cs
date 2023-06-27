using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_DocumentType_VisaPermitType_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_IMMIGRATION_STATUS_DOC_ILR", "Immigration Status Document - Indefinite Leave to Remain + NI Number" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_IMMIGRATION_STATUS_DOC_ILR", "Immigration Status Document - Indefinite Leave to Remain + NI Number" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_IMMIGRATION_STATUS_DOC_ILR", "Immigration Status Document - Indefinite Leave to Remain + NI Number" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_IMMIGRATION_STATUS_DOC_ILR", "Immigration Status Document - Indefinite Leave to Remain + NI Number" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_TIER_1_GENERAL_VISA", "Tier 1 General Visa" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_CURR_IMMIGRATION_NI", "Current Immigration Status Document + NI Number" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_CURR_IMMIGRATION_NI", "Current Immigration Status Document + NI Number" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_CURR_IMMIGRATION_NI", "Current Immigration Status Document + NI Number" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "PERM", "Permanent" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "PERM", "Permanent" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "PERM", "Permanent" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "ORA_HRX_RESIDENT_VISA", "Resident Visa" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "ORA_HRX_RESIDENT_VISA", "Resident Visa" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "SG_PR", "Permanent residence" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "SG_PR", "Permanent residence" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "ORA_HRX_GENERAL_WORK_PERMIT", "General work permit or visa" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "CH_C", "Settled" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "DE_RTWWP", "Residence title with work permit" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "DE_RTWWP", "Residence title with work permit" });
        }
    }
}
