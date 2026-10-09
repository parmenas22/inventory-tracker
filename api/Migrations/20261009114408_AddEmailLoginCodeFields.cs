using api.Database;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20261009114408_AddEmailLoginCodeFields")]
    public partial class AddEmailLoginCodeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmailLoginCodeAttempts",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EmailLoginCodeExpiresAt",
                table: "User",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailLoginCodeHash",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EmailLoginCodeSentAt",
                table: "User",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "EmailLoginCodeAttempts", table: "User");
            migrationBuilder.DropColumn(name: "EmailLoginCodeExpiresAt", table: "User");
            migrationBuilder.DropColumn(name: "EmailLoginCodeHash", table: "User");
            migrationBuilder.DropColumn(name: "EmailLoginCodeSentAt", table: "User");
        }
    }
}
