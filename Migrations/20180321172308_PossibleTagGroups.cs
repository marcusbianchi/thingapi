using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace thingservice.Migrations
{
    public partial class PossibleTagGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "possibleTagGroups",
                table: "ThingGroups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "possibleTagGroups",
                table: "ThingGroups");
        }
    }
}
