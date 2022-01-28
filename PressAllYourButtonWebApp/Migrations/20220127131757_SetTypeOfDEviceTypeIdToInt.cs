using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PressAllYourButtonWebApp.Migrations
{
    public partial class SetTypeOfDEviceTypeIdToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceType_DeviceType_id",
                table: "Devices");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceType_DeviceType_id",
                table: "Devices",
                column: "DeviceType_id",
                principalTable: "DeviceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceType_DeviceType_id",
                table: "Devices");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceType_DeviceType_id",
                table: "Devices",
                column: "DeviceType_id",
                principalTable: "DeviceType",
                principalColumn: "Id");
        }
    }
}
