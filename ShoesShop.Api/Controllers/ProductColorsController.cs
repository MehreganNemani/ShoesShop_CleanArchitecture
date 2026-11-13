using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Api.Contracts.ProductColors;
using ShoesShop.Application.Features.ProductColors.Requests.Commands.AddProductColor;
using ShoesShop.Application.Features.ProductColors.Requests.Commands.RemoveProductColor;
using ShoesShop.Application.Features.ProductColors.Requests.Queries.GetProductColors;

namespace ShoesShop.Api.Controllers
{
	[Route("api/Admin/[controller]")]
	[ApiController]
	public class ProductColorsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductColorsController(IMediator mediator)
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

			var result = await _mediator.Send(new GetProductColorsQuery(productId), cancellationToken);

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] AddProductColorRequest request)
		{
			try
			{
				var command = new AddProductColorCommand
				{
					ProductId = request.ProductID,
					ColorId = request.ColorID
				};

				var result = await _mediator.Send(command);

				if (!result)
				{
					return BadRequest("Create New Color For Product Failed...");
				}

				return Ok("Create New Color For Product Successfully...");
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(new
				{
					message = ex.Message
				});
			}
		}

		[HttpDelete("{productId:int}/{colorId:int}")]
		public async Task<IActionResult> Remove(int productId, int colorId, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new RemoveProductColorCommand
			{
				ProductID = productId,
				ColorID = colorId
			}, cancellationToken);

			if (!result)
			{
				return NotFound("Color Did Not Exists For This Product...");
			}

			return NoContent();
		}
	}
}
