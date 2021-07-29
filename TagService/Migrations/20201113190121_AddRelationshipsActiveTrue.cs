using Microsoft.EntityFrameworkCore.Migrations;

namespace TagService.Migrations
{
    public partial class AddRelationshipsActiveTrue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tags_TagGroupId_TagName",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagGroupId",
                table: "Tags");

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveFlag",
                table: "TagTypes",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveFlag",
                table: "Tags",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<long>(
                name: "TagTypeId",
                table: "Tags",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveFlag",
                table: "ItemTypes",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveFlag",
                table: "ItemTags",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveFlag",
                table: "Items",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagTypeId_TagName",
                table: "Tags",
                columns: new[] { "TagTypeId", "TagName" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_TagTypes_TagTypeId",
                table: "Tags",
                column: "TagTypeId",
                principalTable: "TagTypes",
                principalColumn: "TagTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_TagTypes_TagTypeId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TagTypeId_TagName",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagTypeId",
                table: "Tags");

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveFlag",
                table: "TagTypes",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveFlag",
                table: "Tags",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AddColumn<long>(
                name: "TagGroupId",
                table: "Tags",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveFlag",
                table: "ItemTypes",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveFlag",
                table: "ItemTags",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveFlag",
                table: "Items",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagGroupId_TagName",
                table: "Tags",
                columns: new[] { "TagGroupId", "TagName" },
                unique: true);
        }
    }
}
