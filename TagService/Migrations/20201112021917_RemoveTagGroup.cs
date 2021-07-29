using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TagService.Migrations
{
    public partial class RemoveTagGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_TagGroups_TagGroupId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "TagGroups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TagGroups",
                columns: table => new
                {
                    TagGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RequiredTagId = table.Column<long>(type: "bigint", nullable: true),
                    TagGroupName = table.Column<string>(type: "text", nullable: false),
                    TagGroupTypeId = table.Column<long>(type: "bigint", nullable: false)
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
                name: "IX_TagGroups_ActiveFlag",
                table: "TagGroups",
                column: "ActiveFlag");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_TagGroups_TagGroupId",
                table: "Tags",
                column: "TagGroupId",
                principalTable: "TagGroups",
                principalColumn: "TagGroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
