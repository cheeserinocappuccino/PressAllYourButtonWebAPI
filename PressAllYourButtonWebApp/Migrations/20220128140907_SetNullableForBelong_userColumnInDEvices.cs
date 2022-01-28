using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PressAllYourButtonWebApp.Migrations
{
    public partial class SetNullableForBelong_userColumnInDEvices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Devices_UserInfos_Belong_User",
                table: "Devices");*/

            migrationBuilder.AlterColumn<int>(
                name: "Belong_User",
                table: "Devices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_UserInfos_Belong_User",
                table: "Devices",
                column: "Belong_User",
                principalTable: "UserInfos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_UserInfos_Belong_User",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "Belong_User",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_UserInfos_Belong_User",
                table: "Devices",
                column: "Belong_User",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
