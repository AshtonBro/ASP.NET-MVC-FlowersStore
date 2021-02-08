using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowersStore.Migrations
{
    public partial class UserUniq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_Name",
                table: "User",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Name",
                table: "User");
        }
    }
}
