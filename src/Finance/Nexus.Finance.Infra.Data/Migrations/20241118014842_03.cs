using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Finance.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class _03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PersonDocumentTypes",
                newName: "PersonDocumentTypes",
                newSchema: "Finance");

            migrationBuilder.RenameTable(
                name: "NaturalPeople",
                newName: "NaturalPeople",
                newSchema: "Finance");

            migrationBuilder.RenameTable(
                name: "LegalPeople",
                newName: "LegalPeople",
                newSchema: "Finance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PersonDocumentTypes",
                schema: "Finance",
                newName: "PersonDocumentTypes");

            migrationBuilder.RenameTable(
                name: "NaturalPeople",
                schema: "Finance",
                newName: "NaturalPeople");

            migrationBuilder.RenameTable(
                name: "LegalPeople",
                schema: "Finance",
                newName: "LegalPeople");
        }
    }
}
