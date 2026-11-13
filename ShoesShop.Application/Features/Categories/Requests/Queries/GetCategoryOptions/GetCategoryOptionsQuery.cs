using MediatR;
using ShoesShop.Application.DTOs.Categories;

namespace ShoesShop.Application.Features.Categories.Requests.Queries.GetCategoryOptions
{
	public sealed record GetCategoryOptionsQuery : IRequest<List<CategoryOptionDto>>;
}
