using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentEnrollment.DATA.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUsersAndRoleAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "802a4bae-e065-4467-aae3-ea33d6154eba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1033a88-7ce7-48f4-9fa1-5952685c81bf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "57847633-c07e-4953-8eda-fc653de57353", null, "Aministrator", "ADMINISTRATOR" },
                    { "eb521001-11c8-4eae-a29d-e3acea410518", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3f4631bd-f907-4409-b416-ba356312e659", 0, "55ed8e8d-b5be-41b2-962e-e9e90a340dfb", null, "schooluser@localhost.com", true, "School", "User", false, null, "SCHOOLUSER@LOCALHOST.COM", "SCHOOLUSER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEOuSP/8YcBcGEA1NDNPWy3Mb1R6YZDroRAPM0k6zhKYCP47bSD1qBiMR6dl/eaO+XQ==", null, false, "49f8c564-7a6c-44f5-a2b6-46985650a340", false, "schooluser@localhost.com" },
                    { "408aa945-3d84-4421-8342-7269ec64d949", 0, "87e53bf1-62c7-4bcc-b2e5-2be28c5d2802", null, "schooladmin@localhost.com", true, "School", "Admin", false, null, "SCHOOLADMIN@LOCALHOST.COM", "SCHOOLADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEP+b+QbXC8TMPG6XVCpd+h9knvs5GjRrgYG6dVKu7n1uw1+9MZIubnio0U3lHi8FiA==", null, false, "74aca980-0d0b-4308-b2dc-bcf62644f704", false, "schooladmin@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "eb521001-11c8-4eae-a29d-e3acea410518", "3f4631bd-f907-4409-b416-ba356312e659" },
                    { "57847633-c07e-4953-8eda-fc653de57353", "408aa945-3d84-4421-8342-7269ec64d949" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eb521001-11c8-4eae-a29d-e3acea410518", "3f4631bd-f907-4409-b416-ba356312e659" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "57847633-c07e-4953-8eda-fc653de57353", "408aa945-3d84-4421-8342-7269ec64d949" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57847633-c07e-4953-8eda-fc653de57353");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb521001-11c8-4eae-a29d-e3acea410518");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "802a4bae-e065-4467-aae3-ea33d6154eba", null, "Aministrator", "ADMINISTRATOR" },
                    { "c1033a88-7ce7-48f4-9fa1-5952685c81bf", null, "User", "USER" }
                });
        }
    }
}
