using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Core.Infra.Data.Npgsql.Migrations.Core
{
    /// <inheritdoc />
    public partial class Core_02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NaturalPeople_Users_Id",
                table: "NaturalPeople");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_NaturalPeople_Users_Id",
                table: "NaturalPeople",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
