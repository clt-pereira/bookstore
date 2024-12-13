using BookStore.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Data.Mappings;

public class AssuntoMapping : IEntityTypeConfiguration<Assunto>
{
    public void Configure(EntityTypeBuilder<Assunto> builder)
    {
        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .HasColumnName("Cod_As")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(p => p.Descricao)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder
            .ToTable("Assunto");
    }
}
