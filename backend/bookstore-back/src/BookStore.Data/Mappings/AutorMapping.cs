using BookStore.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Data.Mappings;

public class AutorMapping : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .HasColumnName("Cod_Au")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(p => p.Nome)
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder
            .ToTable("Autor");
    }
}
