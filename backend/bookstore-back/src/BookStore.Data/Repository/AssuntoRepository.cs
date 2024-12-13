using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using BookStore.Data.Context;

namespace BookStore.Data.Repository;

public class AssuntoRepository(DatabaseContext context) : Repository<Assunto>(context), IAssuntoRepository
{

    //public async Task<Autor> ObterAutorLivro(Guid id)
    //{
    //    return await Db.Autor.AsNoTracking()
    //        .Include(c => c.Endereco)
    //        .FirstOrDefaultAsync(c => c.Id == id);
    //}

    //public async Task<Autor> ObterAutorLivroAssunto(Guid id)
    //{
    //    return await Db.Autor.AsNoTracking()
    //        .Include(c => c.Produtos)
    //        .Include(c => c.Endereco)
    //        .FirstOrDefaultAsync(c => c.Id == id);
    //}

    //public async Task<Endereco> ObterLivroPorAutor(Guid fornecedorId)
    //{
    //    return await Db.Enderecos.AsNoTracking()
    //        .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
    //}
}