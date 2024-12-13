using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using BookStore.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repository;

public class LivroRepository(DatabaseContext context) : Repository<Livro>(context), ILivroRepository
{
    public async Task<Livro> ObterComAutoresAssuntosPorId(int id, CancellationToken cancellationToken = default)
    {
        return await Db.Livro.AsNoTracking()
            .Include(c => c.Autores)
            .Include(c => c.Assuntos)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Livro>> ObterTodosComAutoresAssuntos(CancellationToken cancellationToken = default)
    {
        return await Db.Livro.AsNoTracking()
            .Include(c => c.Autores)
            .Include(c => c.Assuntos)
            .ToListAsync(cancellationToken);
    }
}