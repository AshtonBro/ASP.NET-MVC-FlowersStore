using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowersStore.DataAccess.MSSQL.Migrations
{
    public partial class newInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopingCarts_Baskets_BasketId",
                table: "ShopingCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopingCarts_Products_ProductId",
                table: "ShopingCarts");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopingCarts_Baskets_BasketId",
                table: "ShopingCarts",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopingCarts_Products_ProductId",
                table: "ShopingCarts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopingCarts_Baskets_BasketId",
                table: "ShopingCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopingCarts_Products_ProductId",
                table: "ShopingCarts");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopingCarts_Baskets_BasketId",
                table: "ShopingCarts",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopingCarts_Products_ProductId",
                table: "ShopingCarts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
