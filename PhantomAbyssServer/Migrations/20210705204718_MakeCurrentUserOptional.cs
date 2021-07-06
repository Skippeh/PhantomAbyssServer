using Microsoft.EntityFrameworkCore.Migrations;

namespace PhantomAbyssServer.Migrations
{
    public partial class MakeCurrentUserOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_CurrentUserId",
                table: "Routes");

            migrationBuilder.AlterColumn<uint>(
                name: "CurrentUserId",
                table: "Routes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Users_CurrentUserId",
                table: "Routes",
                column: "CurrentUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_CurrentUserId",
                table: "Routes");

            migrationBuilder.AlterColumn<uint>(
                name: "CurrentUserId",
                table: "Routes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Users_CurrentUserId",
                table: "Routes",
                column: "CurrentUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
