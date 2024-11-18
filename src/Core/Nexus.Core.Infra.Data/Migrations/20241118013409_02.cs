using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Core.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class _02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "PersonPhones",
                newName: "PersonPhones",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "PersonEmails",
                newName: "PersonEmails",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "PersonDocuments",
                newName: "PersonDocuments",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "PersonAddresses",
                newName: "PersonAddresses",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "People",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customers",
                newSchema: "Core");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Core",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "PersonPhones",
                schema: "Core",
                newName: "PersonPhones");

            migrationBuilder.RenameTable(
                name: "PersonEmails",
                schema: "Core",
                newName: "PersonEmails");

            migrationBuilder.RenameTable(
                name: "PersonDocuments",
                schema: "Core",
                newName: "PersonDocuments");

            migrationBuilder.RenameTable(
                name: "PersonAddresses",
                schema: "Core",
                newName: "PersonAddresses");

            migrationBuilder.RenameTable(
                name: "People",
                schema: "Core",
                newName: "People");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "Core",
                newName: "Customers");
        }
    }
}
