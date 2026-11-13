using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.ProductTags.Requests.Commands;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.ProductTags.Handlers.Commands.AddProductTag
{
	public sealed class AddProductTagCommandHandler : IRequestHandler<AddProductTagCommand, bool>
	{
		private readonly IRepository<ProductTag> _productTag;
		private readonly IRepository<Product> _product;
		private readonly IRepository<Tag> _tag;
		private readonly IUnitOfWork _unitOfWork;
		public AddProductTagCommandHandler(IRepository<ProductTag> productTag, IRepository<Product> product, IRepository<Tag> tag, IUnitOfWork unitOfWork)
		{
			_productTag = productTag;
			_product = product;
			_tag = tag;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> Handle(AddProductTagCommand request, CancellationToken cancellationToken)
		{
			var productExists = await _product.Query().AnyAsync(p => p.ID == request.ProductId, cancellationToken);

			if (!productExists)
			{
				throw new InvalidOperationException("Product Does Not Exists...");
			}

			var tagExists = await _tag.Query().AnyAsync(c => c.ID == request.TagId, cancellationToken);

			if (!tagExists)
			{
				throw new InvalidOperationException("Tag Does Not Exists...");
			}

			var alreadyExists = await _productTag.Query().AnyAsync(pt => pt.ProductID == request.ProductId && pt.TagID == request.TagId, cancellationToken);

			if (alreadyExists)
			{
				throw new InvalidOperationException("This Tag Already Added To This Product...");
			}

			var productTag = new ProductTag
			{
				ProductID = request.ProductId,
				TagID = request.TagId,
				CreateDate = DateTime.Now,
				IsDeleted = false
			};

			await _productTag.AddAsync(productTag);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
