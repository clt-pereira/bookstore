using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using BookStore.Business.Models.Validations;

namespace BookStore.Business.Services;

public class AssuntoService(
    IAssuntoRepository assuntoRepository,
    ILivroRepository livroRepository,
    INotificador notificador) : BaseService(notificador), IAssuntoService
{
    private readonly IAssuntoRepository _assuntoRepository = assuntoRepository;
    private readonly ILivroRepository _livroRepository = livroRepository;

    public async Task AdicionarAsync(Assunto assunto, CancellationToken cancellationToken = default)
    {
        if (!ExecutarValidacao(new AssuntoValidation(), assunto)) return;

        if(await _assuntoRepository.ExistsByExpressionAsync(f => f.Descricao == assunto.Descricao, cancellationToken))
        {
            Notificar("Já existe um assunto com a descrição informada.");
            return;
        }

        await _assuntoRepository.InsertAsync(assunto, cancellationToken);
    }

    public async Task AtualizarAsync(Assunto assunto, CancellationToken cancellationToken = default)
    {
        if (!ExecutarValidacao(new AssuntoValidation(), assunto)) return;

        if (!await _assuntoRepository.ExistsByExpressionAsync(f => f.Id == assunto.Id, cancellationToken))
        {
            Notificar($"Assunto {assunto.Id} não encontrado.");
            return;
        }

        await _assuntoRepository.UpdateAsync(assunto, cancellationToken);
    }

    public async Task RemoverAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await _livroRepository.ExistsByExpressionAsync(x => x.Assuntos.Any(a => a.Id == id), cancellationToken))
        {
            Notificar($"Exclusão do Assunto: {id} não permitida, o registro já está relacionado à um livro");
            return;
        }

        await _assuntoRepository.RemoveAsync(id, cancellationToken);
    }

    public void Dispose()
    {
        _assuntoRepository?.Dispose();
    }
}
