using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace thingservice.Migrations
{
    public partial class ChangedParameterToTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.AddColumn<string>(
                name: "groupPrefix",
                table: "ThingGroups",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    tagId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    physicalTag = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    tagDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    tagName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    thingGroupId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.tagId);
                    table.ForeignKey(
                        name: "FK_Tags_ThingGroups_thingGroupId",
                        column: x => x.thingGroupId,
                        principalTable: "ThingGroups",
                        principalColumn: "thingGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_thingGroupId",
                table: "Tags",
                column: "thingGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropColumn(
                name: "groupPrefix",
                table: "ThingGroups");

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    parameterId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ParameterCode = table.Column<string>(maxLength: 50, nullable: true),
                    parameterDescription = table.Column<string>(maxLength: 100, nullable: true),
                    parameterName = table.Column<string>(maxLength: 50, nullable: false),
                    physicalTag = table.Column<string>(maxLength: 100, nullable: true),
                    thingGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.parameterId);
                    table.ForeignKey(
                        name: "FK_Parameters_ThingGroups_thingGroupId",
                        column: x => x.thingGroupId,
                        principalTable: "ThingGroups",
                        principalColumn: "thingGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_thingGroupId",
                table: "Parameters",
                column: "thingGroupId");
        }
    }
}
