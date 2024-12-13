using BookStore.Business.Models;

namespace BookStore.Business.Interfaces;

public interface ILivroService : IDisposable
{
    Task AdicionarAsync(Livro livro, CancellationToken cancellationToken = default);
    Task AtualizarAsync(Livro livro, CancellationToken cancellationToken = default);
    Task RemoverAsync(int id, CancellationToken cancellationToken = default);
}
