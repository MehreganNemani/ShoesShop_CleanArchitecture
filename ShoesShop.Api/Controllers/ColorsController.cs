using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Features.Colors.Requests.Commands;
using ShoesShop.Application.Features.Colors.Requests.Queries.GetColorById;
using ShoesShop.Application.Features.Colors.Requests.Queries.GetColors;

namespace ShoesShop.Api.Controllers
{
	[Route("api/Admin/[controller]")]
	[ApiController]
	//[Authorize]
	public sealed class ColorsController : ControllerBase
	{
		private readonly IMediator _mediator;
		public ColorsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET: api/Admin/<ColorsController>
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] string? search,
												[FromQuery] int pageNumber = 1,
												[FromQuery] int pageSize = 10,
												CancellationToken cancellationToken = default)
		{
			var query = new GetColorsQuery(search, pageNumber, pageSize);

			var result = await _mediator.Send(query, cancellationToken);

			return Ok(result);
		}

		// GET api/Admin/<ColorsController>/5
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
		{
			var query = new GetColorByIdQuery(id);

			var result = await _mediator.Send(query, cancellationToken);

			if (result == null)
			{
				return NotFound();
			}

			return Ok(result);
		}

		// POST api/Admin/<ColorsController>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateColorCommand command, CancellationToken cancellationToken)
		{
			var id = await _mediator.Send(command, cancellationToken);

			return Ok(new
			{
				Id = id
			});
		}

		// PUT api/Admin/<ColorsController>/5
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateColorCommand request, CancellationToken cancellationToken)
		{
			var command = new UpdateColorCommand(id, request.ColorName);

			var result = await _mediator.Send(command, cancellationToken);

			if (!result)
			{
				return NotFound();
			}

			return NoContent();

		}

		// DELETE api/Admin/<ColorsController>/5
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var command = new DeleteColorCommand(id);

			var result = await _mediator.Send(command, cancellationToken);

			if (!result)
			{
				return NotFound();
			}

			return NoContent();
		}

		[HttpGet("GetAllColors")]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			var colors = await _mediator.Send(new GetAllColorsQuery(), cancellationToken);
			return Ok(colors);
		}
	}
}
