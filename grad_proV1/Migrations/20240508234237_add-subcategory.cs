using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gradproV1.Migrations
{
    /// <inheritdoc />
    public partial class addsubcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vendors_categories_CategoryId",
                table: "vendors");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "vendors",
                newName: "subcategoyId");

            migrationBuilder.RenameIndex(
                name: "IX_vendors_CategoryId",
                table: "vendors",
                newName: "IX_vendors_subcategoyId");

            migrationBuilder.AddForeignKey(
                name: "FK_vendors_subCategories_subcategoyId",
                table: "vendors",
                column: "subcategoyId",
                principalTable: "subCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vendors_subCategories_subcategoyId",
                table: "vendors");

            migrationBuilder.RenameColumn(
                name: "subcategoyId",
                table: "vendors",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_vendors_subcategoyId",
                table: "vendors",
                newName: "IX_vendors_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_vendors_categories_CategoryId",
                table: "vendors",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
