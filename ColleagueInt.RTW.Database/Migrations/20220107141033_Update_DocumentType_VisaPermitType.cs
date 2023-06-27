using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_DocumentType_VisaPermitType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HCMVisaPermitTypeCode",
                table: "DocumentTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HCMVisaPermitTypeDescription",
                table: "DocumentTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_BRP_ILR", "Biometric Residence Permit (Current) - Indefinite Leave to Remain" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_BRP_ILR", "Biometric Residence Permit (Current) - Indefinite Leave to Remain" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_BRP_ILR", "Biometric Residence Permit (Current) - Indefinite Leave to Remain" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_BRP_ILR", "Biometric Residence Permit (Current) - Indefinite Leave to Remain" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_BRP_TIME_LTD_LTR", "Biometric Residence Permit (Current) – Time Limited Leave to Remain" });

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
                keyValue: 38,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_RIGHT_TO_ABODE", "Right to Abode (Current)" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_STUDENT_VISA", "Student Visa" });

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "HCMVisaPermitTypeCode", "HCMVisaPermitTypeDescription" },
                values: new object[] { "MHR_STUDENT_VISA", "Student Visa" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HCMVisaPermitTypeCode",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "HCMVisaPermitTypeDescription",
                table: "DocumentTypes");
        }
    }
}
