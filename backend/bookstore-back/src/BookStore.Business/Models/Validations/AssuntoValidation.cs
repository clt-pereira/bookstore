using FluentValidation;

namespace BookStore.Business.Models.Validations;

public class AssuntoValidation : AbstractValidator<Assunto>
{
    public AssuntoValidation()
    {
        RuleFor(f => f.Descricao)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}
