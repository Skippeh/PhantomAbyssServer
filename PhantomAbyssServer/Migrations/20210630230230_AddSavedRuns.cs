using Microsoft.EntityFrameworkCore.Migrations;

namespace PhantomAbyssServer.Migrations
{
    public partial class AddSavedRuns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedRuns",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<uint>(type: "INTEGER", nullable: false),
                    DungeonId = table.Column<uint>(type: "INTEGER", nullable: false),
                    RouteId = table.Column<uint>(type: "INTEGER", nullable: false),
                    DungeonFloorNumber = table.Column<uint>(type: "INTEGER", nullable: false),
                    DataHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedRuns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedRuns_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedRuns_DataHash",
                table: "SavedRuns",
                column: "DataHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavedRuns_UserId",
                table: "SavedRuns",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedRuns");
        }
    }
}
