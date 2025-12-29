namespace Driver.API.Models
{
	public class DetermineQuestionModel
	{
		public bool IsCorrect { get; set; }
		public long WrongCount { get; set; } = 0;
		public long CorrectCount { get; set; } = 0;
		public string? CorrectAnswer { get; set; }
	}
}
