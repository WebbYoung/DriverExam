using System.ComponentModel.DataAnnotations;

namespace Driver.API.Models
{
	public class AnswerModel
	{
		[Required]
		public long QuestionId { get; set; }
		[Required]
		public char[]? AnswerChars { get; set; }
		[Required]
		public long WrongCount { get; set; }
		[Required]
		public long CorrectCount { get; set; }
	}
}
