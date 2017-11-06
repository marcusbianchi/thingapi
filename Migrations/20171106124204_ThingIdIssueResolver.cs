using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ThingsAPI.Migrations
{
    public partial class ThingIdIssueResolver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int[]>(
                name: "childrenThingsIds",
                table: "Things",
                type: "integer[]",
                nullable: true,
                oldClrType: typeof(List<Nullable<int>>),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<Nullable<int>>>(
                name: "childrenThingsIds",
                table: "Things",
                nullable: true,
                oldClrType: typeof(int[]),
                oldType: "integer[]",
                oldNullable: true);
        }
    }
}
