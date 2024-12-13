using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using BookStore.Business.Models.Validations;

namespace BookStore.Business.Services;

public class AutorService(
    IAutorRepository autorRepository,
    ILivroRepository livroRepository,
    INotificador notificador) : BaseService(notificador), IAutorService
{
    private readonly IAutorRepository _autorRepository = autorRepository;
    private readonly ILivroRepository _livroRepository = livroRepository;

    public async Task AdicionarAsync(Autor autor, CancellationToken cancellationToken = default)
    {
        if (!ExecutarValidacao(new AutorValidation(), autor)) return;

        if (await _autorRepository.ExistsByExpressionAsync(f => f.Nome == autor.Nome, cancellationToken))
        {
            Notificar("Já existe um autor com este nome infomado.");
            return;
        }

        await _autorRepository.InsertAsync(autor, cancellationToken);
    }

    public async Task AtualizarAsync(Autor autor, CancellationToken cancellationToken = default)
    {
        if (!ExecutarValidacao(new AutorValidation(), autor)) return;

        if (!await _autorRepository.ExistsByExpressionAsync(f => f.Id == autor.Id, cancellationToken))
        {
            Notificar($"Autor {autor.Id} não encontrado.");
            return;
        }

        await _autorRepository.UpdateAsync(autor, cancellationToken);
    }

    public async Task RemoverAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await _livroRepository.ExistsByExpressionAsync(x => x.Autores.Any(a => a.Id == id), cancellationToken))
        {
            Notificar($"Exclusão do Autor: {id} não permitida, o registro já está relacionado à um livro");
            return;
        }

        await _autorRepository.RemoveAsync(id, cancellationToken);
    }

    public void Dispose()
    {
        _autorRepository?.Dispose();
    }
}
