using Driver.DTO;
using System.ComponentModel.DataAnnotations;

namespace Driver.API.Models
{
	public class QuestionProgressModel
	{
		[Required]
		public long CurrentId { get; set; }
		[Required]
		public long CurrentIndex { get; set; } = 1;
		[Required]
		public long TotalCount { get; set; }
	}
}
