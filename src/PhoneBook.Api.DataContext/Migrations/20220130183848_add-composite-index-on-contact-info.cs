using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneBook.Api.DataContext.Migrations
{
    public partial class addcompositeindexoncontactinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ContactInfos_ContactInfoType_ContactPersonId_Value",
                table: "ContactInfos",
                columns: new[] { "ContactInfoType", "ContactPersonId", "Value" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactInfos_ContactInfoType_ContactPersonId_Value",
                table: "ContactInfos");
        }
    }
}
