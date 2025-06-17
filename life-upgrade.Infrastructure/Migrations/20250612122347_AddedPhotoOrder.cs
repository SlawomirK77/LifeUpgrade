using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeUpgrade.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhotoOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Photos");
        }
    }
}
