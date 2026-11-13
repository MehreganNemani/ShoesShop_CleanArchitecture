using Microsoft.AspNetCore.Hosting;
using ShoesShop.Application.Common.Interfaces;

namespace ShoesShop.Persistance.Repositories
{
	public class ImageStorageService : IImageStorageService
	{
		private readonly string _imageDirectory;

		private static readonly HashSet<string> AllowedExceptions = new(StringComparer.OrdinalIgnoreCase)
		{
			".jpg",
			"jpeg",
			".png"
		};

		public ImageStorageService(IWebHostEnvironment enviroment)
		{
			_imageDirectory = Path.Combine(enviroment.WebRootPath ?? Path.Combine(enviroment.ContentRootPath, "wwwroot"), "Images", "Gallery");
		}

		public async Task<string> SaveAsync(Stream fileStream, string originalFileName, CancellationToken cancellationToken = default)
		{
			var extensions = Path.GetExtension(originalFileName);

			if (!AllowedExceptions.Contains(extensions))
			{
				throw new InvalidOperationException("Image Extension Is Not Valid....");
			}

			Directory.CreateDirectory(_imageDirectory);

			var imageName = $"{Guid.NewGuid():N}{extensions.ToLowerInvariant()}";

			var filePath = Path.Combine(_imageDirectory, imageName);

			try
			{
				await using var output = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, useAsync: true);

				await fileStream.CopyToAsync(output, cancellationToken);

				return imageName;
			}
			catch
			{
				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}

				throw;
			}
		}

		public Task DeleteAsync(string imageName, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			if (string.IsNullOrWhiteSpace(imageName) || !string.Equals(Path.GetFileName(imageName), imageName, StringComparison.Ordinal))
			{
				throw new ArgumentException("File Name Is Not Valid...", nameof(imageName));
			}

			var filePath = Path.Combine(_imageDirectory, imageName);

			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}

			return Task.CompletedTask;
		}
	}
}
