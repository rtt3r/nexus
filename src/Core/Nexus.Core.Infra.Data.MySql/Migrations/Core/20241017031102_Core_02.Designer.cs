﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nexus.Core.Infra.Data.MySql;

#nullable disable

namespace Nexus.Core.Infra.Data.MySql.Migrations.Core
{
    [DbContext(typeof(MySqlCoreDbContext))]
    [Migration("20241017031102_Core_02")]
    partial class Core_02
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Nexus.Core.Domain.Customers.Aggregates.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.Person", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<int>("Type")
                        .HasMaxLength(7)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("People", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.PersonAddress", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Complement")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Number")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("PersonId")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("TypeId");

                    b.ToTable("PersonAddresses", (string)null);
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.PersonAddressType", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PersonAddressTypes", (string)null);
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.PersonContact", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("PersonId")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.HasIndex("PersonId", "TypeId", "Value")
                        .IsUnique();

                    b.ToTable("PersonContacts", (string)null);
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.PersonContactType", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PersonContactTypes", (string)null);
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.PersonDocument", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime?>("IssuedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Issuer")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("PersonId")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime?>("ValidUntil")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.HasIndex("TypeId");

                    b.HasIndex("PersonId", "TypeId", "Number")
                        .IsUnique();

                    b.ToTable("PersonDocuments", (string)null);
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.PersonDocumentType", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PersonDocumentTypes", (string)null);
                });

            modelBuilder.Entity("Nexus.Core.Domain.Users.Aggregates.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Avatar")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.LegalPerson", b =>
                {
                    b.HasBaseType("Nexus.Core.Domain.People.Aggregates.Person");

                    b.Property<DateOnly>("OpenedAt")
                        .HasColumnType("date");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "Nexus.Core.Domain.People.Aggregates.LegalPerson.Name#LegalPersonName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("BrandName")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("varchar(128)")
                                .HasColumnName("BrandName");

                            b1.Property<string>("CorporateName")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)")
                                .HasColumnName("CorporateName");
                        });

                    b.ToTable("LegalPeople", (string)null);
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.NaturalPerson", b =>
                {
                    b.HasBaseType("Nexus.Core.Domain.People.Aggregates.Person");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "Nexus.Core.Domain.People.Aggregates.NaturalPerson.Name#NaturalPersonName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("varchar(128)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("varchar(128)")
                                .HasColumnName("LastName");
                        });

                    b.ToTable("NaturalPeople", (string)null);
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.PersonAddress", b =>
                {
                    b.HasOne("Nexus.Core.Domain.People.Aggregates.Person", "Person")
                        .WithMany("Addresses")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nexus.Core.Domain.People.Aggregates.PersonAddressType", "Type")
                        .WithMany("Addresses")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.PersonContact", b =>
                {
                    b.HasOne("Nexus.Core.Domain.People.Aggregates.Person", "Person")
                        .WithMany("Contacts")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nexus.Core.Domain.People.Aggregates.PersonContactType", "Type")
                        .WithMany("Contacts")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.PersonDocument", b =>
                {
                    b.HasOne("Nexus.Core.Domain.People.Aggregates.Person", "Person")
                        .WithMany("Documents")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nexus.Core.Domain.People.Aggregates.PersonDocumentType", "Type")
                        .WithMany("Documents")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.LegalPerson", b =>
                {
                    b.HasOne("Nexus.Core.Domain.People.Aggregates.Person", null)
                        .WithOne()
                        .HasForeignKey("Nexus.Core.Domain.People.Aggregates.LegalPerson", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.NaturalPerson", b =>
                {
                    b.HasOne("Nexus.Core.Domain.People.Aggregates.Person", null)
                        .WithOne()
                        .HasForeignKey("Nexus.Core.Domain.People.Aggregates.NaturalPerson", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.Person", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Contacts");

                    b.Navigation("Documents");
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.PersonAddressType", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.PersonContactType", b =>
                {
                    b.Navigation("Contacts");
                });

            modelBuilder.Entity("Nexus.Core.Domain.People.Aggregates.PersonDocumentType", b =>
                {
                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
