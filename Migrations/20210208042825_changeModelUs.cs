using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowersStore.Migrations
{
    public partial class changeModelUs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_AspNetUsers_UserId",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Basket",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_UserId",
                table: "Basket",
                newName: "IX_Basket_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_AspNetUsers_Id",
                table: "Basket",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_AspNetUsers_Id",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Basket",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_Id",
                table: "Basket",
                newName: "IX_Basket_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_AspNetUsers_UserId",
                table: "Basket",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
