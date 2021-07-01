using Microsoft.EntityFrameworkCore.Migrations;

namespace PhantomAbyssServer.Migrations
{
    public partial class AddDungeonLayoutTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DungeonLayouts",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DungeonId = table.Column<uint>(type: "INTEGER", nullable: false),
                    DungeonVersion = table.Column<uint>(type: "INTEGER", nullable: false),
                    DungeonFloorCount = table.Column<uint>(type: "INTEGER", nullable: false),
                    DungeonFloorNumber = table.Column<uint>(type: "INTEGER", nullable: false),
                    DungeonFloorType = table.Column<uint>(type: "INTEGER", nullable: false),
                    DungeonLayoutType = table.Column<uint>(type: "INTEGER", nullable: false),
                    PermanentSettingsData = table.Column<string>(type: "TEXT", nullable: false),
                    RelicId = table.Column<string>(type: "TEXT", nullable: false),
                    LayoutDataHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DungeonLayouts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DungeonLayouts_DungeonId_DungeonVersion_DungeonFloorCount",
                table: "DungeonLayouts",
                columns: new[] { "DungeonId", "DungeonVersion", "DungeonFloorCount" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DungeonLayouts_LayoutDataHash",
                table: "DungeonLayouts",
                column: "LayoutDataHash",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DungeonLayouts");
        }
    }
}
