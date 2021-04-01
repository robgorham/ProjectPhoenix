using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectPhoenix.Data.Migrations
{
    public partial class Boardv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    updateDate = table.Column<DateTime>(nullable: false),
                    createDate = table.Column<DateTime>(nullable: false),
                    modifyDate = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    userId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.id);
                    table.ForeignKey(
                        name: "FK_Boards_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boards_userId",
                table: "Boards",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}
