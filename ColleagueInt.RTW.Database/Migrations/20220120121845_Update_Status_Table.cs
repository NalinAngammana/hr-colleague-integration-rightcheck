using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_Status_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExternalValue", "StatusName" },
                values: new object[] { "CheckNotCompleted", "CheckNotCompleted" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExternalValue", "StatusName" },
                values: new object[] { "Successful", "Successful" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExternalValue", "StatusName" },
                values: new object[] { "Failed", "Failed" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ExternalValue", "StatusName" },
                values: new object[] { "PartialCheck", "PartialCheck" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ExternalValue", "StatusName" },
                values: new object[] { "UserRemovedCheck", "UserRemovedCheck" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "ExternalValue", "StatusName" },
                values: new object[] { 6, "SystemRemovedCheck", "SystemRemovedCheck" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExternalValue", "StatusName" },
                values: new object[] { "Check Not Completed", "Check Not Completed" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExternalValue", "StatusName" },
                values: new object[] { "Successful Check", "Successful Check" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExternalValue", "StatusName" },
                values: new object[] { "Failed Check", "Failed Check" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ExternalValue", "StatusName" },
                values: new object[] { "Partial Check", "Partial Check" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ExternalValue", "StatusName" },
                values: new object[] { "User Removed Check", "User Removed Check" });
        }
    }
}
