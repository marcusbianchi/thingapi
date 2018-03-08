using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace thingservice.Migrations
{
    public partial class CreateToolGroupAssociated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToolGroupAssociates",
                columns: table => new
                {
                    toolGroupAssociatedId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    quantity = table.Column<int>(nullable: false),
                    thingGroupId = table.Column<int>(nullable: true),
                    toolGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolGroupAssociates", x => x.toolGroupAssociatedId);
                    table.ForeignKey(
                        name: "FK_ToolGroupAssociates_ThingGroups_thingGroupId",
                        column: x => x.thingGroupId,
                        principalTable: "ThingGroups",
                        principalColumn: "thingGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToolGroupAssociates_thingGroupId",
                table: "ToolGroupAssociates",
                column: "thingGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToolGroupAssociates");
        }
    }
}
