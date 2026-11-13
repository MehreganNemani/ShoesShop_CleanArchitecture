using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Api.Contracts.ProductTags;
using ShoesShop.Application.Features.ProductTags.Requests.Commands;
using ShoesShop.Application.Features.ProductTags.Requests.Queries.GetProductTags;

namespace ShoesShop.Api.Controllers
{
	[Route("api/Admin/[controller]")]
	[ApiController]
	public class ProductTagsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductTagsController(IMediator mediator)
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

			var result = await _mediator.Send(new GetProductTagsQuery(productId), cancellationToken);

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] AddProductTagRequest request)
		{
			try
			{
				var command = new AddProductTagCommand
				{
					ProductId = request.ProductID,
					TagId = request.TagID
				};

				var result = await _mediator.Send(command);

				if (!result)
				{
					return BadRequest("Create New Tag For Product Failed...");
				}

				return Ok("Create New Tag For Product Successfully...");
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(new
				{
					message = ex.Message
				});
			}
		}

		[HttpDelete("{productId:int}/{TagId:int}")]
		public async Task<IActionResult> Remove(int productId, int TagId, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new RemoveProductTagCommand
			{
				ProductId = productId,
				TagId = TagId
			}, cancellationToken);

			if (!result)
			{
				return NotFound("Tag Did Not Exists For This Product...");
			}

			return NoContent();
		}
	}
}
