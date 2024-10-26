using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Core.Infra.Data.MySql.Migrations.Core;

/// <inheritdoc />
public partial class Core_03 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_PersonAddresses_PersonAddressTypes_TypeId",
            table: "PersonAddresses");

        migrationBuilder.DropTable(
            name: "PersonAddressTypes");

        migrationBuilder.DropTable(
            name: "PersonContacts");

        migrationBuilder.DropTable(
            name: "PersonContactTypes");

        migrationBuilder.DropIndex(
            name: "IX_PersonAddresses_TypeId",
            table: "PersonAddresses");

        migrationBuilder.DropColumn(
            name: "TypeId",
            table: "PersonAddresses");

        migrationBuilder.AddColumn<bool>(
            name: "Principal",
            table: "PersonAddresses",
            type: "tinyint(1)",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<int>(
            name: "Type",
            table: "PersonAddresses",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "PersonEmails",
            columns: table => new
            {
                Id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                PersonId = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                MailAddress = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Principal = table.Column<bool>(type: "tinyint(1)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersonEmails", x => x.Id);
                table.ForeignKey(
                    name: "FK_PersonEmails_People_PersonId",
                    column: x => x.PersonId,
                    principalTable: "People",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "PersonPhones",
            columns: table => new
            {
                Id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                PersonId = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                CountryCode = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Number = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Principal = table.Column<bool>(type: "tinyint(1)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersonPhones", x => x.Id);
                table.ForeignKey(
                    name: "FK_PersonPhones_People_PersonId",
                    column: x => x.PersonId,
                    principalTable: "People",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_PersonEmails_PersonId_MailAddress",
            table: "PersonEmails",
            columns: ["PersonId", "MailAddress"],
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_PersonPhones_PersonId_CountryCode_Number",
            table: "PersonPhones",
            columns: ["PersonId", "CountryCode", "Number"],
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PersonEmails");

        migrationBuilder.DropTable(
            name: "PersonPhones");

        migrationBuilder.DropColumn(
            name: "Principal",
            table: "PersonAddresses");

        migrationBuilder.DropColumn(
            name: "Type",
            table: "PersonAddresses");

        migrationBuilder.AddColumn<string>(
            name: "TypeId",
            table: "PersonAddresses",
            type: "varchar(64)",
            maxLength: 64,
            nullable: false,
            defaultValue: "")
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "PersonAddressTypes",
            columns: table => new
            {
                Id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Description = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersonAddressTypes", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "PersonContactTypes",
            columns: table => new
            {
                Id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Description = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersonContactTypes", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "PersonContacts",
            columns: table => new
            {
                Id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                PersonId = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                TypeId = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Value = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersonContacts", x => x.Id);
                table.ForeignKey(
                    name: "FK_PersonContacts_People_PersonId",
                    column: x => x.PersonId,
                    principalTable: "People",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PersonContacts_PersonContactTypes_TypeId",
                    column: x => x.TypeId,
                    principalTable: "PersonContactTypes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_PersonAddresses_TypeId",
            table: "PersonAddresses",
            column: "TypeId");

        migrationBuilder.CreateIndex(
            name: "IX_PersonAddressTypes_Name",
            table: "PersonAddressTypes",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_PersonContacts_PersonId_TypeId_Value",
            table: "PersonContacts",
            columns: ["PersonId", "TypeId", "Value"],
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_PersonContacts_TypeId",
            table: "PersonContacts",
            column: "TypeId");

        migrationBuilder.CreateIndex(
            name: "IX_PersonContactTypes_Name",
            table: "PersonContactTypes",
            column: "Name",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_PersonAddresses_PersonAddressTypes_TypeId",
            table: "PersonAddresses",
            column: "TypeId",
            principalTable: "PersonAddressTypes",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
