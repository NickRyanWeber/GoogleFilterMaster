using Microsoft.EntityFrameworkCore.Migrations;

namespace sdgreacttemplate.Migrations
{
    public partial class AddedPrimaryKeyForAccountsCacheAndFiltersCache : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountsCacheId",
                table: "FiltersCache",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiltersCache_AccountsCacheId",
                table: "FiltersCache",
                column: "AccountsCacheId");

            migrationBuilder.AddForeignKey(
                name: "FK_FiltersCache_AccountsCache_AccountsCacheId",
                table: "FiltersCache",
                column: "AccountsCacheId",
                principalTable: "AccountsCache",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FiltersCache_AccountsCache_AccountsCacheId",
                table: "FiltersCache");

            migrationBuilder.DropIndex(
                name: "IX_FiltersCache_AccountsCacheId",
                table: "FiltersCache");

            migrationBuilder.DropColumn(
                name: "AccountsCacheId",
                table: "FiltersCache");
        }
    }
}
