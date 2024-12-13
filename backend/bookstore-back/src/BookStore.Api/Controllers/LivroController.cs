using AutoMapper;
using BookStore.Api.ViewModels;
using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Api.Controllers;

[Route("api/v1/livro")]
public class LivroController(
    IMapper mapper,
    ILivroRepository livroRepository,
    ILivroService livroService,
    INotificador notificador) : MainController(notificador)
{
    private readonly IMapper _mapper = mapper;
    private readonly ILivroRepository _livroRepository = livroRepository;
    private readonly ILivroService _livroService = livroService;

    [HttpGet]
    public async Task<IEnumerable<LivroViewModel>> ObterTodosAsync(CancellationToken cancellationToken = default)
    {
        var response = _mapper.Map<IEnumerable<LivroViewModel>>(await _livroRepository.ObterTodosComAutoresAssuntos(cancellationToken));
        return response;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LivroViewModel>> ObterPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var livro = _mapper.Map<LivroViewModel>(await _livroRepository.ObterComAutoresAssuntosPorId(id, cancellationToken));

        if (livro is null) return NotFound();

        return livro;
    }

    [HttpPost]
    public async Task<ActionResult<LivroViewModel>> AdicionarAsync(LivroViewModel livroViewModel, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _livroService.AdicionarAsync(_mapper.Map<Livro>(livroViewModel), cancellationToken);

        return CustomResponse(HttpStatusCode.Created, livroViewModel);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<LivroViewModel>> AtualizarAsync(int id, LivroViewModel livroViewModel, CancellationToken cancellationToken = default)
    {
        if (id != livroViewModel.Id)
        {
            NotificarErro("O id informado não é o mesmo que foi passado na query");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _livroService.AtualizarAsync(_mapper.Map<Livro>(livroViewModel), cancellationToken);

        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<LivroViewModel>> ExcluirAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!await _livroRepository.ExistsByExpressionAsync(x => x.Id == id, cancellationToken)) 
            return NotFound();

        await _livroService.RemoverAsync(id, cancellationToken);

        return CustomResponse(HttpStatusCode.NoContent);
    }
}