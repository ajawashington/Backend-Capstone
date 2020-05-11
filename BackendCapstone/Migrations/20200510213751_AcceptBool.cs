using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendCapstone.Migrations
{
    public partial class AcceptBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "Trade",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "582ecef5-8458-43d7-b16a-eda9ff992819", "AQAAAAEAACcQAAAAEAxZ31vRTe9O/BkRUD3fROupcW4/PCTxS7zF0TESS89jswInc6jES/llRTVCK9T6ig==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Trade");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "138f7e61-4465-44de-826f-65d3165bb945", "AQAAAAEAACcQAAAAECBJnwbZrAUKqjdpGJBMpfR2rS6H/mdNfvQM1z+jXqfump3sM0FB6v5Z/ai/nxHwSQ==" });
        }
    }
}
