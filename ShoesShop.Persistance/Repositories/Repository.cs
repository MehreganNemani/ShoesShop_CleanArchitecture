using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Domain.Entities.Common;

namespace ShoesShop.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ShoesShop_DbContext _context;
        protected readonly DbSet<T> DbSet;
        public Repository(ShoesShop_DbContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.ID == id && !x.IsDeleted, cancellationToken);
        }
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
        }
        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }
        public IQueryable<T> Query()
        {
            return _context.Set<T>();
        }
    }
}
