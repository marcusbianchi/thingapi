using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace thingservice.Migrations
{
    public partial class EnableRemoveParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_ThingGroups_thingGroupId",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "enabled",
                table: "Parameters");

            migrationBuilder.AlterColumn<int>(
                name: "thingGroupId",
                table: "Parameters",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_ThingGroups_thingGroupId",
                table: "Parameters",
                column: "thingGroupId",
                principalTable: "ThingGroups",
                principalColumn: "thingGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_ThingGroups_thingGroupId",
                table: "Parameters");

            migrationBuilder.AlterColumn<int>(
                name: "thingGroupId",
                table: "Parameters",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int4");

            migrationBuilder.AddColumn<bool>(
                name: "enabled",
                table: "Parameters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_ThingGroups_thingGroupId",
                table: "Parameters",
                column: "thingGroupId",
                principalTable: "ThingGroups",
                principalColumn: "thingGroupId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
