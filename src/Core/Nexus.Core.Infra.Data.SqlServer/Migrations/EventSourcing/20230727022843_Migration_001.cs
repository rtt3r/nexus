﻿using Microsoft.EntityFrameworkCore.Migrations;


namespace Nexus.Core.Infra.Data.SqlServer.Migrations.EventSourcing;

/// <inheritdoc />
public partial class Migration_001 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "StoredEvents",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Timestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                AggregateId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                EventType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_StoredEvents", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "StoredEvents");
    }
}
