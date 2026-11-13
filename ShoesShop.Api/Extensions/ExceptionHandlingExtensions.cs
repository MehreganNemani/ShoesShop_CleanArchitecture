using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using ShoesShop.Application.Exceptions;
using System.Net;

namespace ShoesShop.Api.Extensions
{
	public static class ExceptionHandlingExtensions
	{
		public static void UseGlobalExceptionHandling(this WebApplication app)
		{
			app.UseExceptionHandler(errorApp =>
			{
				errorApp.Run(async context =>
				{
					var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

					if (exceptionFeature?.Error is BadRequestException badRequestException)
					{
						context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
						context.Response.ContentType = "application/json";
						await context.Response.WriteAsJsonAsync(new
						{
							Message = badRequestException.Message
						});

						return;
					}

					if (exceptionFeature?.Error is ValidationException validationException)
					{
						context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
						context.Response.ContentType = "application/json";

						var errors = validationException.Errors.GroupBy(c => c.PropertyName).ToDictionary(group => group.Key, group => group.Select(c => c.ErrorMessage).ToArray());

						await context.Response.WriteAsJsonAsync(new
						{
							Errors = errors
						});

						return;
					}

					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				});
			});
		}
	}
}
