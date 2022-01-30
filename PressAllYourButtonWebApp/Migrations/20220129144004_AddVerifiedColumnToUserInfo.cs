using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PressAllYourButtonWebApp.Migrations
{
    public partial class AddVerifiedColumnToUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "UserInfos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verified",
                table: "UserInfos");
        }
    }
}
