using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectPhoenix.Data.Migrations
{
    public partial class Columnv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Columns",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    createDate = table.Column<DateTime>(nullable: false),
                    modifyDate = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    order = table.Column<int>(nullable: false),
                    boardid = table.Column<Guid>(nullable: true),
                    userId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Columns", x => x.id);
                    table.ForeignKey(
                        name: "FK_Columns_Boards_boardid",
                        column: x => x.boardid,
                        principalTable: "Boards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Columns_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Columns_boardid",
                table: "Columns",
                column: "boardid");

            migrationBuilder.CreateIndex(
                name: "IX_Columns_userId",
                table: "Columns",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Columns");
        }
    }
}
