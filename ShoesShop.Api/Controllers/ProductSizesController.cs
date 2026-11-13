using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Api.Contracts.ProductSizes;
using ShoesShop.Application.Features.ProductSizes.Requests.Commands;
using ShoesShop.Application.Features.ProductSizes.Requests.Queries.GetAllSizes;
using ShoesShop.Application.Features.ProductSizes.Requests.Queries.GetProductSizes;

namespace ShoesShop.Api.Controllers
{
	[Route("api/Admin/[controller]")]
	[ApiController]
	public class ProductSizesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductSizesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{productId:int}")]
		public async Task<IActionResult> GetByProductId(int productId, CancellationToken cancellationToken)
		{
			if (productId <= 0)
			{
				return BadRequest("Product ID Is Not Valid...");
			}

			var result = await _mediator.Send(new GetProductSizesQuery(productId), cancellationToken);

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] AddProductSizeRequest request)
		{
			try
			{
				var command = new AddProductSizeCommand
				{
					ProductId = request.ProductID,
					SizeId = request.SizeID
				};

				var result = await _mediator.Send(command);

				if (!result)
				{
					return BadRequest("Create New Size For Product Failed...");
				}

				return Ok("Create New Size For Product Successfully...");
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(new
				{
					message = ex.Message
				});
			}
		}

		[HttpDelete("{productId:int}/{sizeId:int}")]
		public async Task<IActionResult> Remove(int productId, int sizeId, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new RemoveProductSizeCommand
			{
				ProductID = productId,
				SizeID = sizeId
			}, cancellationToken);

			if (!result)
			{
				return NotFound("Size Did Not Exists For This Product...");
			}

			return NoContent();
		}

		[HttpGet("GetAllSizes")]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			var sizes = await _mediator.Send(new GetAllSizesQuery(), cancellationToken);
			return Ok(sizes);
		}
	}
}
