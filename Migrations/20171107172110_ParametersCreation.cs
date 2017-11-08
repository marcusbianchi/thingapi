using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace thingservice.Migrations
{
    public partial class ParametersCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parameter",
                columns: table => new
                {
                    parameterId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ParameterCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    enabled = table.Column<bool>(type: "bool", nullable: false),
                    parameterDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    parameterName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    physicalTag = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    thingGroupId = table.Column<int>(type: "int4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameter", x => x.parameterId);
                    table.ForeignKey(
                        name: "FK_Parameter_ThingGroups_thingGroupId",
                        column: x => x.thingGroupId,
                        principalTable: "ThingGroups",
                        principalColumn: "thingGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parameter_thingGroupId",
                table: "Parameter",
                column: "thingGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parameter");
        }
    }
}
