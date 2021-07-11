using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhantomAbyssServer.Migrations
{
    public partial class AddRunDateAndRemoveRequiredHashFromSavedRun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentUserId",
                table: "Routes");

            migrationBuilder.AlterColumn<string>(
                name: "DataHash",
                table: "SavedRuns",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "RunDateTime",
                table: "SavedRuns",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RunDateTime",
                table: "SavedRuns");

            migrationBuilder.AlterColumn<string>(
                name: "DataHash",
                table: "SavedRuns",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "CurrentUserId",
                table: "Routes",
                type: "INTEGER",
                nullable: true);
        }
    }
}
