using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Features.Tags.Requests.Commands;
using ShoesShop.Application.Features.Tags.Requests.Queries.GetAllTags;
using ShoesShop.Application.Features.Tags.Requests.Queries.GetTagById;
using ShoesShop.Application.Features.Tags.Requests.Queries.GetTags;

namespace ShoesShop.Api.Controllers
{
	[Route("api/Admin/[controller]")]
	[ApiController]
	public class TagsController : ControllerBase
	{
		private readonly IMediator _mediator;
		public TagsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET: api/Admin/<TagsController>
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] string? search,
												[FromQuery] int pageNumber = 1,
												[FromQuery] int pageSize = 10,
												CancellationToken cancellationToken = default)
		{
			var query = new GetTagsQuery(search, pageNumber, pageSize);

			var result = await _mediator.Send(query, cancellationToken);

			return Ok(result);
		}

		// GET api/Admin/<TagsController>/5
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
		{
			var query = new GetTagByIdQuery(id);

			var result = await _mediator.Send(query, cancellationToken);

			if (result == null)
			{
				return NotFound();
			}

			return Ok(result);
		}

		// POST api/Admin/<TagsController>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateTagCommand command, CancellationToken cancellationToken)
		{
			var id = await _mediator.Send(command, cancellationToken);

			return Ok(new
			{
				Id = id
			});
		}

		// PUT api/Admin/<TagsController>/5
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateTagCommand request, CancellationToken cancellationToken)
		{

			var command = new UpdateTagCommand(id, request.TagTitle);

			var result = await _mediator.Send(command, cancellationToken);

			if (!result)
			{
				return NotFound();
			}

			return NoContent();

		}

		// DELETE api/Admin/<TagsController>/5
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var command = new DeleteTagCommand(id);

			var result = await _mediator.Send(command, cancellationToken);

			if (!result)
			{
				return NotFound();
			}

			return NoContent();
		}

		[HttpGet("GetAllTags")]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			var tags = await _mediator.Send(new GetAllTagsQuery(), cancellationToken);
			return Ok(tags);
		}
	}
}
