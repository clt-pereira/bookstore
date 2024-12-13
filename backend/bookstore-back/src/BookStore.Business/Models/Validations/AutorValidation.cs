using FluentValidation;

namespace BookStore.Business.Models.Validations;

public class AutorValidation : AbstractValidator<Autor>
{
    public AutorValidation()
    {
        RuleFor(f => f.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 40).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}
