namespace Driver.Common
{
	public class ApiResult
	{
		public required string Status { get; set; }
		public string Message { get; set; } = string.Empty;
		public object? Data { get; set; }
	}
}
