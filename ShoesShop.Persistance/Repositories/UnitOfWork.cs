using ShoesShop.Application.Common.Interfaces;

namespace ShoesShop.Persistance.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly ShoesShop_DbContext _context;
		public UnitOfWork(ShoesShop_DbContext context)
		{
			_context = context;
		}

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return _context.SaveChangesAsync(cancellationToken);
		}
	}
}
