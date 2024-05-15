using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gradproV1.Migrations
{
    /// <inheritdoc />
    public partial class adimgUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "vendors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "vendors");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "categories");
        }
    }
}
