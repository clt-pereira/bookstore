using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assunto",
                columns: table => new
                {
                    Cod_As = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assunto", x => x.Cod_As);
                });

            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    Cod_Au = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "varchar(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.Cod_Au);
                });

            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    Codl = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "varchar(40)", nullable: false),
                    Editora = table.Column<string>(type: "varchar(40)", nullable: false),
                    Edicao = table.Column<int>(type: "INTEGER", nullable: false),
                    AnoPublicacao = table.Column<string>(type: "varchar(4)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.Codl);
                });

            migrationBuilder.CreateTable(
                name: "Livro_Assunto",
                columns: table => new
                {
                    Assunto_codAs = table.Column<int>(type: "INTEGER", nullable: false),
                    Livro_Codl = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro_Assunto", x => new { x.Assunto_codAs, x.Livro_Codl });
                    table.ForeignKey(
                        name: "FK_Livro_Assunto_Assunto_Assunto_codAs",
                        column: x => x.Assunto_codAs,
                        principalTable: "Assunto",
                        principalColumn: "Cod_As",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Assunto_Livro_Livro_Codl",
                        column: x => x.Livro_Codl,
                        principalTable: "Livro",
                        principalColumn: "Codl",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Livro_Autor",
                columns: table => new
                {
                    Autor_CodAu = table.Column<int>(type: "INTEGER", nullable: false),
                    Livro_Codl = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro_Autor", x => new { x.Autor_CodAu, x.Livro_Codl });
                    table.ForeignKey(
                        name: "FK_Livro_Autor_Autor_Autor_CodAu",
                        column: x => x.Autor_CodAu,
                        principalTable: "Autor",
                        principalColumn: "Cod_Au",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Autor_Livro_Livro_Codl",
                        column: x => x.Livro_Codl,
                        principalTable: "Livro",
                        principalColumn: "Codl",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Assunto_Livro_Codl",
                table: "Livro_Assunto",
                column: "Livro_Codl");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Autor_Livro_Codl",
                table: "Livro_Autor",
                column: "Livro_Codl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livro_Assunto");

            migrationBuilder.DropTable(
                name: "Livro_Autor");

            migrationBuilder.DropTable(
                name: "Assunto");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Livro");
        }
    }
}
