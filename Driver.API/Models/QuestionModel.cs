using Driver.DTO;

namespace Driver.API.Models
{
	public class QuestionModel
	{
		public long CurrentIndex { get; set; } = 1;
		public QuestionDTO? Question { get; set; }
	}
}
