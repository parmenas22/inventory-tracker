using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedNewAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("428437b3-6c12-4354-97d1-af7342ca2624"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("8a31feec-20c3-450c-ba65-77811f491cd6"));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("4d97305a-73bd-468b-9383-4c08fc1ae98e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(5347), new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(5348) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("4f9d04d9-f0b4-4c46-b04c-e689876274bb"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(5344), new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(5345) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(5341), new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(5342) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("90dd49d9-47f5-486f-b1b2-d1a0e8fceffd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(5346), new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(5346) });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CategoryId", "CreatedAt", "CreatedBy", "IsDeleted", "MinStockAlert", "Name", "Price", "Quantity", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("321f1c4d-79fd-4921-b35e-4a13859152a5"), new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"), new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(3784), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), false, 10, "Lenovo ThinkPad", 100000m, 30, new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(3785), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6ee2176c-dba5-4fec-b2de-e0d314b09ffe"), new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"), new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(3774), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), false, 10, "Iphone 17 pro", 150000m, 25, new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(3775), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("73de8fa4-119e-4e36-81bc-17ff5762ac44"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 28, 8, 55, 14, 356, DateTimeKind.Utc).AddTicks(127), new DateTime(2026, 10, 28, 8, 55, 14, 356, DateTimeKind.Utc).AddTicks(127) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("79471720-7377-4526-8d05-32163c09fd82"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 28, 8, 55, 14, 356, DateTimeKind.Utc).AddTicks(118), new DateTime(2026, 10, 28, 8, 55, 14, 356, DateTimeKind.Utc).AddTicks(122) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 28, 8, 55, 13, 958, DateTimeKind.Utc).AddTicks(3073), "$2a$11$zc0u6oYJ8zN/GG2IOT/NBOoIcMyih53aiaj3KrjHNi5ZpK0k16JDa", new DateTime(2026, 10, 28, 8, 55, 13, 958, DateTimeKind.Utc).AddTicks(3076) });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "CreatedAt", "CreatedBy", "Email", "EmailLoginCodeAttempts", "EmailLoginCodeExpiresAt", "EmailLoginCodeHash", "EmailLoginCodeSentAt", "FirstName", "IsDeleted", "LastName", "Password", "ResetToken", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("3f97d2b5-4738-4422-9dbe-ec19656466c3"), new DateTime(2026, 10, 28, 8, 55, 14, 152, DateTimeKind.Utc).AddTicks(6209), new Guid("00000000-0000-0000-0000-000000000000"), "machariaparmenas22@gmail.com", 0, null, null, null, "Parmenas", false, "Macharia", "$2a$11$p1QwgFRC9tDheD.Pn8YxzOTpBTtBnmPBnthmGH4USPXY.RPaCmCFW", null, new DateTime(2026, 10, 28, 8, 55, 14, 152, DateTimeKind.Utc).AddTicks(6214), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "CreatedAt", "CreatedBy", "IsDeleted", "RoleId", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[,]
                {
                    { new Guid("69dd297c-9d7b-44ab-beae-f1cc002e4541"), new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(533), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), false, new Guid("79471720-7377-4526-8d05-32163c09fd82"), new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(533), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144") },
                    { new Guid("d5e351c9-68a2-4224-8706-01567b82aaad"), new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(495), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), false, new Guid("79471720-7377-4526-8d05-32163c09fd82"), new DateTime(2026, 10, 28, 8, 55, 14, 357, DateTimeKind.Utc).AddTicks(497), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("3f97d2b5-4738-4422-9dbe-ec19656466c3") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("321f1c4d-79fd-4921-b35e-4a13859152a5"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("6ee2176c-dba5-4fec-b2de-e0d314b09ffe"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("69dd297c-9d7b-44ab-beae-f1cc002e4541"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("d5e351c9-68a2-4224-8706-01567b82aaad"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("3f97d2b5-4738-4422-9dbe-ec19656466c3"));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("4d97305a-73bd-468b-9383-4c08fc1ae98e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 27, 9, 0, 55, 338, DateTimeKind.Utc).AddTicks(6954), new DateTime(2026, 10, 27, 9, 0, 55, 338, DateTimeKind.Utc).AddTicks(6955) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("4f9d04d9-f0b4-4c46-b04c-e689876274bb"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 27, 9, 0, 55, 338, DateTimeKind.Utc).AddTicks(6949), new DateTime(2026, 10, 27, 9, 0, 55, 338, DateTimeKind.Utc).AddTicks(6950) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 27, 9, 0, 55, 338, DateTimeKind.Utc).AddTicks(6942), new DateTime(2026, 10, 27, 9, 0, 55, 338, DateTimeKind.Utc).AddTicks(6945) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("90dd49d9-47f5-486f-b1b2-d1a0e8fceffd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 27, 9, 0, 55, 338, DateTimeKind.Utc).AddTicks(6952), new DateTime(2026, 10, 27, 9, 0, 55, 338, DateTimeKind.Utc).AddTicks(6952) });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CategoryId", "CreatedAt", "CreatedBy", "IsDeleted", "MinStockAlert", "Name", "Price", "Quantity", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("428437b3-6c12-4354-97d1-af7342ca2624"), new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"), new DateTime(2026, 10, 27, 9, 0, 55, 338, DateTimeKind.Utc).AddTicks(3071), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), false, 10, "Lenovo ThinkPad", 100000m, 30, new DateTime(2026, 10, 27, 9, 0, 55, 338, DateTimeKind.Utc).AddTicks(3071), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("8a31feec-20c3-450c-ba65-77811f491cd6"), new Guid("5e0045b8-03fc-4dbb-95fb-4c676e5533a1"), new DateTime(2026, 10, 27, 9, 0, 55, 338, DateTimeKind.Utc).AddTicks(3044), new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"), false, 10, "Iphone 17 pro", 150000m, 25, new DateTime(2026, 10, 27, 9, 0, 55, 338, DateTimeKind.Utc).AddTicks(3052), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("73de8fa4-119e-4e36-81bc-17ff5762ac44"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 27, 9, 0, 55, 334, DateTimeKind.Utc).AddTicks(418), new DateTime(2026, 10, 27, 9, 0, 55, 334, DateTimeKind.Utc).AddTicks(419) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("79471720-7377-4526-8d05-32163c09fd82"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 27, 9, 0, 55, 334, DateTimeKind.Utc).AddTicks(409), new DateTime(2026, 10, 27, 9, 0, 55, 334, DateTimeKind.Utc).AddTicks(414) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("888a7a52-03ac-4cfc-a4fa-00b3e225c144"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 27, 9, 0, 54, 946, DateTimeKind.Utc).AddTicks(5019), "$2a$11$nKKPWIHHtylJHHtsZanUZuLoxgSsXctY4Kuif0eZpQFa/1XawXNXq", new DateTime(2026, 10, 27, 9, 0, 54, 946, DateTimeKind.Utc).AddTicks(5024) });
        }
    }
}
