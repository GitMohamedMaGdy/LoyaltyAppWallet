using Microsoft.EntityFrameworkCore.Migrations;

namespace Loyalty.Services.Migrations
{
    public partial class FixRealtionsbetweenTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Registrations");

            migrationBuilder.AlterColumn<string>(
                name: "PassTypeIdentifier",
                table: "Registrations",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceLibraryIdentifier",
                table: "Registrations",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                columns: new[] { "DeviceLibraryIdentifier", "PassTypeIdentifier" });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_PassTypeIdentifier",
                table: "Registrations",
                column: "PassTypeIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Devices_DeviceLibraryIdentifier",
                table: "Registrations",
                column: "DeviceLibraryIdentifier",
                principalTable: "Devices",
                principalColumn: "DeviceLibraryIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Passes_PassTypeIdentifier",
                table: "Registrations",
                column: "PassTypeIdentifier",
                principalTable: "Passes",
                principalColumn: "PassTypeIdentifier");
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

            migrationBuilder.DropIndex(
                name: "IX_Registrations_PassTypeIdentifier",
                table: "Registrations");

            migrationBuilder.AlterColumn<string>(
                name: "PassTypeIdentifier",
                table: "Registrations",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceLibraryIdentifier",
                table: "Registrations",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Registrations",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                column: "Id");
        }
    }
}
