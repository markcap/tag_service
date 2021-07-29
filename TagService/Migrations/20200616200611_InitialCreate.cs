using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TagService.Migrations
{
  public partial class InitialCreate : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "ItemTypes",
          columns: table => new
          {
            ItemTypeId = table.Column<long>(nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            ItemTypeName = table.Column<string>(nullable: false),
            Active = table.Column<bool>(nullable: false),
            CreatedAt = table.Column<DateTime>(nullable: false),
            CreatedBy = table.Column<string>(nullable: true),
            LastUpdatedAt = table.Column<DateTime>(nullable: true),
            LastUpdatedBy = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_ItemTypes", x => x.ItemTypeId);
          });

      migrationBuilder.CreateTable(
          name: "TagGroupTypes",
          columns: table => new
          {
            TagGroupTypeId = table.Column<long>(nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            TagGroupTypeName = table.Column<string>(nullable: false),
            Active = table.Column<bool>(nullable: false),
            CreatedAt = table.Column<DateTime>(nullable: false),
            CreatedBy = table.Column<string>(nullable: true),
            LastUpdatedAt = table.Column<DateTime>(nullable: true),
            LastUpdatedBy = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_TagGroupTypes", x => x.TagGroupTypeId);
          });

      migrationBuilder.CreateTable(
          name: "Items",
          columns: table => new
          {
            ItemId = table.Column<long>(nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            ForeignId = table.Column<long>(nullable: false),
            ItemTypeId = table.Column<long>(nullable: false),
            Active = table.Column<bool>(nullable: false),
            CreatedAt = table.Column<DateTime>(nullable: false),
            CreatedBy = table.Column<string>(nullable: true),
            LastUpdatedAt = table.Column<DateTime>(nullable: true),
            LastUpdatedBy = table.Column<string>(nullable: true)
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
            ItemTagId = table.Column<long>(nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            ItemId = table.Column<long>(nullable: false),
            TagId = table.Column<long>(nullable: false),
            Active = table.Column<bool>(nullable: false),
            CreatedAt = table.Column<DateTime>(nullable: false),
            CreatedBy = table.Column<string>(nullable: true),
            LastUpdatedAt = table.Column<DateTime>(nullable: true),
            LastUpdatedBy = table.Column<string>(nullable: true)
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
          });

      migrationBuilder.CreateTable(
          name: "Tags",
          columns: table => new
          {
            TagId = table.Column<long>(nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            TagName = table.Column<string>(nullable: false),
            TagGroupId = table.Column<long>(nullable: false),
            Active = table.Column<bool>(nullable: false),
            CreatedAt = table.Column<DateTime>(nullable: false),
            CreatedBy = table.Column<string>(nullable: true),
            LastUpdatedAt = table.Column<DateTime>(nullable: true),
            LastUpdatedBy = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Tags", x => x.TagId);
          });

      migrationBuilder.CreateTable(
          name: "TagGroups",
          columns: table => new
          {
            TagGroupId = table.Column<long>(nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            TagGroupName = table.Column<string>(nullable: false),
            TagGroupTypeId = table.Column<long>(nullable: false),
            RequiredTagId = table.Column<long>(nullable: true),
            Active = table.Column<bool>(nullable: false),
            CreatedAt = table.Column<DateTime>(nullable: false),
            CreatedBy = table.Column<string>(nullable: true),
            LastUpdatedAt = table.Column<DateTime>(nullable: true),
            LastUpdatedBy = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_TagGroups", x => x.TagGroupId);
            table.ForeignKey(
                      name: "FK_TagGroups_Tags_RequiredTagId",
                      column: x => x.RequiredTagId,
                      principalTable: "Tags",
                      principalColumn: "TagId",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_TagGroups_TagGroupTypes_TagGroupTypeId",
                      column: x => x.TagGroupTypeId,
                      principalTable: "TagGroupTypes",
                      principalColumn: "TagGroupTypeId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Items_Active",
          table: "Items",
          column: "Active");

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
          name: "IX_ItemTags_Active",
          table: "ItemTags",
          column: "Active");

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
          name: "IX_ItemTypes_Active",
          table: "ItemTypes",
          column: "Active");

      migrationBuilder.CreateIndex(
          name: "IX_ItemTypes_ItemTypeName",
          table: "ItemTypes",
          column: "ItemTypeName",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_TagGroups_Active",
          table: "TagGroups",
          column: "Active");

      migrationBuilder.CreateIndex(
          name: "IX_TagGroups_RequiredTagId",
          table: "TagGroups",
          column: "RequiredTagId");

      migrationBuilder.CreateIndex(
          name: "IX_TagGroups_TagGroupName",
          table: "TagGroups",
          column: "TagGroupName");

      migrationBuilder.CreateIndex(
          name: "IX_TagGroups_TagGroupTypeId_TagGroupName",
          table: "TagGroups",
          columns: new[] { "TagGroupTypeId", "TagGroupName" },
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_TagGroupTypes_Active",
          table: "TagGroupTypes",
          column: "Active");

      migrationBuilder.CreateIndex(
          name: "IX_TagGroupTypes_TagGroupTypeName",
          table: "TagGroupTypes",
          column: "TagGroupTypeName",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_Tags_Active",
          table: "Tags",
          column: "Active");

      migrationBuilder.CreateIndex(
          name: "IX_Tags_TagName",
          table: "Tags",
          column: "TagName");

      migrationBuilder.CreateIndex(
          name: "IX_Tags_TagGroupId_TagName",
          table: "Tags",
          columns: new[] { "TagGroupId", "TagName" },
          unique: true);

      migrationBuilder.AddForeignKey(
          name: "FK_ItemTags_Tags_TagId",
          table: "ItemTags",
          column: "TagId",
          principalTable: "Tags",
          principalColumn: "TagId",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_Tags_TagGroups_TagGroupId",
          table: "Tags",
          column: "TagGroupId",
          principalTable: "TagGroups",
          principalColumn: "TagGroupId",
          onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_TagGroups_Tags_RequiredTagId",
          table: "TagGroups");

      migrationBuilder.DropTable(
          name: "ItemTags");

      migrationBuilder.DropTable(
          name: "Items");

      migrationBuilder.DropTable(
          name: "ItemTypes");

      migrationBuilder.DropTable(
          name: "Tags");

      migrationBuilder.DropTable(
          name: "TagGroups");

      migrationBuilder.DropTable(
          name: "TagGroupTypes");
    }
  }
}
