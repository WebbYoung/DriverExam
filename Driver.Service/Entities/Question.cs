using Driver.DTO;
using Driver.Service.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Service.Entities
{
	public record Question:Entity
	{
		public string Title { get; set; } = string.Empty;
		public List<Option> Options { get; set; } = new List<Option>();
		public QuestionType QuestionType { get; set; }
		public string? PicPath { get; set; }
	}
	public enum QuestionType
	{
		Single = 0,
		Multiple = 1,
		Judgement = 2
	}
}
