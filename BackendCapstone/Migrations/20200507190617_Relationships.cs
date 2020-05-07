using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendCapstone.Migrations
{
    public partial class Relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //ALTER TABLE[dbo.AspNetUsers] ADD CONSTRAINT[FK_Trade_AspNetUsers_SenderId] FOREIGN KEY([SenderId]) REFERENCES[Trade]([SenderId]) ON DELETE CASCADE;

            migrationBuilder.InsertData(
                table: "Trade",
                columns: new[] { "TradeId", "IsCompleted", "Message", "ReceiverId", "SenderId" },
                values: new object[,]
                {
                    { 3, false, "Hello, I love your products, Would really like to have some of those mushrooms", "3c44096a-bfe3-4bc0-ab01-16b7d8eaa400", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 4, false, "Hello, I love your products, Would really like to have some of those mushrooms", "00000000-ffff-ffff-ffff-ffffffffffff", "3c44096a-bfe3-4bc0-ab01-16b7d8eaa400" },
                    { 5, false, "Hello, I love your products, Would really like to have some of those mushrooms", "3c44096a-bfe3-4bc0-ab01-16b7d8eaa400", "00000000-ffff-ffff-ffff-ffffffffffff" }
                });

            migrationBuilder.InsertData(
                table: "BarterTrade",
                columns: new[] { "BarterTradeId", "BarterItemId", "TradeId" },
                values: new object[] { 1, 3, 4 });

            migrationBuilder.InsertData(
                table: "BarterTrade",
                columns: new[] { "BarterTradeId", "BarterItemId", "TradeId" },
                values: new object[] { 2, 4, 4 });
           
            migrationBuilder.AddForeignKey(
                name: "FK_Trade_AspNetUsers_SenderId",
                table: "dbo.AspNetUsers",
                column: "SenderId",
                principalTable: "Trade",
                principalColumn: "SenderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BarterTrade",
                keyColumn: "BarterTradeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BarterTrade",
                keyColumn: "BarterTradeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trade",
                keyColumn: "TradeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Trade",
                keyColumn: "TradeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Trade",
                keyColumn: "TradeId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "31da68d7-b518-4b67-be77-71f8d43e4613", "AQAAAAEAACcQAAAAEDFnR60/0Zorc8b+NDLbgua13r7iqGnNCuxS1pWZtuKFV4BVnffgsQxRWJzUvJJXnA==" });
        }
    }
}
