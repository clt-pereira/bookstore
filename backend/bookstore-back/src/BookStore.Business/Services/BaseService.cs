using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using BookStore.Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace BookStore.Business.Services;

public abstract class BaseService(INotificador notificador)
{
    private readonly INotificador _notificador = notificador;

    protected void Notificar(ValidationResult validationResult)
    {
        foreach (var item in validationResult.Errors) 
        {
            Notificar(item.ErrorMessage);
        }
    }

    protected void Notificar(string mensagem)
    {
        _notificador.Handle(new Notificacao(mensagem));
    }

    protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) 
        where TV : AbstractValidator<TE>
        where TE : Entity
    {
        var validator = validacao.Validate(entidade);

        if (validator.IsValid) return true;

        Notificar(validator);

        return false;
    }
}
