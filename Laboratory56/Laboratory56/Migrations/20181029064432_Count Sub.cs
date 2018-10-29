using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Laboratory56.Migrations
{
    public partial class CountSub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subscription",
                table: "Publications");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionCount",
                table: "Subscriptions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionCount",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "Subscription",
                table: "Publications",
                nullable: false,
                defaultValue: 0);
        }
    }
}
