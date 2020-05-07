using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendCapstone.Migrations
{
    public partial class Clear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarterTrade_Trade_TradeId",
                table: "BarterTrade");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "31da68d7-b518-4b67-be77-71f8d43e4613", "AQAAAAEAACcQAAAAEDFnR60/0Zorc8b+NDLbgua13r7iqGnNCuxS1pWZtuKFV4BVnffgsQxRWJzUvJJXnA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_BarterTrade_Trade_TradeId",
                table: "BarterTrade",
                column: "TradeId",
                principalTable: "Trade",
                principalColumn: "TradeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarterTrade_Trade_TradeId",
                table: "BarterTrade");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "af402944-fb09-45f9-80c1-fb0d5540af15", "AQAAAAEAACcQAAAAEF9HCYa55p5CqfOyF1UN0HlW/GLLJovMLmgsgEHOtrSaAZjW0O5Ho96x728KRq3v8Q==" });

            migrationBuilder.AddForeignKey(
                name: "FK_BarterTrade_Trade_TradeId",
                table: "BarterTrade",
                column: "TradeId",
                principalTable: "Trade",
                principalColumn: "TradeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
