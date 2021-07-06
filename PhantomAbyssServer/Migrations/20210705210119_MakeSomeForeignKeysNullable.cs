using Microsoft.EntityFrameworkCore.Migrations;

namespace PhantomAbyssServer.Migrations
{
    public partial class MakeSomeForeignKeysNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dungeons_Users_CompletedById",
                table: "Dungeons");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_CompletedById",
                table: "Routes");

            migrationBuilder.AlterColumn<uint>(
                name: "CompletedById",
                table: "Routes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<uint>(
                name: "CompletedById",
                table: "Dungeons",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Dungeons_Users_CompletedById",
                table: "Dungeons",
                column: "CompletedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Users_CompletedById",
                table: "Routes",
                column: "CompletedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dungeons_Users_CompletedById",
                table: "Dungeons");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_CompletedById",
                table: "Routes");

            migrationBuilder.AlterColumn<uint>(
                name: "CompletedById",
                table: "Routes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "CompletedById",
                table: "Dungeons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dungeons_Users_CompletedById",
                table: "Dungeons",
                column: "CompletedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Users_CompletedById",
                table: "Routes",
                column: "CompletedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
