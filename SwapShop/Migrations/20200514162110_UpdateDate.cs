using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SwapShop.Migrations
{
    public partial class UpdateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCompleted",
                table: "Trade",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "474729a7-0d5b-438e-ba93-3d6b6fa5e307", "AQAAAAEAACcQAAAAED/6qQnr1Bw/mPGKAC0bzmRHMwjWSAqJ8tOcC/AJZTcSjQw8t7dbyusYwMSDta4txg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCompleted",
                table: "Trade",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "99af2c7a-0f50-44c1-877c-bf75363a35c4", "AQAAAAEAACcQAAAAEPehQOT6///W6UV7Q16GSTJCPnIYecqH02uXzIzi8KM+sQrl3uxt7n3HCsrDWDGx/g==" });
        }
    }
}
