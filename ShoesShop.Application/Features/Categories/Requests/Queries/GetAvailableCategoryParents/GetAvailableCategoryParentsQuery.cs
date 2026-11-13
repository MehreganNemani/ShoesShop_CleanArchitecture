using MediatR;
using ShoesShop.Application.DTOs.Categories;

namespace ShoesShop.Application.Features.Categories.Requests.Queries.GetAvailableCategoryParents
{
	public sealed record GetAvailableCategoryParentsQuery(int categoryId) : IRequest<IReadOnlyList<CategoryOptionDto>>;
}
