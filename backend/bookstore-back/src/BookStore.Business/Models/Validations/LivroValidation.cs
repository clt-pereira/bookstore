using FluentValidation;

namespace BookStore.Business.Models.Validations;

public class LivroValidation : AbstractValidator<Livro>
{
    public LivroValidation()
    {
        RuleFor(f => f.Titulo)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 40).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        //RuleFor(f => f.Editora)
        //    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
        //    .Length(2, 40).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        //RuleFor(c => c.Edicao)
        //    .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");

        //RuleFor(f => f.AnoPublicacao)
        //    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
        //    .Length(2, 4).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        //RuleFor(c => c.Valor)
        //    .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
    }
}
