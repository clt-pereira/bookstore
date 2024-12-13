using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using BookStore.Business.Models.Validations;

namespace BookStore.Business.Services;

public class LivroService(
    ILivroRepository livroRepository,
    IAutorRepository autorRepository,
    IAssuntoRepository assuntoRepository,
    INotificador notificador) : BaseService(notificador), ILivroService
{
    private readonly ILivroRepository _livroRepository = livroRepository;
    private readonly IAutorRepository _autorRepository = autorRepository;
    private readonly IAssuntoRepository _assuntoRepository = assuntoRepository;

    public async Task AdicionarAsync(Livro livro, CancellationToken cancellationToken = default)
    {
        if (!ExecutarValidacao(new LivroValidation(), livro)) return;

        if (await _livroRepository.ExistsByExpressionAsync(f => f.Titulo == livro.Titulo, cancellationToken))
        {
            Notificar("Já existe um livro com este título infomado.");
            return;
        }

        var autorIds = livro.Autores.Select(a => a.Id).ToList();
        var assuntoIds = livro.Assuntos.Select(a => a.Id).ToList();

        var autores = await _autorRepository.FindByExpressionAsync(x => autorIds.Contains(x.Id), cancellationToken);
        livro.Autores = autores.ToList();

        var assuntos = await _assuntoRepository.FindByExpressionAsync(x => assuntoIds.Contains(x.Id), cancellationToken);
        livro.Assuntos = assuntos.ToList();

        await _livroRepository.InsertAsync(livro, cancellationToken);
    }

    public async Task AtualizarAsync(Livro livro, CancellationToken cancellationToken = default)
    {
        if (!ExecutarValidacao(new LivroValidation(), livro)) return;

        if (!await _livroRepository.ExistsByExpressionAsync(x => x.Id == livro.Id, cancellationToken))
        {
            Notificar($"Livro: {livro.Id} - {livro.Titulo} não encontrado.");
            return;
        }

        var autorIds = livro.Autores.Select(a => a.Id).ToList();
        var assuntoIds = livro.Assuntos.Select(a => a.Id).ToList();

        await _livroRepository.RemoveAsync(livro.Id, cancellationToken);

        var autores = await _autorRepository.FindByExpressionAsync(x => autorIds.Contains(x.Id), cancellationToken);
        livro.Autores = autores.ToList();

        var assuntos = await _assuntoRepository.FindByExpressionAsync(x => assuntoIds.Contains(x.Id), cancellationToken);
        livro.Assuntos = assuntos.ToList();

        // Atualiza o livro com os novos autores e assuntos
        await _livroRepository.InsertAsync(livro, cancellationToken);
    }


    public async Task RemoverAsync(int id, CancellationToken cancellationToken = default)
    {
        await _livroRepository.RemoveAsync(id, cancellationToken);
    }

    public void Dispose()
    {
        _livroRepository?.Dispose();
    }
}
