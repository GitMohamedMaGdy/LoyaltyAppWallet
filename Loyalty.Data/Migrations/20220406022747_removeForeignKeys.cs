using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Services.Migrations
{
    public partial class removeForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex
                (
                name: "IX_Registrations_PassTypeIdentifier",
                table: "Registrations"
                );
        }
         

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Registrations_PassTypeIdentifier1",
                table: "Registrations");

          
        }
    }
}
