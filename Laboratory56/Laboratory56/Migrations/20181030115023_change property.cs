using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Laboratory56.Migrations
{
    public partial class changeproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Subscriptions_PublicSubSubscriptionId",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Publications_PublicSubSubscriptionId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "PublicSubId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "PublicSubSubscriptionId",
                table: "Publications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicSubId",
                table: "Publications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicSubSubscriptionId",
                table: "Publications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publications_PublicSubSubscriptionId",
                table: "Publications",
                column: "PublicSubSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Subscriptions_PublicSubSubscriptionId",
                table: "Publications",
                column: "PublicSubSubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "SubscriptionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
