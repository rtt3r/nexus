using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Core.Infra.Data.Migrations;

/// <inheritdoc />
public partial class _01 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Core");

        migrationBuilder.CreateTable(
            name: "Companies",
            schema: "Core",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                TaxId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                Active = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Companies", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Persons",
            schema: "Core",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                PersonType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                Active = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Persons", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "LegalEntities",
            schema: "Core",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                BrandName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                OpeningDate = table.Column<DateOnly>(type: "date", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LegalEntities", x => x.Id);
                table.ForeignKey(
                    name: "FK_LegalEntities_Persons_Id",
                    column: x => x.Id,
                    principalSchema: "Core",
                    principalTable: "Persons",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PersonAddresses",
            schema: "Core",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                PersonId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Type = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                ZipCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                Street = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                Number = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                Complement = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                Neighborhood = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                City = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                State = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                Country = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                Active = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersonAddresses", x => x.Id);
                table.ForeignKey(
                    name: "FK_PersonAddresses_Persons_PersonId",
                    column: x => x.PersonId,
                    principalSchema: "Core",
                    principalTable: "Persons",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PersonContacts",
            schema: "Core",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                PersonId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Type = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                LandlinePhone = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                MobilePhone = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                Whatsapp = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                Active = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersonContacts", x => x.Id);
                table.ForeignKey(
                    name: "FK_PersonContacts_Persons_PersonId",
                    column: x => x.PersonId,
                    principalSchema: "Core",
                    principalTable: "Persons",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PersonDocuments",
            schema: "Core",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                PersonId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Type = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                Number = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Active = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersonDocuments", x => x.Id);
                table.ForeignKey(
                    name: "FK_PersonDocuments_Persons_PersonId",
                    column: x => x.PersonId,
                    principalSchema: "Core",
                    principalTable: "Persons",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Companies",
            schema: "Core",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CompanyId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                HeadquartersId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CompanyType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                BranchCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Companies", x => x.Id);
                table.ForeignKey(
                    name: "FK_Companies_Companies_CompanyId",
                    column: x => x.CompanyId,
                    principalSchema: "Core",
                    principalTable: "Companies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Companies_Companies_HeadquartersId",
                    column: x => x.HeadquartersId,
                    principalSchema: "Core",
                    principalTable: "Companies",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Companies_LegalEntities_Id",
                    column: x => x.Id,
                    principalSchema: "Core",
                    principalTable: "LegalEntities",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserCompanies",
            schema: "Core",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                UserId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                CompanyId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                RoleInCompany = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserCompanies", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserCompanies_Companies_CompanyId",
                    column: x => x.CompanyId,
                    principalSchema: "Core",
                    principalTable: "Companies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Companies_TaxId",
            schema: "Core",
            table: "Companies",
            column: "TaxId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Companies_CompanyId",
            schema: "Core",
            table: "Companies",
            column: "CompanyId");

        migrationBuilder.CreateIndex(
            name: "IX_Companies_HeadquartersId",
            schema: "Core",
            table: "Companies",
            column: "HeadquartersId");

        migrationBuilder.CreateIndex(
            name: "IX_PersonAddresses_PersonId",
            schema: "Core",
            table: "PersonAddresses",
            column: "PersonId");

        migrationBuilder.CreateIndex(
            name: "IX_PersonContacts_PersonId",
            schema: "Core",
            table: "PersonContacts",
            column: "PersonId");

        migrationBuilder.CreateIndex(
            name: "IX_PersonDocuments_PersonId_Type_Number",
            schema: "Core",
            table: "PersonDocuments",
            columns: new[] { "PersonId", "Type", "Number" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UserCompanies_CompanyId",
            schema: "Core",
            table: "UserCompanies",
            column: "CompanyId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PersonAddresses",
            schema: "Core");

        migrationBuilder.DropTable(
            name: "PersonContacts",
            schema: "Core");

        migrationBuilder.DropTable(
            name: "PersonDocuments",
            schema: "Core");

        migrationBuilder.DropTable(
            name: "UserCompanies",
            schema: "Core");

        migrationBuilder.DropTable(
            name: "Companies",
            schema: "Core");

        migrationBuilder.DropTable(
            name: "Companies",
            schema: "Core");

        migrationBuilder.DropTable(
            name: "LegalEntities",
            schema: "Core");

        migrationBuilder.DropTable(
            name: "Persons",
            schema: "Core");
    }
}
