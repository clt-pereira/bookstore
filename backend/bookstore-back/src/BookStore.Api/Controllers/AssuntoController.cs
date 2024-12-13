using AutoMapper;
using BookStore.Api.ViewModels;
using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Api.Controllers;

[Route("api/v1/assunto")]
public class AssuntoController(
    IMapper mapper,
    IAssuntoRepository assuntoRepository,
    IAssuntoService assuntoService,
    INotificador notificador) : MainController(notificador)
{
    private readonly IMapper _mapper = mapper;
    private readonly IAssuntoRepository _assuntoRepository = assuntoRepository;
    private readonly IAssuntoService _assuntoService = assuntoService;

    [HttpGet]
    public async Task<IEnumerable<AssuntoViewModel>> ObterTodosAsync(CancellationToken cancellationToken = default)
    {
        return _mapper.Map<IEnumerable<AssuntoViewModel>>(await _assuntoRepository.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AssuntoViewModel>> ObterPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var assunto = _mapper.Map<AssuntoViewModel>(await _assuntoRepository.GetByIdAsync(id, cancellationToken));

        if (assunto is null) return NotFound();

        return assunto;
    }

    [HttpPost]
    public async Task<ActionResult<AssuntoViewModel>> AdicionarAsync(AssuntoViewModel assuntoViewModel, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _assuntoService.AdicionarAsync(_mapper.Map<Assunto>(assuntoViewModel), cancellationToken);

        return CustomResponse(HttpStatusCode.Created, assuntoViewModel);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AssuntoViewModel>> AtualizarAsync(int id, AssuntoViewModel assuntoViewModel, CancellationToken cancellationToken = default)
    {
        if (id != assuntoViewModel.Id)
        {
            NotificarErro("O id informado não é o mesmo que foi passado na query");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _assuntoService.AtualizarAsync(_mapper.Map<Assunto>(assuntoViewModel), cancellationToken);

        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<AssuntoViewModel>> ExcluirAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!await _assuntoRepository.ExistsByExpressionAsync(x => x.Id == id, cancellationToken)) 
            return NotFound();

        await _assuntoService.RemoverAsync(id, cancellationToken);

        return CustomResponse(HttpStatusCode.NoContent);
    }
}