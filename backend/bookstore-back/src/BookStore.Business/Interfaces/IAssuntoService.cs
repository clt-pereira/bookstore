using BookStore.Business.Models;

namespace BookStore.Business.Interfaces;

public interface IAssuntoService : IDisposable
{
    Task AdicionarAsync(Assunto assunto, CancellationToken cancellationToken = default);
    Task AtualizarAsync(Assunto assunto, CancellationToken cancellationToken = default);
    Task RemoverAsync(int id, CancellationToken cancellationToken = default);
}
