using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Minibank.Data.Migrations
{
    public partial class AddUserAccountsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.AddPrimaryKey(
                name: "pk_id",
                table: "user",
                column: "id");

            migrationBuilder.CreateTable(
                name: "user_accounts",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Balance = table.Column<double>(type: "double precision", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    AccountPriv = table.Column<bool>(type: "boolean", nullable: false),
                    OpenAccount = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CloseAccount = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_accounts_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_accounts_UserId",
                table: "user_accounts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_accounts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_id",
                table: "user");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");
        }
    }
}
