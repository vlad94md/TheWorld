using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheWorld.Migrations
{
    public partial class RenameColumnLogitudeInStopEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "Stops");

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Stops",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Stops");

            migrationBuilder.AddColumn<double>(
                name: "Longtitude",
                table: "Stops",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
