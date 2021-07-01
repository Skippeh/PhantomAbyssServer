using Microsoft.EntityFrameworkCore.Migrations;

namespace PhantomAbyssServer.Migrations
{
    public partial class AddRunSuccessfulPropertyToSavedRun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RunSuccessful",
                table: "SavedRuns",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RunSuccessful",
                table: "SavedRuns");
        }
    }
}
