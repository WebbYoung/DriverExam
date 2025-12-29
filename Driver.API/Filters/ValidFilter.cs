using Driver.API.Attributes;
using Driver.API.Extensions;
using Driver.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Driver.API.Filters
{
	public class ValidFilter : IAsyncActionFilter
	{
		public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
			if (descriptor == null||!descriptor.MethodInfo.GetCustomAttributes<ValidatedAttribute>().Any())
			{
				next();
				return Task.CompletedTask;
			}
			if (context.ModelState.IsValid)
			{
			    next();
				return Task.CompletedTask;
			}
			string errors = context.ModelState.GetErrorMsg();
			context.Result = new OkObjectResult(new ApiResult { Status = "error", Message = context.ModelState.GetErrorMsg() });
			return Task.CompletedTask;
		}
	}
}
