namespace BookStore.Business.Models;

public class Livro : Entity
{
    public string Titulo { get; set; }
    public string Editora { get; set; }
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; }
    public decimal Valor { get; set; }
    public ICollection<Assunto> Assuntos { get; set; } = [];
    public ICollection<Autor> Autores { get; set; } = [];
}
