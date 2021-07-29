using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TagService.Migrations
{
    public partial class UseExistingTagsSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_TagTypes_TagTypeId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "ItemTags");

            migrationBuilder.DropTable(
                name: "TagTypes");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TagName",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TagTypeId_TagName",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagTypeId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "Taggings",
                columns: table => new
                {
                    TaggingId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ActiveFlag = table.Column<bool>(nullable: false, defaultValue: true),
                    TaggableType = table.Column<string>(nullable: false),
                    TaggableId = table.Column<long>(nullable: false),
                    TaggerId = table.Column<long>(nullable: true),
                    TaggerType = table.Column<string>(nullable: true),
                    Context = table.Column<string>(nullable: false),
                    TagId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taggings", x => x.TaggingId);
                    table.ForeignKey(
                        name: "FK_Taggings_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagName",
                table: "Tags",
                column: "TagName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Taggings_ActiveFlag",
                table: "Taggings",
                column: "ActiveFlag");

            migrationBuilder.CreateIndex(
                name: "IX_Taggings_TagId",
                table: "Taggings",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Taggings_TaggableId_TaggableType_Context",
                table: "Taggings",
                columns: new[] { "TaggableId", "TaggableType", "Context" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Taggings");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TagName",
                table: "Tags");

            migrationBuilder.AddColumn<long>(
                name: "TagTypeId",
                table: "Tags",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    ItemTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ItemTypeName = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.ItemTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TagTypes",
                columns: table => new
                {
                    TagTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TagTypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTypes", x => x.TagTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ForeignId = table.Column<long>(type: "bigint", nullable: false),
                    ItemTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "ItemTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemTags",
                columns: table => new
                {
                    ItemTagId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TagId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTags", x => x.ItemTagId);
                    table.ForeignKey(
                        name: "FK_ItemTags_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagName",
                table: "Tags",
                column: "TagName");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagTypeId_TagName",
                table: "Tags",
                columns: new[] { "TagTypeId", "TagName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ActiveFlag",
                table: "Items",
                column: "ActiveFlag");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ForeignId",
                table: "Items",
                column: "ForeignId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ForeignId_ItemTypeId",
                table: "Items",
                columns: new[] { "ForeignId", "ItemTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemTags_ActiveFlag",
                table: "ItemTags",
                column: "ActiveFlag");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTags_TagId",
                table: "ItemTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTags_ItemId_TagId",
                table: "ItemTags",
                columns: new[] { "ItemId", "TagId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_ActiveFlag",
                table: "ItemTypes",
                column: "ActiveFlag");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_ItemTypeName",
                table: "ItemTypes",
                column: "ItemTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagTypes_ActiveFlag",
                table: "TagTypes",
                column: "ActiveFlag");

            migrationBuilder.CreateIndex(
                name: "IX_TagTypes_TagTypeName",
                table: "TagTypes",
                column: "TagTypeName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_TagTypes_TagTypeId",
                table: "Tags",
                column: "TagTypeId",
                principalTable: "TagTypes",
                principalColumn: "TagTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
