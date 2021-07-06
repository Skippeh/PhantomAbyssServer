using Microsoft.EntityFrameworkCore.Migrations;

namespace PhantomAbyssServer.Migrations
{
    public partial class AddCurrentUserToRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShareCode",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "WageredWhipId",
                table: "Routes");

            migrationBuilder.AddColumn<uint>(
                name: "CurrentUserId",
                table: "Routes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateIndex(
                name: "IX_SavedRuns_RouteId",
                table: "SavedRuns",
                column: "RouteId");

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedRuns_Routes_RouteId",
                table: "SavedRuns",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_CurrentUserId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedRuns_Routes_RouteId",
                table: "SavedRuns");

            migrationBuilder.DropIndex(
                name: "IX_SavedRuns_RouteId",
                table: "SavedRuns");

            migrationBuilder.DropIndex(
                name: "IX_Routes_CurrentUserId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "CurrentUserId",
                table: "Routes");

            migrationBuilder.AddColumn<string>(
                name: "ShareCode",
                table: "Routes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WageredWhipId",
                table: "Routes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
