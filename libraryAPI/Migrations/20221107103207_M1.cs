using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace libraryAPI.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author", x => new { x.name, x.date_of_birth });
                });

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    date_of_first_publication = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    edition = table.Column<int>(type: "integer", nullable: false),
                    publisher = table.Column<string>(type: "text", nullable: false),
                    original_language = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => new { x.name, x.date_of_first_publication });
                });

            migrationBuilder.CreateTable(
                name: "relation",
                columns: table => new
                {
                    bookname = table.Column<string>(type: "text", nullable: false),
                    bookdate_of_first_publication = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    authorname = table.Column<string>(type: "text", nullable: false),
                    authordate_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relation", x => new { x.bookname, x.bookdate_of_first_publication, x.authorname, x.authordate_of_birth });
                    table.ForeignKey(
                        name: "FK_relation_author_authorname_authordate_of_birth",
                        columns: x => new { x.authorname, x.authordate_of_birth },
                        principalTable: "author",
                        principalColumns: new[] { "name", "date_of_birth" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_relation_book_bookname_bookdate_of_first_publication",
                        columns: x => new { x.bookname, x.bookdate_of_first_publication },
                        principalTable: "book",
                        principalColumns: new[] { "name", "date_of_first_publication" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_relation_authorname_authordate_of_birth",
                table: "relation",
                columns: new[] { "authorname", "authordate_of_birth" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "relation");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "book");
        }
    }
}
