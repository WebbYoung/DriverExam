using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace Driver.API.Extensions
{
	public static class ModelStateExtension
	{
		public static string GetErrorMsg(this ModelStateDictionary modelState)
		{
			if (modelState == null || modelState.IsValid) return string.Empty;
			StringBuilder sb = new StringBuilder();
			foreach (var item in modelState.Values)
			{
				foreach (var error in item.Errors)
				{
					if (!string.IsNullOrEmpty(error.ErrorMessage))
					{
						sb.AppendLine(error.ErrorMessage);
					}
				}
			}
			return sb.ToString();
		}
	}
}
