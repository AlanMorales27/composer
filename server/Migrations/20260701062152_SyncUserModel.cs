using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class SyncUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Restaurants_RestaurantId",
                table: "Stations");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Restaurants_RestaurantId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "Users",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RestaurantId",
                table: "Users",
                newName: "IX_Users_AccountId");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "Stations",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_RestaurantId",
                table: "Stations",
                newName: "IX_Stations_AccountId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Restaurants_AccountId",
                table: "Stations",
                column: "AccountId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Restaurants_AccountId",
                table: "Users",
                column: "AccountId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Restaurants_AccountId",
                table: "Stations");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Restaurants_AccountId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Users",
                newName: "RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_AccountId",
                table: "Users",
                newName: "IX_Users_RestaurantId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Stations",
                newName: "RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_AccountId",
                table: "Stations",
                newName: "IX_Stations_RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Restaurants_RestaurantId",
                table: "Stations",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Restaurants_RestaurantId",
                table: "Users",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
