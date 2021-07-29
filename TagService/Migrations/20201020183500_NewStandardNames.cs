using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TagService.Migrations
{
    public partial class NewStandardNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Tags_Active",
                newName: "IX_Tags_ActiveFlag",
                table: "Tags");

            migrationBuilder.RenameIndex(
                name: "IX_TagGroupTypes_Active",
                newName: "IX_TagGroupTypes_ActiveFlag",
                table: "TagGroupTypes");

            migrationBuilder.RenameIndex(
                name: "IX_TagGroups_Active",
                newName: "IX_TagGroups_ActiveFlag",
                table: "TagGroups");

            migrationBuilder.RenameIndex(
                name: "IX_ItemTypes_Active",
                newName: "IX_ItemTypes_ActiveFlag",
                table: "ItemTypes");

            migrationBuilder.RenameIndex(
                name: "IX_ItemTags_Active",
                newName: "IX_ItemTags_ActiveFlag",
                table: "ItemTags");

            migrationBuilder.RenameIndex(
                name: "IX_Items_Active",
                newName: "IX_Items_ActiveFlag",
                table: "Items");

            
            migrationBuilder.RenameColumn(
                name: "Active",
                newName: "ActiveFlag",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                newName: "CreatedDate",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                newName: "ModifiedDate",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                newName: "ModifiedBy",
                table: "Tags");


            migrationBuilder.RenameColumn(
                name: "Active",
                newName: "ActiveFlag",
                table: "TagGroupTypes");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                newName: "CreatedDate",
                table: "TagGroupTypes");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                newName: "ModifiedDate",
                table: "TagGroupTypes");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                newName: "ModifiedBy",
                table: "TagGroupTypes");


            migrationBuilder.RenameColumn(
                name: "Active",
                newName: "ActiveFlag",
                table: "TagGroups");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                newName: "CreatedDate",
                table: "TagGroups");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                newName: "ModifiedDate",
                table: "TagGroups");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                newName: "ModifiedBy",
                table: "TagGroups");


            migrationBuilder.RenameColumn(
                name: "Active",
                newName: "ActiveFlag",
                table: "ItemTypes");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                newName: "CreatedDate",
                table: "ItemTypes");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                newName: "ModifiedDate",
                table: "ItemTypes");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                newName: "ModifiedBy",
                table: "ItemTypes");


            migrationBuilder.RenameColumn(
                name: "Active",
                newName: "ActiveFlag",
                table: "ItemTags");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                newName: "CreatedDate",
                table: "ItemTags");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                newName: "ModifiedDate",
                table: "ItemTags");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                newName: "ModifiedBy",
                table: "ItemTags");


            migrationBuilder.RenameColumn(
                name: "Active",
                newName: "ActiveFlag",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                newName: "CreatedDate",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                newName: "ModifiedDate",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                newName: "ModifiedBy",
                table: "Items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                newName: "IX_Tags_Active",
                name: "IX_Tags_ActiveFlag",
                table: "Tags");

            migrationBuilder.RenameIndex(
                newName: "IX_TagGroupTypes_Active",
                name: "IX_TagGroupTypes_ActiveFlag",
                table: "TagGroupTypes");

            migrationBuilder.RenameIndex(
                newName: "IX_TagGroups_Active",
                name: "IX_TagGroups_ActiveFlag",
                table: "TagGroups");

            migrationBuilder.RenameIndex(
                newName: "IX_ItemTypes_Active",
                name: "IX_ItemTypes_ActiveFlag",
                table: "ItemTypes");

            migrationBuilder.RenameIndex(
                newName: "IX_ItemTags_Active",
                name: "IX_ItemTags_ActiveFlag",
                table: "ItemTags");

            migrationBuilder.RenameIndex(
                newName: "IX_Items_Active",
                name: "IX_Items_ActiveFlag",
                table: "Items");


            migrationBuilder.RenameColumn(
                newName: "Active",
                name: "ActiveFlag",
                table: "Tags");

            migrationBuilder.RenameColumn(
                newName: "CreatedAt",
                name: "CreatedDate",
                table: "Tags");

            migrationBuilder.RenameColumn(
                newName: "LastUpdatedAt",
                name: "ModifiedDate",
                table: "Tags");

            migrationBuilder.RenameColumn(
                newName: "LastUpdatedBy",
                name: "ModifiedBy",
                table: "Tags");


            migrationBuilder.RenameColumn(
                newName: "Active",
                name: "ActiveFlag",
                table: "TagGroupTypes");

            migrationBuilder.RenameColumn(
                newName: "CreatedAt",
                name: "CreatedDate",
                table: "TagGroupTypes");

            migrationBuilder.RenameColumn(
                newName: "LastUpdatedAt",
                name: "ModifiedDate",
                table: "TagGroupTypes");

            migrationBuilder.RenameColumn(
                newName: "LastUpdatedBy",
                name: "ModifiedBy",
                table: "TagGroupTypes");


            migrationBuilder.RenameColumn(
                newName: "Active",
                name: "ActiveFlag",
                table: "TagGroups");

            migrationBuilder.RenameColumn(
                newName: "CreatedAt",
                name: "CreatedDate",
                table: "TagGroups");

            migrationBuilder.RenameColumn(
                newName: "LastUpdatedAt",
                name: "ModifiedDate",
                table: "TagGroups");

            migrationBuilder.RenameColumn(
                newName: "LastUpdatedBy",
                name: "ModifiedBy",
                table: "TagGroups");


            migrationBuilder.RenameColumn(
                newName: "Active",
                name: "ActiveFlag",
                table: "ItemTypes");

            migrationBuilder.RenameColumn(
                newName: "CreatedAt",
                name: "CreatedDate",
                table: "ItemTypes");

            migrationBuilder.RenameColumn(
                newName: "LastUpdatedAt",
                name: "ModifiedDate",
                table: "ItemTypes");

            migrationBuilder.RenameColumn(
                newName: "LastUpdatedBy",
                name: "ModifiedBy",
                table: "ItemTypes");


            migrationBuilder.RenameColumn(
                newName: "Active",
                name: "ActiveFlag",
                table: "ItemTags");

            migrationBuilder.RenameColumn(
                newName: "CreatedAt",
                name: "CreatedDate",
                table: "ItemTags");

            migrationBuilder.RenameColumn(
                newName: "LastUpdatedAt",
                name: "ModifiedDate",
                table: "ItemTags");

            migrationBuilder.RenameColumn(
                newName: "LastUpdatedBy",
                name: "ModifiedBy",
                table: "ItemTags");


            migrationBuilder.RenameColumn(
                newName: "Active",
                name: "ActiveFlag",
                table: "Items");

            migrationBuilder.RenameColumn(
                newName: "CreatedAt",
                name: "CreatedDate",
                table: "Items");

            migrationBuilder.RenameColumn(
                newName: "LastUpdatedAt",
                name: "ModifiedDate",
                table: "Items");

            migrationBuilder.RenameColumn(
                newName: "LastUpdatedBy",
                name: "ModifiedBy",
                table: "Items");
        }
    }
}
