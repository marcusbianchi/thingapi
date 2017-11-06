using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ThingsAPI.Migrations
{
    public partial class ChangeThingIdToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Things_Things_parentthingId",
                table: "Things");

            migrationBuilder.DropIndex(
                name: "IX_Things_parentthingId",
                table: "Things");

            migrationBuilder.RenameColumn(
                name: "parentthingId",
                table: "Things",
                newName: "parentThingId");

            migrationBuilder.AddColumn<List<Nullable<int>>>(
                name: "childrenThingsIds",
                table: "Things",
                type: "int4[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "childrenThingsIds",
                table: "Things");

            migrationBuilder.RenameColumn(
                name: "parentThingId",
                table: "Things",
                newName: "parentthingId");

            migrationBuilder.CreateIndex(
                name: "IX_Things_parentthingId",
                table: "Things",
                column: "parentthingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Things_Things_parentthingId",
                table: "Things",
                column: "parentthingId",
                principalTable: "Things",
                principalColumn: "thingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
