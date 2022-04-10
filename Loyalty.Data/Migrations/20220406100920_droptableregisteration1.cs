using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Services.Migrations
{
    public partial class droptableregisteration1 : Migration
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
                    DeviceLibraryIdentifier = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PassTypeIdentifier = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeviceLibraryIdentifier1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PassTypeIdentifier1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PushToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => new { x.DeviceLibraryIdentifier, x.PassTypeIdentifier });
                    table.ForeignKey(
                        name: "FK_Registrations_Devices_DeviceLibraryIdentifier1",
                        column: x => x.DeviceLibraryIdentifier1,
                        principalTable: "Devices",
                        principalColumn: "DeviceLibraryIdentifier",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registrations_Passes_PassTypeIdentifier1",
                        column: x => x.PassTypeIdentifier1,
                        principalTable: "Passes",
                        principalColumn: "PassTypeIdentifier",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_DeviceLibraryIdentifier1",
                table: "Registrations",
                column: "DeviceLibraryIdentifier1");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_PassTypeIdentifier1",
                table: "Registrations",
                column: "PassTypeIdentifier1");
        }
    }
}
