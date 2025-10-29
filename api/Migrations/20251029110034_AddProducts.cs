using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("ec8a8f10-1fba-4a9a-b456-237795dfdd37"));

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MinStockAlert = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("4d97305a-73bd-468b-9383-4c08fc1ae98e"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9315), new Guid("00000000-0000-0000-0000-000000000000"), false, "Clothing", new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9316), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4f9d04d9-f0b4-4c46-b04c-e689876274bb"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9313), new Guid("00000000-0000-0000-0000-000000000000"), false, "Furniture", new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9313), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9310), new Guid("00000000-0000-0000-0000-000000000000"), false, "Electronics", new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9310), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("90dd49d9-47f5-486f-b1b2-d1a0e8fceffd"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9314), new Guid("00000000-0000-0000-0000-000000000000"), false, "Food", new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9315), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("73de8fa4-119e-4e36-81bc-17ff5762ac44"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(4032), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(4033) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("79471720-7377-4526-8d05-32163c09fd82"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(4023), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(4027) });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "CreatedAt", "CreatedBy", "Email", "FirstName", "IsDeleted", "LastName", "Password", "ResetToken", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), new DateTime(2025, 10, 29, 11, 0, 33, 384, DateTimeKind.Utc).AddTicks(8160), new Guid("00000000-0000-0000-0000-000000000000"), "system@user.com", "System", false, "User", "$2a$11$Qw2C3bmm6j8nZF21nAmOlONGqK20uDexSC4QejSO8hyfTNr4iSBGa", null, new DateTime(2025, 10, 29, 11, 0, 33, 384, DateTimeKind.Utc).AddTicks(8162), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CategoryId", "CreatedAt", "CreatedBy", "IsDeleted", "MinStockAlert", "Name", "Price", "Quantity", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("2a1a0a2b-5fdc-43fb-b399-47afadfc6606"), new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(8740), new Guid("00000000-0000-0000-0000-000000000000"), false, 10, "Lenovo ThinkPad", 100000m, 30, new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(8740), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("e16a92ea-544a-4c8f-8674-1ababba8b1e3"), new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(8733), new Guid("00000000-0000-0000-0000-000000000000"), false, 10, "Iphone 17 pro", 150000m, 25, new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(8734), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("73de8fa4-119e-4e36-81bc-17ff5762ac44"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 27, 16, 20, 53, 823, DateTimeKind.Utc).AddTicks(9351), new DateTime(2025, 10, 27, 16, 20, 53, 823, DateTimeKind.Utc).AddTicks(9352) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("79471720-7377-4526-8d05-32163c09fd82"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 27, 16, 20, 53, 823, DateTimeKind.Utc).AddTicks(9257), new DateTime(2025, 10, 27, 16, 20, 53, 823, DateTimeKind.Utc).AddTicks(9262) });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "CreatedAt", "CreatedBy", "Email", "FirstName", "IsDeleted", "LastName", "Password", "ResetToken", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("ec8a8f10-1fba-4a9a-b456-237795dfdd37"), new DateTime(2025, 10, 27, 16, 20, 53, 556, DateTimeKind.Utc).AddTicks(7381), new Guid("00000000-0000-0000-0000-000000000000"), "system@user.com", "System", false, "User", "$2a$11$Mp.2XbF1p2L.DR7dzdozueoakx2nfIN32004fXHccAOotrqFfSsZO", null, new DateTime(2025, 10, 27, 16, 20, 53, 556, DateTimeKind.Utc).AddTicks(7384), new Guid("00000000-0000-0000-0000-000000000000") });
        }
    }
}
