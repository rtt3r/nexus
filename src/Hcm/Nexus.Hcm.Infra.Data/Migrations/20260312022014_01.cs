using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Hcm.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class _01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    PersonType = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentAttributes",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DocumentId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentAttributes_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "Core",
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NaturalPeople",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Gender = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalPeople", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaturalPeople_People_Id",
                        column: x => x.Id,
                        principalSchema: "Core",
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonAddresses",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    PersonId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ZipCode = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Street = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Number = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Complement = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Neighborhood = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    City = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    State = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Country = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonAddresses_People_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Core",
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonContacts",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    PersonId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    LandlinePhone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    MobilePhone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Whatsapp = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonContacts_People_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Core",
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonDocuments",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    PersonId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DocumentId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Value = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "Core",
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonDocuments_People_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Core",
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonDocumentAttributes",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DocumentId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    AttributeId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Value = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDocumentAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonDocumentAttributes_DocumentAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "Core",
                        principalTable: "DocumentAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonDocumentAttributes_PersonDocuments_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "Core",
                        principalTable: "PersonDocuments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAttributes_DocumentId",
                schema: "Core",
                table: "DocumentAttributes",
                column: "DocumentId");

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
                name: "IX_PersonDocumentAttributes_AttributeId",
                schema: "Core",
                table: "PersonDocumentAttributes",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDocumentAttributes_DocumentId",
                schema: "Core",
                table: "PersonDocumentAttributes",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDocuments_DocumentId",
                schema: "Core",
                table: "PersonDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDocuments_PersonId_DocumentId_Value",
                schema: "Core",
                table: "PersonDocuments",
                columns: new[] { "PersonId", "DocumentId", "Value" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NaturalPeople",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "PersonAddresses",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "PersonContacts",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "PersonDocumentAttributes",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "DocumentAttributes",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "PersonDocuments",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "People",
                schema: "Core");
        }
    }
}
