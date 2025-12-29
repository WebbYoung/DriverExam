using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Service.Entities
{
	public record User:Entity
	{
		public required string PhoneNumber { get; set; }
		public required string PasswordHash { get; set; }
		public required string PasswordSalt { get; set; }
		public bool IsVIP { get; set; }
	}
}
