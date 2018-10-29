using Microsoft.EntityFrameworkCore.Migrations;

namespace ESportStatistics.Data.Migrations
{
    public partial class SEED_ROLES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a3dedad8-8c85-476a-82cd-31c1d8b7bb1e", "e403caea-9686-47ea-99a7-cec1ff487790", "user", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae1efce3-1799-4543-9043-c8585301924f", "a054707f-4b8d-4c14-807b-9e8c18400699", "administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a3dedad8-8c85-476a-82cd-31c1d8b7bb1e", "e403caea-9686-47ea-99a7-cec1ff487790" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "ae1efce3-1799-4543-9043-c8585301924f", "a054707f-4b8d-4c14-807b-9e8c18400699" });
        }
    }
}
