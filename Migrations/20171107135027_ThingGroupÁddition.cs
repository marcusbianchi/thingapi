using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace thingservice.Migrations
{
    public partial class ThingGroupÁddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThingGroups",
                columns: table => new
                {
                    thingGroupId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    enabled = table.Column<bool>(type: "bool", maxLength: 100, nullable: false),
                    groupCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    groupDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    groupName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    thingsIds = table.Column<int[]>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThingGroups", x => x.thingGroupId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThingGroups");
        }
    }
}
