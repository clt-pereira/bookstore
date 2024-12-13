using BookStore.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Data.Mappings;

public class LivroMapping : IEntityTypeConfiguration<Livro>
{
    public void Configure(EntityTypeBuilder<Livro> builder)
    {
        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .HasColumnName("Codl")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(p => p.Titulo)
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder
            .Property(p => p.Editora)
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder
            .Property(p => p.Edicao)
            .IsRequired();

        builder
            .Property(p => p.AnoPublicacao)
            .HasColumnType("varchar(4)")
            .IsRequired();

        builder
            .Property(p => p.Valor)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder
            .HasMany(p => p.Assuntos)
            .WithMany(p => p.Livros)
            .UsingEntity<Dictionary<string, object>>(
                "Livro_Assunto",
                assunto => assunto.HasOne<Assunto>().WithMany().HasForeignKey("Assunto_codAs"),
                livro => livro.HasOne<Livro>().WithMany().HasForeignKey("Livro_Codl")
            );

        builder
            .HasMany(p => p.Autores)
            .WithMany(a => a.Livros)
            .UsingEntity<Dictionary<string, object>>(
                "Livro_Autor",
                autor => autor.HasOne<Autor>().WithMany().HasForeignKey("Autor_CodAu"),
                livro => livro.HasOne<Livro>().WithMany().HasForeignKey("Livro_Codl")
            );

        builder.ToTable("Livro");
    }
}
