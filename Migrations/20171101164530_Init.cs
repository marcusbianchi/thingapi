using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace thingservice.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Things",
                columns: table => new
                {
                    thingId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    enabled = table.Column<bool>(type: "bool", nullable: false),
                    parentthingId = table.Column<int>(type: "int4", nullable: true),
                    physicalConnection = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    thingCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    thingName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Things", x => x.thingId);
                    table.ForeignKey(
                        name: "FK_Things_Things_parentthingId",
                        column: x => x.parentthingId,
                        principalTable: "Things",
                        principalColumn: "thingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Things_parentthingId",
                table: "Things",
                column: "parentthingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Things");
        }
    }
}
