using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RaportareOTR.Data.Migrations
{
    public partial class removeUserExtraColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClickCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentWorkPoint",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileViews",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClickCount",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CurrentWorkPoint",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProfileViews",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
