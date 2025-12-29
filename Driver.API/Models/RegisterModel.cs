using System.ComponentModel.DataAnnotations;

namespace Driver.API.Models
{
	public class RegisterModel
	{
		[Required]
		[Phone]
		[StringLength(15, MinimumLength = 10)]
		public string? PhoneNumber { get; set; }
		[Required]
		[StringLength(100, MinimumLength = 6)]
		public string? Password { get; set; }
		[Required]
		[Compare(nameof(Password))]
		public string? ConfirmPassword { get; set; }
		[Required]
		[StringLength(32, MinimumLength = 32)]
		public string? VerifyCodeId { get; set; }
		[Required]
		[StringLength(4, MinimumLength = 4)]
		public string? VerifyCode { get; set; }
		[Required]
		[StringLength(6, MinimumLength = 6)]
		public string? SMSCode { get; set; }
	}
}
