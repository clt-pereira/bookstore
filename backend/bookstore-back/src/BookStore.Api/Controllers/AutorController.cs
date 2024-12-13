using AutoMapper;
using BookStore.Api.ViewModels;
using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Api.Controllers;

[Route("api/v1/autor")]
public class AutorController(
    IMapper mapper,
    IAutorRepository autorRepository,
    IAutorService autorService,
    INotificador notificador) : MainController(notificador)
{
    private readonly IMapper _mapper = mapper;
    private readonly IAutorRepository _autorRepository = autorRepository;
    private readonly IAutorService _autorService = autorService;

    [HttpGet]
    public async Task<IEnumerable<AutorViewModel>> ObterTodosAsync(CancellationToken cancellationToken = default)
    {
        return _mapper.Map<IEnumerable<AutorViewModel>>(await _autorRepository.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AutorViewModel>> ObterPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var autor = _mapper.Map<AutorViewModel>(await _autorRepository.GetByIdAsync(id, cancellationToken));

        if (autor is null) return NotFound();

        return autor;
    }

    [HttpPost]
    public async Task<ActionResult<AutorViewModel>> AdicionarAsync(AutorViewModel autorViewModel, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _autorService.AdicionarAsync(_mapper.Map<Autor>(autorViewModel), cancellationToken);

        return CustomResponse(HttpStatusCode.Created, autorViewModel);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AutorViewModel>> AtualizarAsync(int id, AutorViewModel autorViewModel, CancellationToken cancellationToken = default)
    {
        if (id != autorViewModel.Id)
        {
            NotificarErro("O id informado não é o mesmo que foi passado na query");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _autorService.AtualizarAsync(_mapper.Map<Autor>(autorViewModel), cancellationToken);

        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<AutorViewModel>> ExcluirAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!await _autorRepository.ExistsByExpressionAsync(x => x.Id == id, cancellationToken)) 
            return NotFound();

        await _autorService.RemoverAsync(id, cancellationToken);

        return CustomResponse(HttpStatusCode.NoContent);
    }
}