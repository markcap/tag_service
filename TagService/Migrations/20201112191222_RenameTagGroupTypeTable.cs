using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TagService.Migrations
{
    public partial class RenameTagGroupTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagGroupTypes");

            migrationBuilder.CreateTable(
                name: "TagTypes",
                columns: table => new
                {
                    TagGroupTypeId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ActiveFlag = table.Column<bool>(nullable: false),
                    TagGroupTypeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTypes", x => x.TagGroupTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagTypes_ActiveFlag",
                table: "TagTypes",
                column: "ActiveFlag");

            migrationBuilder.CreateIndex(
                name: "IX_TagTypes_TagGroupTypeName",
                table: "TagTypes",
                column: "TagGroupTypeName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagTypes");

            migrationBuilder.CreateTable(
                name: "TagGroupTypes",
                columns: table => new
                {
                    TagGroupTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TagGroupTypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagGroupTypes", x => x.TagGroupTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagGroupTypes_ActiveFlag",
                table: "TagGroupTypes",
                column: "ActiveFlag");

            migrationBuilder.CreateIndex(
                name: "IX_TagGroupTypes_TagGroupTypeName",
                table: "TagGroupTypes",
                column: "TagGroupTypeName",
                unique: true);
        }
    }
}
