using BookStore.Business.Models;

namespace BookStore.Business.Interfaces;

public interface IAutorService : IDisposable
{
    Task AdicionarAsync(Autor autor, CancellationToken cancellationToken = default);
    Task AtualizarAsync(Autor autor, CancellationToken cancellationToken = default);
    Task RemoverAsync(int id, CancellationToken cancellationToken = default);
}
