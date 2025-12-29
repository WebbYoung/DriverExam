using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Driver.IService
{
	public interface ITokenService
	{
		string CreateJWT(List<Claim> claims);
	}
}
