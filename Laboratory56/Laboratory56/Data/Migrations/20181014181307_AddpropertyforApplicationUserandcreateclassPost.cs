using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Laboratory56.Data.Migrations
{
    public partial class AddpropertyforApplicationUserandcreateclassPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostsCounter",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Subscriber",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subscribtions",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    CommentPost = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    LikePost = table.Column<string>(nullable: true),
                    NamePost = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_ApplicationUserId",
                table: "Post",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropColumn(
                name: "PostsCounter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Subscriber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Subscribtions",
                table: "AspNetUsers");
        }
    }
}
