using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Services.Migrations
{
    public partial class setIdToBeAutoIncreamented : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Passes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

        
            migrationBuilder.AddUniqueConstraint(
                name: "IX_UniquePassIndex",
                table: "Passes",
                columns: new[] { "PassTypeIdentifier", "SerialNumber" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "IX_UniquePassIndex",
                table: "Passes");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Passes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

           
        }
    }
}
