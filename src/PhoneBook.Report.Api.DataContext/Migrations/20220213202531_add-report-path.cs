using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneBook.Report.Api.DataContext.Migrations
{
    public partial class addreportpath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDate",
                table: "Reports",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportPath",
                table: "Reports",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedDate",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReportPath",
                table: "Reports");
        }
    }
}
