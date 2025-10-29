using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByToSeedingScript : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("2a1a0a2b-5fdc-43fb-b399-47afadfc6606"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("e16a92ea-544a-4c8f-8674-1ababba8b1e3"));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("4d97305a-73bd-468b-9383-4c08fc1ae98e"),
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4775), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4775) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("4f9d04d9-f0b4-4c46-b04c-e689876274bb"),
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4772), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4772) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"),
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4770), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("90dd49d9-47f5-486f-b1b2-d1a0e8fceffd"),
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4773), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), new DateTime(2025, 10, 29, 11, 18, 59, 197, DateTimeKind.Utc).AddTicks(4774) });

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
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 196, DateTimeKind.Utc).AddTicks(7047), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), new DateTime(2025, 10, 29, 11, 18, 59, 196, DateTimeKind.Utc).AddTicks(7048) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("79471720-7377-4526-8d05-32163c09fd82"),
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 196, DateTimeKind.Utc).AddTicks(7038), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), new DateTime(2025, 10, 29, 11, 18, 59, 196, DateTimeKind.Utc).AddTicks(7042) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 18, 59, 2, DateTimeKind.Utc).AddTicks(3259), "$2a$11$la58H5paKVkarVC9SUCcpelDCbXVF/g4rPS2Fq4h7nY9lzeESaNxy", new DateTime(2025, 10, 29, 11, 18, 59, 2, DateTimeKind.Utc).AddTicks(3261) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("2a14c9ea-4c32-4bc7-9a95-83a88ab38f5b"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("8734cc11-60c5-4b9b-83ad-398e6f0da893"));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("4d97305a-73bd-468b-9383-4c08fc1ae98e"),
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9315), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9316) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("4f9d04d9-f0b4-4c46-b04c-e689876274bb"),
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9313), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9313) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"),
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9310), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9310) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("90dd49d9-47f5-486f-b1b2-d1a0e8fceffd"),
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9314), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(9315) });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CategoryId", "CreatedAt", "CreatedBy", "IsDeleted", "MinStockAlert", "Name", "Price", "Quantity", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("2a1a0a2b-5fdc-43fb-b399-47afadfc6606"), new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(8740), new Guid("00000000-0000-0000-0000-000000000000"), false, 10, "Lenovo ThinkPad", 100000m, 30, new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(8740), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("e16a92ea-544a-4c8f-8674-1ababba8b1e3"), new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(8733), new Guid("00000000-0000-0000-0000-000000000000"), false, 10, "Iphone 17 pro", 150000m, 25, new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(8734), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("73de8fa4-119e-4e36-81bc-17ff5762ac44"),
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(4032), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(4033) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("79471720-7377-4526-8d05-32163c09fd82"),
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(4023), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 10, 29, 11, 0, 33, 607, DateTimeKind.Utc).AddTicks(4027) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 29, 11, 0, 33, 384, DateTimeKind.Utc).AddTicks(8160), "$2a$11$Qw2C3bmm6j8nZF21nAmOlONGqK20uDexSC4QejSO8hyfTNr4iSBGa", new DateTime(2025, 10, 29, 11, 0, 33, 384, DateTimeKind.Utc).AddTicks(8162) });
        }
    }
}
