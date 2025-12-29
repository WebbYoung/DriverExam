using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Service
{
	public class JWTSettings
	{
		public string? SecKey { get; set; }
		public int ExpiredTime { get; set; }
	}
}
