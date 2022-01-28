using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PressAllYourButtonWebApp.Migrations
{
    public partial class AddNicknameByUseColumnInDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NicknameByUser",
                table: "Devices",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NicknameByUser",
                table: "Devices");
        }
    }
}
