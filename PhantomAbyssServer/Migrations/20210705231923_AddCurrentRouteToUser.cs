using Microsoft.EntityFrameworkCore.Migrations;

namespace PhantomAbyssServer.Migrations
{
    public partial class AddCurrentRouteToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_CurrentUserId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_CurrentUserId",
                table: "Routes");

            migrationBuilder.AddColumn<uint>(
                name: "CurrentRouteId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CurrentRouteId",
                table: "Users",
                column: "CurrentRouteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Routes_CurrentRouteId",
                table: "Users",
                column: "CurrentRouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Routes_CurrentRouteId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CurrentRouteId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrentRouteId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_CurrentUserId",
                table: "Routes",
                column: "CurrentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Users_CurrentUserId",
                table: "Routes",
                column: "CurrentUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
