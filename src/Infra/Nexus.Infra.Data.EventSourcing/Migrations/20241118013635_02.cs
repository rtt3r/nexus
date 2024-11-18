using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Infra.Data.EventSourcing.Migrations
{
    /// <inheritdoc />
    public partial class _02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "EventSourcing");

            migrationBuilder.RenameTable(
                name: "StoredEvents",
                newName: "StoredEvents",
                newSchema: "EventSourcing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "StoredEvents",
                schema: "EventSourcing",
                newName: "StoredEvents");
        }
    }
}
