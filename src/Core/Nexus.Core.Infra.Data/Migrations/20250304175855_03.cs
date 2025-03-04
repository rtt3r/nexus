using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Core.Infra.Data.Migrations;

/// <inheritdoc />
public partial class _03 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_BusinessGroups_TaxId",
            schema: "Core",
            table: "BusinessGroups");

        migrationBuilder.AlterColumn<string>(
            name: "TaxId",
            schema: "Core",
            table: "BusinessGroups",
            type: "nvarchar(32)",
            maxLength: 32,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(32)",
            oldMaxLength: 32);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            schema: "Core",
            table: "BusinessGroups",
            type: "nvarchar(256)",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(256)",
            oldMaxLength: 256);

        migrationBuilder.CreateIndex(
            name: "IX_BusinessGroups_TaxId",
            schema: "Core",
            table: "BusinessGroups",
            column: "TaxId",
            unique: true,
            filter: "[TaxId] IS NOT NULL");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_BusinessGroups_TaxId",
            schema: "Core",
            table: "BusinessGroups");

        migrationBuilder.AlterColumn<string>(
            name: "TaxId",
            schema: "Core",
            table: "BusinessGroups",
            type: "nvarchar(32)",
            maxLength: 32,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(32)",
            oldMaxLength: 32,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            schema: "Core",
            table: "BusinessGroups",
            type: "nvarchar(256)",
            maxLength: 256,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(256)",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_BusinessGroups_TaxId",
            schema: "Core",
            table: "BusinessGroups",
            column: "TaxId",
            unique: true);
    }
}
