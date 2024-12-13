using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.ViewModels;

public class AssuntoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(20, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Descricao { get; set; }
}
