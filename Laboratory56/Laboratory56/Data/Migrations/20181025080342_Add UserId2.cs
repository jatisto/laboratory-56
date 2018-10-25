using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Laboratory56.Data.Migrations
{
    public partial class AddUserId2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_AspNetUsers_ApplicationUserId",
                table: "Publications");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Publications",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Publications_ApplicationUserId",
                table: "Publications",
                newName: "IX_Publications_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_AspNetUsers_UserId",
                table: "Publications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_AspNetUsers_UserId",
                table: "Publications");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Publications",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Publications_UserId",
                table: "Publications",
                newName: "IX_Publications_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_AspNetUsers_ApplicationUserId",
                table: "Publications",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
