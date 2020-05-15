using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SwapShop.Migrations
{
    public partial class DateCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCompleted",
                table: "Trade",
                nullable: true,
                computedColumnSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8d31883c-1d86-4b37-9b55-ffeeebafc41d", "AQAAAAEAACcQAAAAEB9dgoCNvjoLKPORhiwkvjHbHVP7XzWyOpE7eylcKMJM4xh2HdfWlAbSN45rsb/qmg==" });
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
                oldComputedColumnSql: "GETDATE()");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e5d7b766-5d08-49a6-8ec6-5e1ae587a2aa", "AQAAAAEAACcQAAAAEGBT2XYCL8DSuxNoWXplJ/l3RgZQFsDF+NBunvmOqlqf2DuTlYhkK0qP+H/FzqAIMg==" });
        }
    }
}
