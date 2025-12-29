using Driver.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Driver.API.Filters
{
	public class GlobalExceptionFilter : IAsyncExceptionFilter
	{
		public Task OnExceptionAsync(ExceptionContext context)
		{
			//todo:写入日志文件
			Console.WriteLine(context.Exception.Message);
			context.Result = new OkObjectResult(new ApiResult { Status = "error", Message = "服务端发生异常", Data = null });
			context.ExceptionHandled = true;
			return Task.CompletedTask;
		}
	}
}
