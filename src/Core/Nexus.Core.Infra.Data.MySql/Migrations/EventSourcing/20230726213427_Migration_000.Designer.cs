﻿// <auto-generated />
using Nexus.Core.Infra.Data.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Nexus.Core.Infra.Data.MySql.Migrations.EventSourcing
{
    [DbContext(typeof(MySqlEventSourcingDbContext))]
    [Migration("20230726213427_Migration_000")]
    partial class Migration_000
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);
#pragma warning restore 612, 618
        }
    }
}
