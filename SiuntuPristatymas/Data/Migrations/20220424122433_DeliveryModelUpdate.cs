using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiuntuPristatymas.Data.Migrations
{
    public partial class DeliveryModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_UserName",
                table: "Deliveries");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Deliveries",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_UserName",
                table: "Deliveries",
                newName: "IX_Deliveries_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_UserId",
                table: "Deliveries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_UserId",
                table: "Deliveries");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Deliveries",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_UserId",
                table: "Deliveries",
                newName: "IX_Deliveries_UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_UserName",
                table: "Deliveries",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
