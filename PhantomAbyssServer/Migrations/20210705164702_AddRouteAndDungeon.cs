using Microsoft.EntityFrameworkCore.Migrations;

namespace PhantomAbyssServer.Migrations
{
    public partial class AddRouteAndDungeon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_CurrencyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserCurrency");

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompletedById = table.Column<uint>(type: "INTEGER", nullable: false),
                    RouteAttemptCount = table.Column<uint>(type: "INTEGER", nullable: false),
                    ShareCode = table.Column<string>(type: "TEXT", nullable: true),
                    WageredWhipId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_Users_CompletedById",
                        column: x => x.CompletedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dungeons",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompletedById = table.Column<uint>(type: "INTEGER", nullable: false),
                    NumFloors = table.Column<uint>(type: "INTEGER", nullable: false),
                    RouteStage = table.Column<uint>(type: "INTEGER", nullable: false),
                    RelicId = table.Column<string>(type: "TEXT", nullable: true),
                    RouteId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dungeons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dungeons_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dungeons_Users_CompletedById",
                        column: x => x.CompletedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CurrencyId",
                table: "Users",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Dungeons_CompletedById",
                table: "Dungeons",
                column: "CompletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Dungeons_RouteId",
                table: "Dungeons",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_CompletedById",
                table: "Routes",
                column: "CompletedById");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedRuns_Dungeons_DungeonId",
                table: "SavedRuns",
                column: "DungeonId",
                principalTable: "Dungeons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedRuns_Dungeons_DungeonId",
                table: "SavedRuns");

            migrationBuilder.DropTable(
                name: "Dungeons");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Users_CurrencyId",
                table: "Users");

            migrationBuilder.AddColumn<uint>(
                name: "UserId",
                table: "UserCurrency",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CurrencyId",
                table: "Users",
                column: "CurrencyId",
                unique: true);
        }
    }
}
