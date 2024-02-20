using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStoreApi.Migrations
{
    public partial class AddImagePathToAdvertisement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Advertisement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Advertisement");
        }
    }
}
