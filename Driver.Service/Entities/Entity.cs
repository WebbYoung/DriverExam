using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Service.Entities
{
	public record Entity
	{
		public long Id { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.UtcNow;
		public bool IsDeleted { get; set; }
	}
}
