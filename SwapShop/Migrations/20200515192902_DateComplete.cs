using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SwapShop.Migrations
{
    public partial class DateComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCompleted",
                table: "Trade",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e5d7b766-5d08-49a6-8ec6-5e1ae587a2aa", "AQAAAAEAACcQAAAAEGBT2XYCL8DSuxNoWXplJ/l3RgZQFsDF+NBunvmOqlqf2DuTlYhkK0qP+H/FzqAIMg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCompleted",
                table: "Trade",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "474729a7-0d5b-438e-ba93-3d6b6fa5e307", "AQAAAAEAACcQAAAAED/6qQnr1Bw/mPGKAC0bzmRHMwjWSAqJ8tOcC/AJZTcSjQw8t7dbyusYwMSDta4txg==" });
        }
    }
}
