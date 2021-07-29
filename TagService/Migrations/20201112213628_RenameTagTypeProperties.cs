using Microsoft.EntityFrameworkCore.Migrations;

namespace TagService.Migrations
{
    public partial class RenameTagTypeProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TagGroupTypeName",
                table: "TagTypes",
                newName: "TagTypeName");

            migrationBuilder.RenameColumn(
                name: "TagGroupTypeId",
                table: "TagTypes",
                newName: "TagTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TagTypes_TagGroupTypeName",
                table: "TagTypes",
                newName: "IX_TagTypes_TagTypeName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TagTypeName",
                table: "TagTypes",
                newName: "TagGroupTypeName");

            migrationBuilder.RenameColumn(
                name: "TagTypeId",
                table: "TagTypes",
                newName: "TagGroupTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TagTypes_TagTypeName",
                table: "TagTypes",
                newName: "IX_TagTypes_TagGroupTypeName");
        }
    }
}
