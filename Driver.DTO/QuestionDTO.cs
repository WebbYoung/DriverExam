using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.DTO
{
	public class QuestionDTO:DTO
	{
		public string? Title { get; set; }
		public string? QuestionType { get; set; }
		public OptionDTO[]? Options { get; set; }
	}
	
}
