using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendCapstone.Migrations
{
    public partial class RequestedAmountBarterTrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestedAmount",
                table: "BarterTrade",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c851010d-217d-464f-b9f4-58529231c7a3", "AQAAAAEAACcQAAAAEMg1x2XWBIrt9S5nBx0n3MMLwSmYqdQo+xjf9ALqavPO7dm+jVntePVjmW/e5iBKwA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestedAmount",
                table: "BarterTrade");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "582ecef5-8458-43d7-b16a-eda9ff992819", "AQAAAAEAACcQAAAAEAxZ31vRTe9O/BkRUD3fROupcW4/PCTxS7zF0TESS89jswInc6jES/llRTVCK9T6ig==" });
        }
    }
}
