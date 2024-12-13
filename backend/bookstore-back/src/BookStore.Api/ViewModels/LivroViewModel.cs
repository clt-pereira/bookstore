using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.ViewModels;

public class LivroViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(40, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(40, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Editora { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int Edicao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(40, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 4)]
    public string AnoPublicacao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public ICollection<AssuntoViewModel> Assuntos { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public ICollection<AutorViewModel> Autores { get; set; }
}
