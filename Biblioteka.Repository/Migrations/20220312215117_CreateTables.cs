using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Biblioteka.Repository.Migrations
{
    public partial class CreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    author = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    publishing_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bookId", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    passwordHash = table.Column<string>(type: "CHAR(64)", nullable: false),
                    passwordSalt = table.Column<string>(type: "CHAR(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_userId", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_passwordSalt",
                table: "User",
                column: "passwordSalt",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_username",
                table: "User",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
