using Microsoft.EntityFrameworkCore.Migrations;

namespace TagService.Migrations
{
    public partial class AddFeatureId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FeatureId",
                table: "Taggings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeatureId",
                table: "Taggings");
        }
    }
}
