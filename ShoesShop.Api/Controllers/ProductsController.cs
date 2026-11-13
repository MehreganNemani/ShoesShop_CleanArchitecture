using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Features.Products.Requests.Commands;
using ShoesShop.Application.Features.Products.Requests.Queries.GetProductById;
using ShoesShop.Application.Features.Products.Requests.Queries.GetProducts;

namespace ShoesShop.Api.Controllers
{
	[Route("api/Admin/[controller]")]
	[ApiController]
	public sealed class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;
		public ProductsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET: api/Admin/<ProductsController>
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] string? search,
												[FromQuery] int pageNumber = 1,
												[FromQuery] int pageSize = 10,
												CancellationToken cancellationToken = default)
		{
			var query = new GetProductsQuery(search, pageNumber, pageSize);

			var result = await _mediator.Send(query, cancellationToken);

			return Ok(result);
		}

		// GET api/Admin/<ProductsController>/5
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
		{
			var query = new GetProductByIdQuery(id);

			var result = await _mediator.Send(query, cancellationToken);

			if (result == null)
			{
				return NotFound();
			}

			return Ok(result);
		}

		// POST api/Admin/<ProductsController>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
		{
			var id = await _mediator.Send(command, cancellationToken);

			return Ok(new
			{
				Id = id
			});
		}

		// PUT api/Admin/<ProductsController>/5
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand request, CancellationToken cancellationToken)
		{

			var command = new UpdateProductCommand(id, request.ProductName, request.ProductCode,
													   request.ProductModel, request.ProductPrice, request.ProductDesc,
													   request.ShowInSlider, request.IsAvailable);

			var result = await _mediator.Send(command, cancellationToken);

			if (!result)
			{
				return NotFound();
			}

			return NoContent();

		}

		// DELETE api/Admin/<ProductsController>/5
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var command = new DeleteProductCommand(id);

			var result = await _mediator.Send(command, cancellationToken);

			if (!result)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
