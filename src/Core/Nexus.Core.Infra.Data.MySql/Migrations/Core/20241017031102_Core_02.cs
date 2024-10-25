using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Core.Infra.Data.MySql.Migrations.Core;

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
            type: "varchar(64)",
            maxLength: 64,
            nullable: false,
            defaultValue: "")
            .Annotation("MySql:CharSet", "utf8mb4");
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
