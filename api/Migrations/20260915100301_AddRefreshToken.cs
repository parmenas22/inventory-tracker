using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("2a14c9ea-4c32-4bc7-9a95-83a88ab38f5b"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("8734cc11-60c5-4b9b-83ad-398e6f0da893"));

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    RefreshTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HashRefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevokedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("4d97305a-73bd-468b-9383-4c08fc1ae98e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 9, 15, 10, 2, 59, 472, DateTimeKind.Utc).AddTicks(5646), new DateTime(2026, 9, 15, 10, 2, 59, 472, DateTimeKind.Utc).AddTicks(5646) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("4f9d04d9-f0b4-4c46-b04c-e689876274bb"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 9, 15, 10, 2, 59, 472, DateTimeKind.Utc).AddTicks(5641), new DateTime(2026, 9, 15, 10, 2, 59, 472, DateTimeKind.Utc).AddTicks(5642) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 9, 15, 10, 2, 59, 472, DateTimeKind.Utc).AddTicks(5636), new DateTime(2026, 9, 15, 10, 2, 59, 472, DateTimeKind.Utc).AddTicks(5637) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("90dd49d9-47f5-486f-b1b2-d1a0e8fceffd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 9, 15, 10, 2, 59, 472, DateTimeKind.Utc).AddTicks(5644), new DateTime(2026, 9, 15, 10, 2, 59, 472, DateTimeKind.Utc).AddTicks(5644) });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CategoryId", "CreatedAt", "CreatedBy", "IsDeleted", "MinStockAlert", "Name", "Price", "Quantity", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("0dac6d17-39c6-4e1f-9372-8af3ee271c7b"), new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"), new DateTime(2026, 9, 15, 10, 2, 59, 472, DateTimeKind.Utc).AddTicks(3710), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), false, 10, "Lenovo ThinkPad", 100000m, 30, new DateTime(2026, 9, 15, 10, 2, 59, 472, DateTimeKind.Utc).AddTicks(3710), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("46949e5a-fff9-4eb7-b8a0-c7b60c97ae23"), new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"), new DateTime(2026, 9, 15, 10, 2, 59, 472, DateTimeKind.Utc).AddTicks(3692), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), false, 10, "Iphone 17 pro", 150000m, 25, new DateTime(2026, 9, 15, 10, 2, 59, 472, DateTimeKind.Utc).AddTicks(3697), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("73de8fa4-119e-4e36-81bc-17ff5762ac44"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 9, 15, 10, 2, 59, 470, DateTimeKind.Utc).AddTicks(2224), new DateTime(2026, 9, 15, 10, 2, 59, 470, DateTimeKind.Utc).AddTicks(2224) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("79471720-7377-4526-8d05-32163c09fd82"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 9, 15, 10, 2, 59, 470, DateTimeKind.Utc).AddTicks(2212), new DateTime(2026, 9, 15, 10, 2, 59, 470, DateTimeKind.Utc).AddTicks(2218) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 9, 15, 10, 2, 59, 140, DateTimeKind.Utc).AddTicks(6161), "$2a$11$0eCDt2TU7dBSfN1xTbxnwOMzvbA/C3Y7QC4H3f9pLvvJQVf1DJ8Ye", new DateTime(2026, 9, 15, 10, 2, 59, 140, DateTimeKind.Utc).AddTicks(6167) });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("0dac6d17-39c6-4e1f-9372-8af3ee271c7b"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("46949e5a-fff9-4eb7-b8a0-c7b60c97ae23"));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("4d97305a-73bd-468b-9383-4c08fc1ae98e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4775), new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4775) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("4f9d04d9-f0b4-4c46-b04c-e689876274bb"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4772), new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4772) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4770), new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("90dd49d9-47f5-486f-b1b2-d1a0e8fceffd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4773), new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4774) });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CategoryId", "CreatedAt", "CreatedBy", "IsDeleted", "MinStockAlert", "Name", "Price", "Quantity", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("2a14c9ea-4c32-4bc7-9a95-83a88ab38f5b"), new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"), new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4038), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), false, 10, "Iphone 17 pro", 150000m, 25, new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4038), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("8734cc11-60c5-4b9b-83ad-398e6f0da893"), new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"), new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4047), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), false, 10, "Lenovo ThinkPad", 100000m, 30, new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4048), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("73de8fa4-119e-4e36-81bc-17ff5762ac44"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 196, DateTimeKind.Utc).AddTicks(7047), new DateTime(2025, 10, 29, 11, 18, 59, 196, DateTimeKind.Utc).AddTicks(7048) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("79471720-7377-4526-8d05-32163c09fd82"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 196, DateTimeKind.Utc).AddTicks(7038), new DateTime(2025, 10, 29, 11, 18, 59, 196, DateTimeKind.Utc).AddTicks(7042) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 2, DateTimeKind.Utc).AddTicks(3259), "$2a$11$la58H5paKVkarVC9SUCcpelDCbXVF/g4rPS2Fq4h7nY9lzeESaNxy", new DateTime(2025, 10, 29, 11, 18, 59, 2, DateTimeKind.Utc).AddTicks(3261) });
        }
    }
}
