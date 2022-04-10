using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Services.Migrations
{
    public partial class addregisterationtable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceLibraryIdentifier = table.Column<string>(maxLength: 450, nullable: true),
                    PassTypeIdentifier = table.Column<string>(maxLength: 450, nullable: true),
                    SerialNumber = table.Column<string>(nullable: false),
                    PushToken = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_Devices_DeviceLibraryIdentifier",
                        column: x => x.DeviceLibraryIdentifier,
                        principalTable: "Devices",
                        principalColumn: "DeviceLibraryIdentifier",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registrations_Passes_PassTypeIdentifier",
                        column: x => x.PassTypeIdentifier,
                        principalTable: "Passes",
                        principalColumn: "PassTypeIdentifier",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_DeviceLibraryIdentifier",
                table: "Registrations",
                column: "DeviceLibraryIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_PassTypeIdentifier",
                table: "Registrations",
                column: "PassTypeIdentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");
        }
    }
}
