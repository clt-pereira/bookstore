CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;
CREATE TABLE "Assunto" (
    "Cod_As" INTEGER NOT NULL CONSTRAINT "PK_Assunto" PRIMARY KEY AUTOINCREMENT,
    "Descricao" varchar(20) NOT NULL
);

CREATE TABLE "Autor" (
    "Cod_Au" INTEGER NOT NULL CONSTRAINT "PK_Autor" PRIMARY KEY AUTOINCREMENT,
    "Nome" varchar(40) NOT NULL
);

CREATE TABLE "Livro" (
    "Codl" INTEGER NOT NULL CONSTRAINT "PK_Livro" PRIMARY KEY AUTOINCREMENT,
    "Titulo" varchar(40) NOT NULL,
    "Editora" varchar(40) NOT NULL,
    "Edicao" INTEGER NOT NULL,
    "AnoPublicacao" varchar(4) NOT NULL,
    "Valor" decimal(18,2) NOT NULL
);

CREATE TABLE "Livro_Assunto" (
    "Assunto_codAs" INTEGER NOT NULL,
    "Livro_Codl" INTEGER NOT NULL,
    CONSTRAINT "PK_Livro_Assunto" PRIMARY KEY ("Assunto_codAs", "Livro_Codl"),
    CONSTRAINT "FK_Livro_Assunto_Assunto_Assunto_codAs" FOREIGN KEY ("Assunto_codAs") REFERENCES "Assunto" ("Cod_As") ON DELETE CASCADE,
    CONSTRAINT "FK_Livro_Assunto_Livro_Livro_Codl" FOREIGN KEY ("Livro_Codl") REFERENCES "Livro" ("Codl") ON DELETE CASCADE
);

CREATE TABLE "Livro_Autor" (
    "Autor_CodAu" INTEGER NOT NULL,
    "Livro_Codl" INTEGER NOT NULL,
    CONSTRAINT "PK_Livro_Autor" PRIMARY KEY ("Autor_CodAu", "Livro_Codl"),
    CONSTRAINT "FK_Livro_Autor_Autor_Autor_CodAu" FOREIGN KEY ("Autor_CodAu") REFERENCES "Autor" ("Cod_Au") ON DELETE CASCADE,
    CONSTRAINT "FK_Livro_Autor_Livro_Livro_Codl" FOREIGN KEY ("Livro_Codl") REFERENCES "Livro" ("Codl") ON DELETE CASCADE
);

CREATE INDEX "IX_Livro_Assunto_Livro_Codl" ON "Livro_Assunto" ("Livro_Codl");

CREATE INDEX "IX_Livro_Autor_Livro_Codl" ON "Livro_Autor" ("Livro_Codl");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241212150927_InitialCreate', '9.0.0');

COMMIT;

