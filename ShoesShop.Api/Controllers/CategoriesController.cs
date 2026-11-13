using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Api.Contracts.Categories;
using ShoesShop.Application.Features.Categories.Requests.Commands;
using ShoesShop.Application.Features.Categories.Requests.Queries.GetAllCategories;
using ShoesShop.Application.Features.Categories.Requests.Queries.GetAvailableCategoryParents;
using ShoesShop.Application.Features.Categories.Requests.Queries.GetCategoryById;
using ShoesShop.Application.Features.Categories.Requests.Queries.GetCategoryOptions;

namespace ShoesShop.Api.Controllers
{
	[Route("api/Admin/[controller]")]
	[ApiController]
	public sealed class CategoriesController : ControllerBase
	{
		private readonly IMediator _mediator;
		public CategoriesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET: api/Admin/Categories
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] string? search, CancellationToken cancellationToken)
		{
			var query = new GetCategoriesQuery(search);

			var result = await _mediator.Send(query, cancellationToken);

			return Ok(result);
		}

		// GET api/Admin/Categories/5
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
		{
			var query = new GetCategoryByIdQuery(id);

			var result = await _mediator.Send(query, cancellationToken);

			if (result == null)
			{
				return NotFound();
			}

			return Ok(result);
		}

		[HttpGet("options")]
		public async Task<IActionResult> GetOptions(CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetCategoryOptionsQuery(), cancellationToken);
			return Ok(result);
		}

		[HttpGet("{id:int}/available-parents")]
		public async Task<IActionResult> GetAvailableParents(int id, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetAvailableCategoryParentsQuery(id), cancellationToken);
			return Ok(result);
		}
		// POST api/Admin/Categories
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
		{
			var id = await _mediator.Send(command, cancellationToken);

			return CreatedAtAction(nameof(GetById), new { id }, new { id });
		}

		// PUT api/Admin/Categories/5
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
		{
			var command = new UpdateCategoryCommand(id, request.CategoryName, request.ParentId, request.Code);

			await _mediator.Send(command, cancellationToken);

			return NoContent();
		}

		// DELETE api/Admin/<CategoriesController>/5
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);

			return NoContent();
		}
	}
}
