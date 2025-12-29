using Driver.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Service.Services
{
	public class TokenService:ITokenService
	{
		private readonly IOptionsSnapshot<JWTSettings> options;
		public TokenService(IOptionsSnapshot<JWTSettings> options)
		{
			this.options = options;
		}
		public string CreateJWT(List<Claim> claims)
		{
			string? key = options.Value.SecKey;
		    DateTime expires = DateTime.UtcNow.AddMinutes(options.Value.ExpiredTime);
			if(string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
			byte[] secBytes = Encoding.UTF8.GetBytes(key);
			var secKey = new SymmetricSecurityKey(secBytes);
			var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
			var tokenDescrptor = new JwtSecurityToken(claims:claims, expires:expires,signingCredentials:credentials);
			return new JwtSecurityTokenHandler().WriteToken(tokenDescrptor);
		}
	}
}
