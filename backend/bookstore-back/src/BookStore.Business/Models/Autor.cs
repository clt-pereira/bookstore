
namespace BookStore.Business.Models;

public class Autor : Entity
{
    public string Nome { get; set; }
    public ICollection<Livro> Livros { get; set; } = [];
}
