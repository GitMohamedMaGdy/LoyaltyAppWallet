using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Services.Migrations
{
    public partial class deleteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceLibraryIdentifier = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    PassTypeIdentifier = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    PushToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                });
        }
    }
}
