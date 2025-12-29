using Driver.Common;
using Driver.DTO;
using Driver.IService;
using Driver.Service.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Driver.Service.Services
{
	public class UserService : IUserService
	{
		public async Task<bool> CheckLoginAsync(string phoneNumber, string password)
		{
			using(var context=new DriverDbContext())
			{
				var user = await context.Users.AsNoTracking()
				.SingleOrDefaultAsync(e => e.PhoneNumber == phoneNumber);
				if (user == null) return false;
				string salt = user.PasswordSalt;
				string hashedPassword = CommonHelper.CalcMD5(salt + password);
				return hashedPassword == user.PasswordHash;
			}
		}
		public async Task<UserDTO?> GetByPhoneNumberAsync(string phoneNumber)
		{
			using(var context=new DriverDbContext())
			{
				var user = await context.Users.AsNoTracking()
				.SingleOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
				return user == null ? null : ToDTO(user);
			}
		}
		public static UserDTO ToDTO(User user)
		{
			var dto = new UserDTO
			{
				Id = user.Id,
				PhoneNumber = user.PhoneNumber,
				IsVIP = user.IsVIP,
				CreateDate= user.CreateDate
			};
			return dto;
		}

		public async Task AddUserAsync(string phoneNumber, string password)
		{
			using(var context=new DriverDbContext())
			{
				if (await context.Users.AsNoTracking().AnyAsync(e => e.PhoneNumber == phoneNumber))
				{
					throw new ArgumentException($"手机号已被注册:{phoneNumber}");
				}
				string salt = CommonHelper.CreateVerifyCode(6);
				string hashedPassword = CommonHelper.CalcMD5(salt + password);
				User user = new User
				{
					PhoneNumber = phoneNumber,
					PasswordSalt = salt,
					PasswordHash = hashedPassword,
					IsVIP = false
				};
				await context.Users.AddAsync(user);
				if (await context.SaveChangesAsync() != 1)
				{
					throw new ArgumentException("添加用户失败");
				}
			}
		}

		public async Task<UserDTO?> GetByIdAsync(long id)
		{
			using(var context=new DriverDbContext())
			{
				var user = await context.Users.AsNoTracking()
					.SingleOrDefaultAsync(e=>e.Id==id);
				return user == null ? null : ToDTO(user);
			} 
		}
	}
}
