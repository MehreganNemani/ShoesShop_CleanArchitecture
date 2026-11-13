using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Features.ProductCategories.Requests.Commands.AddProductCategory;
using ShoesShop.Application.Features.ProductCategories.Requests.Commands.RemoveProductCategory;
using ShoesShop.Application.Features.ProductCategories.Requests.Queries.GetChildCategories;
using ShoesShop.Application.Features.ProductCategories.Requests.Queries.GetParentCategories;
using ShoesShop.Application.Features.ProductCategories.Requests.Queries.GetProductCategories;

namespace ShoesShop.Api.Controllers
{
	[Route("api/Admin/[controller]")]
	[ApiController]
	public class ProductCategoriesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductCategoriesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("parents")]
		public async Task<IActionResult> GetParentCategories()
		{
			var result = await _mediator.Send(new GetParentCategoriesQuery());
			return Ok(result);
		}

		[HttpGet("children/{parentId:int}")]
		public async Task<IActionResult> GetChildCategories(int parentId)
		{
			var result = await _mediator.Send(new GetChildCategoriesQuery
			{
				ParentId = parentId
			});

			return Ok(result);
		}

		[HttpGet("product/{productId:int}")]
		public async Task<IActionResult> GetProductCategories(int productId)
		{
			var result = await _mediator.Send(new GetProductCategoriesQuery
			{
				ProductId = productId
			});

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> AddProductCategory([FromBody] AddProductCategoryCommand command)
		{
			try
			{
				var result = await _mediator.Send(command);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(new
				{
					message = ex.Message
				});
			}
		}

		[HttpDelete("product/{productId:int}/category/{categoryId:int}")]
		public async Task<IActionResult> RemoveProductCategory(int productId, int categoryId)
		{
			var result = await _mediator.Send(new RemoveProductCategoryCommand
			{
				ProductId = productId,
				CategoryId = categoryId
			});

			if (!result)
			{
				return NotFound();
			}

			return NoContent();
		}


	}
}
