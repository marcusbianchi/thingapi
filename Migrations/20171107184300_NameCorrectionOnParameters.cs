using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace thingservice.Migrations
{
    public partial class NameCorrectionOnParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameter_ThingGroups_thingGroupId",
                table: "Parameter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parameter",
                table: "Parameter");

            migrationBuilder.RenameTable(
                name: "Parameter",
                newName: "Parameters");

            migrationBuilder.RenameIndex(
                name: "IX_Parameter_thingGroupId",
                table: "Parameters",
                newName: "IX_Parameters_thingGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parameters",
                table: "Parameters",
                column: "parameterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_ThingGroups_thingGroupId",
                table: "Parameters",
                column: "thingGroupId",
                principalTable: "ThingGroups",
                principalColumn: "thingGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_ThingGroups_thingGroupId",
                table: "Parameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parameters",
                table: "Parameters");

            migrationBuilder.RenameTable(
                name: "Parameters",
                newName: "Parameter");

            migrationBuilder.RenameIndex(
                name: "IX_Parameters_thingGroupId",
                table: "Parameter",
                newName: "IX_Parameter_thingGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parameter",
                table: "Parameter",
                column: "parameterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parameter_ThingGroups_thingGroupId",
                table: "Parameter",
                column: "thingGroupId",
                principalTable: "ThingGroups",
                principalColumn: "thingGroupId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
