using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.DTO
{
	public record OptionDTO(char OptionChar, string? Content,bool IsAnswer);
}
