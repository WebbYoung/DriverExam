using Driver.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Service.ValueObjects
{
	public record Option
	{
		public char OptionChar { get; set; }
		public string Content { get; set; } = string.Empty;
		public bool IsAnswer { get; set; }
	}
}
