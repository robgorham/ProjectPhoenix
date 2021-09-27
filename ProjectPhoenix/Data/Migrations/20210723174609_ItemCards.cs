using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectPhoenix.Data.Migrations
{
    public partial class ItemCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Columns_Boards_boardid",
                table: "Columns");

            migrationBuilder.RenameColumn(
                name: "boardid",
                table: "Columns",
                newName: "BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_Columns_boardid",
                table: "Columns",
                newName: "IX_Columns_BoardId");

            migrationBuilder.AlterColumn<Guid>(
                name: "BoardId",
                table: "Columns",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ItemCards",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Boardid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Columnid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCards", x => x.id);
                    table.ForeignKey(
                        name: "FK_ItemCards_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemCards_Boards_Boardid",
                        column: x => x.Boardid,
                        principalTable: "Boards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemCards_Columns_Columnid",
                        column: x => x.Columnid,
                        principalTable: "Columns",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCards_Boardid",
                table: "ItemCards",
                column: "Boardid");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCards_Columnid",
                table: "ItemCards",
                column: "Columnid");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCards_UserId",
                table: "ItemCards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Columns_Boards_BoardId",
                table: "Columns",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Columns_Boards_BoardId",
                table: "Columns");

            migrationBuilder.DropTable(
                name: "ItemCards");

            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "Columns",
                newName: "boardid");

            migrationBuilder.RenameIndex(
                name: "IX_Columns_BoardId",
                table: "Columns",
                newName: "IX_Columns_boardid");

            migrationBuilder.AlterColumn<Guid>(
                name: "boardid",
                table: "Columns",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Columns_Boards_boardid",
                table: "Columns",
                column: "boardid",
                principalTable: "Boards",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
