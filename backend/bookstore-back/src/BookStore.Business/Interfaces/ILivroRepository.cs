using BookStore.Business.Models;

namespace BookStore.Business.Interfaces;

public interface ILivroRepository : IRepository<Livro>
{
    Task<ICollection<Livro>> ObterTodosComAutoresAssuntos(CancellationToken cancellationToken = default);
    Task<Livro> ObterComAutoresAssuntosPorId(int id, CancellationToken cancellationToken = default);
}
