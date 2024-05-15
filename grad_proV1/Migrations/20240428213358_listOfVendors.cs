using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gradproV1.Migrations
{
    /// <inheritdoc />
    public partial class listOfVendors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vendors_categories_CategoryId",
                table: "vendors");

            migrationBuilder.AddForeignKey(
                name: "FK_vendors_categories_CategoryId",
                table: "vendors",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vendors_categories_CategoryId",
                table: "vendors");

            migrationBuilder.AddForeignKey(
                name: "FK_vendors_categories_CategoryId",
                table: "vendors",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
