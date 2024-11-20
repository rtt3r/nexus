﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Finance.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class _02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Id_UserId",
                schema: "Finance",
                table: "Accounts",
                columns: new[] { "Id", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_Id_UserId",
                schema: "Finance",
                table: "Accounts");
        }
    }
}
