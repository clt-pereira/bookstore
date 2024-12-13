using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.ViewModels;

public class AutorViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(40, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get; set; }
}
