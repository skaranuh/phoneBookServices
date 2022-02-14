﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PhoneBook.Report.Api.DataContext;

namespace PhoneBook.Report.Api.DataContext.Migrations
{
    [DbContext(typeof(PhoneBookReportDataContext))]
    [Migration("20220213202531_add-report-path")]
    partial class addreportpath
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("PhoneBook.Report.Api.Entities.Entities.ReportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CompletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ReportPath")
                        .HasColumnType("text");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}