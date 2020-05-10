using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Imageboard.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShortUrl = table.Column<string>(maxLength: 16, nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boards_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 64, nullable: false),
                    Signature = table.Column<string>(maxLength: 32, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    BoardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Signature = table.Column<string>(maxLength: 32, nullable: true),
                    IsOp = table.Column<bool>(nullable: false),
                    Text = table.Column<string>(maxLength: 15000, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    TopicId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Posts_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Posts_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OriginalFilename = table.Column<string>(nullable: true),
                    Filename = table.Column<string>(maxLength: 64, nullable: false),
                    ContentType = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_PostId",
                table: "Attachments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_GroupId",
                table: "Boards",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_ShortUrl",
                table: "Boards",
                column: "ShortUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_SortOrder",
                table: "Boards",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SortOrder",
                table: "Groups",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Created",
                table: "Posts",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ParentId",
                table: "Posts",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TopicId",
                table: "Posts",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_BoardId",
                table: "Topics",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_LastUpdated",
                table: "Topics",
                column: "LastUpdated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
