using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Data.Migrations
{
    public partial class OwnerUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerUserId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OwnerUserId",
                table: "Products",
                column: "OwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_OwnerUserId",
                table: "Products",
                column: "OwnerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_OwnerUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OwnerUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "Products");
        }
    }
}
