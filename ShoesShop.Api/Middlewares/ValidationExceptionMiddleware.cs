using FluentValidation;
using System.Text.Json;

namespace ShoesShop.Api.Middlewares
{
	public sealed class ValidationExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		public ValidationExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (ValidationException ex)
			{
				context.Response.StatusCode = StatusCodes.Status400BadRequest;

				context.Response.ContentType = "application/json; charset=utf-8";

				var errors = ex.Errors
					.GroupBy(g => g.PropertyName)
					.ToDictionary(group => group.Key,
								  group => group
								  .Select(g => g.ErrorMessage)
								  .Distinct().ToArray());

				await context.Response.WriteAsync(JsonSerializer.Serialize(new
				{
					message = "Request Is Not Valid...",
					errors
				}));
			}
		}
	}
}
