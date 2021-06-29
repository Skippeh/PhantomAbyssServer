using Microsoft.EntityFrameworkCore.Migrations;

namespace PhantomAbyssServer.Migrations
{
    public partial class AddUserIndicesAndHealth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "HealthId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateTable(
                name: "UserHealth",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<uint>(type: "INTEGER", nullable: false),
                    BaseHealth = table.Column<uint>(type: "INTEGER", nullable: false),
                    BonusHealth = table.Column<uint>(type: "INTEGER", nullable: false),
                    MaxBonusHealth = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHealth", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_HealthId",
                table: "Users",
                column: "HealthId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_SharerId",
                table: "Users",
                column: "SharerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserHealth_HealthId",
                table: "Users",
                column: "HealthId",
                principalTable: "UserHealth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserHealth_HealthId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserHealth");

            migrationBuilder.DropIndex(
                name: "IX_Users_HealthId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SharerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HealthId",
                table: "Users");
        }
    }
}
