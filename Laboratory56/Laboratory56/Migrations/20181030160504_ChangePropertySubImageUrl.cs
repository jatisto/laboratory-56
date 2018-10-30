using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Laboratory56.Migrations
{
    public partial class ChangePropertySubImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Publications_PublicationId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_PublicationId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "Subscriptions");

            migrationBuilder.AddColumn<string>(
                name: "SubImageUrl",
                table: "Subscriptions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubImageUrl",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "PublicationId",
                table: "Subscriptions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PublicationId",
                table: "Subscriptions",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Publications_PublicationId",
                table: "Subscriptions",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
