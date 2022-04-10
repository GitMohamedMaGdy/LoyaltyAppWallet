using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Services.Migrations
{
    public partial class createDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceLibraryIdentifier = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    PushToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceLibraryIdentifier);
                });

            migrationBuilder.CreateTable(
                name: "Passes",
                columns: table => new
                {
                    PassTypeIdentifier = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    LastUpdatedTag = table.Column<DateTime>(nullable: false),
                    AuthenticationToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passes", x => x.PassTypeIdentifier);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassTypeIdentifier = table.Column<string>(nullable: true),
                    DeviceLibraryIdentifier = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    PushToken = table.Column<string>(nullable: true)
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

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Passes");
        }
    }
}
