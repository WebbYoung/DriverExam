using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace Driver.API.Models
{
	public class QuestionInfoModel
	{
		public long TotalCount { get; set; }
		public long CorrectCount { get; set; }
		public long WrongCount { get; set; }
		public int CorrectRate { get; set; }
		public int Progress { get; set; }
	}
}
