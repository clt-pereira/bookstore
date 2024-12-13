using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using BookStore.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Data.Repository;

public abstract class Repository<TEntity>(DatabaseContext db) : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly DatabaseContext Db = db;
    protected readonly DbSet<TEntity> DbSet = db.Set<TEntity>();

    public virtual async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default) 
        => await DbSet.FindAsync(id, cancellationToken);

    public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) 
        => await DbSet.AsNoTracking().ToListAsync(cancellationToken);

    public virtual async Task<IEnumerable<TEntity>> FindByExpressionAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) 
        => await DbSet.Where(predicate).ToListAsync(cancellationToken);

    public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {        
        DbSet.Add(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(new TEntity { Id = id });
        await SaveChangesAsync(cancellationToken);
    }
    
    public async Task<bool> ExistsByExpressionAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking().AnyAsync(predicate, cancellationToken);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await Db.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Db.Dispose();
    }
}
