namespace ShoesShop.Application.Common.Interfaces
{
	public interface ICategoryRepository
	{
		Task<bool> IsExistAsync(int id, CancellationToken cancellationToken = default);
		Task<bool> IsNameExistAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default);
		Task<bool> IsCodeExistAsync(int code, int? excludeId = null, CancellationToken cancellationToken = default);
		Task<bool> ParentExistsAsync(int parentId, CancellationToken cancellationToken = default);
		Task<bool> IsChildOfAsync(int categoryId, int potentialParentId, CancellationToken cancellationToken = default);
		Task<bool> HasChilderAsync(int categoryId, CancellationToken cancellationToken = default);
		Task<IReadOnlyList<int>> GetDescendantIdsAsync(int categoryId, CancellationToken cancellationToken);
	}
}
