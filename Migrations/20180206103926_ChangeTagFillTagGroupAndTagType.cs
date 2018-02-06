using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace thingservice.Migrations
{
    public partial class ChangeTagFillTagGroupAndTagType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tagGroup",
                table: "Tags",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tagType",
                table: "Tags",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tagGroup",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "tagType",
                table: "Tags");
        }
    }
}
