using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Services.Migrations
{
    public partial class addCompsiteKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Devices_DeviceLibraryIdentifier",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Passes_PassTypeIdentifier",
                table: "Registrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_DeviceLibraryIdentifier",
                table: "Registrations");

            migrationBuilder.AlterColumn<string>(
                name: "PassTypeIdentifier",
                table: "Registrations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceLibraryIdentifier",
                table: "Registrations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                columns: new[] { "DeviceLibraryIdentifier", "PassTypeIdentifier" });

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Devices_DeviceLibraryIdentifier",
                table: "Registrations",
                column: "DeviceLibraryIdentifier",
                principalTable: "Devices",
                principalColumn: "DeviceLibraryIdentifier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Passes_PassTypeIdentifier",
                table: "Registrations",
                column: "PassTypeIdentifier",
                principalTable: "Passes",
                principalColumn: "PassTypeIdentifier",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Devices_DeviceLibraryIdentifier",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Passes_PassTypeIdentifier",
                table: "Registrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

         

            migrationBuilder.AlterColumn<string>(
                name: "PassTypeIdentifier",
                table: "Registrations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DeviceLibraryIdentifier",
                table: "Registrations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_DeviceLibraryIdentifier",
                table: "Registrations",
                column: "DeviceLibraryIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Devices_DeviceLibraryIdentifier",
                table: "Registrations",
                column: "DeviceLibraryIdentifier",
                principalTable: "Devices",
                principalColumn: "DeviceLibraryIdentifier",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Passes_PassTypeIdentifier",
                table: "Registrations",
                column: "PassTypeIdentifier",
                principalTable: "Passes",
                principalColumn: "PassTypeIdentifier",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
