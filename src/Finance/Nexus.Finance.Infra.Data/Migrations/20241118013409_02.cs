using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Finance.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class _02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Finance");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "Finance");

            migrationBuilder.RenameTable(
                name: "PersonPhones",
                newName: "PersonPhones",
                newSchema: "Finance");

            migrationBuilder.RenameTable(
                name: "PersonEmails",
                newName: "PersonEmails",
                newSchema: "Finance");

            migrationBuilder.RenameTable(
                name: "PersonDocuments",
                newName: "PersonDocuments",
                newSchema: "Finance");

            migrationBuilder.RenameTable(
                name: "PersonAddresses",
                newName: "PersonAddresses",
                newSchema: "Finance");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "People",
                newSchema: "Finance");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customers",
                newSchema: "Finance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Finance",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "PersonPhones",
                schema: "Finance",
                newName: "PersonPhones");

            migrationBuilder.RenameTable(
                name: "PersonEmails",
                schema: "Finance",
                newName: "PersonEmails");

            migrationBuilder.RenameTable(
                name: "PersonDocuments",
                schema: "Finance",
                newName: "PersonDocuments");

            migrationBuilder.RenameTable(
                name: "PersonAddresses",
                schema: "Finance",
                newName: "PersonAddresses");

            migrationBuilder.RenameTable(
                name: "People",
                schema: "Finance",
                newName: "People");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "Finance",
                newName: "Customers");
        }
    }
}
