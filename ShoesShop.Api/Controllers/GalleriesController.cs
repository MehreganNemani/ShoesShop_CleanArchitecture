using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Api.Contracts.Gallery;
using ShoesShop.Application.Features.Galleries.Requests.Commands;
using ShoesShop.Application.Features.Galleries.Requests.Queries.GetGalleryByProductId;

namespace ShoesShop.Api.Controllers
{
	[Route("api/Admin/[controller]")]
	[ApiController]
	public sealed class GalleriesController : ControllerBase
	{
		private IMediator _mediator;

		public GalleriesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("product/{productID:int}")]
		public async Task<ActionResult<List<GalleryImageDto>>> GetByProduct(int productID, CancellationToken cancellationToken)
		{
			if (productID <= 0)
			{
				return BadRequest("Product Is Not Exists...");
			}

			var query = new GetGalleryByProductIdQuery(productID);

			var images = await _mediator.Send(query, cancellationToken);

			var result = images.Select(g => new GalleryImageDto
			{
				Id = g.ID,
				ProductId = g.ProductID,
				ImageName = g.ImageName,
				IsMain = g.IsMain,
				CreateDate = g.CreateDate
			}).ToList();

			return Ok(result);
		}

		[HttpPost("product/{productID:int}")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Add(int productID, [FromForm] List<IFormFile> images, CancellationToken cancellationToken)
		{

			var command = new AddGalleryImageCommand
			{
				productId = productID,
				Images = images
			};

			await _mediator.Send(command, cancellationToken);

			return Ok(new { message = "Images Save Successfully..." });
		}

		[HttpDelete("{Id:int}")]
		public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
		{
			var deleted = await _mediator.Send(new DeleteGalleryImageCommand { Id = Id }, cancellationToken);

			if (!deleted)
			{
				return NotFound("Photo Not Found...");
			}

			return Ok(new { message = "Photo Has Been Deleted..." });
		}

		[HttpPut("{Id:int}/main")]
		public async Task<IActionResult> SetMain(int Id, CancellationToken cancellationToken)
		{
			var updated = await _mediator.Send(new SetMainGalleryImageCommand { Id = Id }, cancellationToken);

			if (!updated)
			{
				return NotFound("Photo Not Found...");
			}

			return Ok(new { message = "Main Photo Has Been Changed..." });
		}
	}
}
