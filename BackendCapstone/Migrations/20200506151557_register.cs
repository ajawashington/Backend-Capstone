using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendCapstone.Migrations
{
    public partial class register : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "af402944-fb09-45f9-80c1-fb0d5540af15", "AQAAAAEAACcQAAAAEF9HCYa55p5CqfOyF1UN0HlW/GLLJovMLmgsgEHOtrSaAZjW0O5Ho96x728KRq3v8Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1d4020c8-58cd-4882-a5b8-e5a6cf964557", "AQAAAAEAACcQAAAAEM7pVVKJ0fxgqTK8kPzrcclnPegqWhH5vpqtdLlZu34vUJwX8s9kCejupei8MFDVvQ==" });
        }
    }
}
