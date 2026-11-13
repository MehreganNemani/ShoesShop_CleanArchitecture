using MediatR;

namespace ShoesShop.Application.Features.Products.Requests.Commands
{
	public sealed record CreateProductCommand(string ProductName , 
											  string ProductCode , string ProductModel , 
											  decimal ProductPrice , string ProductDesc ,
											  bool ShowInSlider , bool IsAvailable) : IRequest<int>;
}
