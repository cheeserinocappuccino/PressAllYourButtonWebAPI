using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PressAllYourButtonWebApp.Migrations
{
    public partial class ChangeColumnTypeofActionTimeToDateTimeInConnectionAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionTime",
                table: "ConnectionAudits",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionTime",
                table: "ConnectionAudits",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");
        }
    }
}
