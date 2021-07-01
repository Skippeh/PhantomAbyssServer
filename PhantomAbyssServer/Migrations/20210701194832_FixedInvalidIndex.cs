using Microsoft.EntityFrameworkCore.Migrations;

namespace PhantomAbyssServer.Migrations
{
    public partial class FixedInvalidIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DungeonLayouts_DungeonId_DungeonVersion_DungeonFloorCount",
                table: "DungeonLayouts");

            migrationBuilder.CreateIndex(
                name: "IX_DungeonLayouts_DungeonId_DungeonVersion_DungeonFloorNumber",
                table: "DungeonLayouts",
                columns: new[] { "DungeonId", "DungeonVersion", "DungeonFloorNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DungeonLayouts_DungeonId_DungeonVersion_DungeonFloorNumber",
                table: "DungeonLayouts");

            migrationBuilder.CreateIndex(
                name: "IX_DungeonLayouts_DungeonId_DungeonVersion_DungeonFloorCount",
                table: "DungeonLayouts",
                columns: new[] { "DungeonId", "DungeonVersion", "DungeonFloorCount" },
                unique: true);
        }
    }
}
