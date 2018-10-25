using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Laboratory56.Migrations
{
    public partial class AddproertyImageUrlforComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComentCount",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "ComentCount",
                table: "Comments",
                nullable: false,
                defaultValue: 0);
        }
    }
}
