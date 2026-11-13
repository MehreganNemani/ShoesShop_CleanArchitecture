using ShoesShop.Domain.Entities.Common;

namespace ShoesShop.Application.Common.Interfaces
{
	public interface IRepository<T> where T : BaseEntity
	{
		IQueryable<T> Query();
		Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task AddAsync(T entity, CancellationToken cancellationToken = default);
		void Update(T entity);
		void Delete(T entity);
	}
}
