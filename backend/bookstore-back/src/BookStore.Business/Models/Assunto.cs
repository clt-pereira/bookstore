namespace BookStore.Business.Models;

public class Assunto : Entity
{
    public string Descricao { get; set; }
    public ICollection<Livro> Livros { get; set; } = [];
}
