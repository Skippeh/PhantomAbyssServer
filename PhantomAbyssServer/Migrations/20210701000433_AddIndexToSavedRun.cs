using Microsoft.EntityFrameworkCore.Migrations;

namespace PhantomAbyssServer.Migrations
{
    public partial class AddIndexToSavedRun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SavedRuns_DungeonId_RouteId_DungeonFloorNumber",
                table: "SavedRuns",
                columns: new[] { "DungeonId", "RouteId", "DungeonFloorNumber" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SavedRuns_DungeonId_RouteId_DungeonFloorNumber",
                table: "SavedRuns");
        }
    }
}
