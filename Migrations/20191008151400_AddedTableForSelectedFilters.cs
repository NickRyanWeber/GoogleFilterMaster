using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace sdgreacttemplate.Migrations
{
    public partial class AddedTableForSelectedFilters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelectedFilter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    GoogleAccountId = table.Column<string>(nullable: true),
                    GoogleFilterId = table.Column<string>(nullable: true),
                    GoogleAccountName = table.Column<string>(nullable: true),
                    GoogleFilterName = table.Column<string>(nullable: true),
                    MasterFilterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedFilter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedFilter_MasterFilter_MasterFilterId",
                        column: x => x.MasterFilterId,
                        principalTable: "MasterFilter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedFilter_MasterFilterId",
                table: "SelectedFilter",
                column: "MasterFilterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedFilter");
        }
    }
}
