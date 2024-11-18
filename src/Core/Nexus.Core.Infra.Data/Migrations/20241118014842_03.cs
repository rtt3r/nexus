using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Core.Infra.Data.Migrations
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
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "NaturalPeople",
                newName: "NaturalPeople",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "LegalPeople",
                newName: "LegalPeople",
                newSchema: "Core");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PersonDocumentTypes",
                schema: "Core",
                newName: "PersonDocumentTypes");

            migrationBuilder.RenameTable(
                name: "NaturalPeople",
                schema: "Core",
                newName: "NaturalPeople");

            migrationBuilder.RenameTable(
                name: "LegalPeople",
                schema: "Core",
                newName: "LegalPeople");
        }
    }
}
