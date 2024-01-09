﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nexus.Core.Infra.Data.MySql;

#nullable disable

namespace Nexus.Core.Infra.Data.MySql.Migrations.EventSourcing
{
    [DbContext(typeof(MySqlEventSourcingDbContext))]
    partial class MySqlEventSourcingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Nexus.Core.Infra.Data.EventSourcing.StoredEvent", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AggregateId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("StoredEvents", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
