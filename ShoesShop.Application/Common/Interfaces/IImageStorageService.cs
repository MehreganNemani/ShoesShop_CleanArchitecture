namespace ShoesShop.Application.Common.Interfaces
{
	public interface IImageStorageService
	{
		Task<string> SaveAsync(Stream fileStream, string originalFileName, CancellationToken cancellationToken = default);
		Task DeleteAsync(string imageName, CancellationToken cancellationToken = default);
	}
}
