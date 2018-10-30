using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Laboratory56.Migrations
{
    public partial class IntOnStringPulicSubId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Subscriptions_PublicSubId",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Publications_PublicSubId",
                table: "Publications");

            migrationBuilder.AlterColumn<string>(
                name: "PublicSubId",
                table: "Publications",
                nullable: true,
                oldClrType: typeof(int));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Subscriptions_PublicSubSubscriptionId",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Publications_PublicSubSubscriptionId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "PublicSubSubscriptionId",
                table: "Publications");

            migrationBuilder.AlterColumn<int>(
                name: "PublicSubId",
                table: "Publications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publications_PublicSubId",
                table: "Publications",
                column: "PublicSubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Subscriptions_PublicSubId",
                table: "Publications",
                column: "PublicSubId",
                principalTable: "Subscriptions",
                principalColumn: "SubscriptionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
