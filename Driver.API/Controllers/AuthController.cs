using CaptchaGen.NetCore;
using Driver.API.Attributes;
using Driver.API.Extensions;
using Driver.API.Models;
using Driver.Common;
using Driver.IService;
using Driver.Service;
using Driver.Service.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
namespace Driver.API.Controllers
{
	/// <summary>
	/// 这边我就200派吧，原因是Ajax error返回的好像不是个JSON对象,不知道咋搞
	/// </summary>
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		//private readonly ITokenService tokenService;
		private readonly IUserService userService;
		private readonly IMemoryCache memoryCache;
		private readonly ITokenService tokenService;
		public AuthController(IUserService userService,IMemoryCache memoryCache,ITokenService tokenService)
		{
			this.userService = userService;
			this.memoryCache = memoryCache;
			this.tokenService = tokenService;
		}
		/// <summary>
		/// 用户登录
		/// </summary>
		/// <param name = "request" > 登录模型类 </ param >
		/// < returns > ApiResult模型 </ returns >
		/// < exception cref="ArgumentNullException">找不到用户异常</exception>
		[HttpPost("login")]
		[Validated]
		public async Task<IActionResult> Login(LoginModel request)
		{
			//这里DataAnnotations已经做了基本的验证,应该不会为空
			string phone = request.PhoneNumber!;
			//检查验证码
			string cacheKey="verify-code_"+request.VerifyCodeId;
			string? verifyCode = memoryCache.Get<string>(cacheKey);
			memoryCache.Remove(cacheKey);
			if (verifyCode != request.VerifyCode)
				return Ok(new ApiResult { Status = "error", Message = "验证码错误" });
			//检查手机号和密码
			if (!await userService.CheckLoginAsync(phone, request.Password!))
				return Ok(new ApiResult { Status = "error", Message = "手机号或密码错误" });
			var user = await userService.GetByPhoneNumberAsync(phone) ?? throw new ArgumentNullException($"找不到用户:{phone}");
			//老杨选择了存数据库，这里我先存缓存里
			memoryCache.Set($"login_{user.Id}",DateTime.UtcNow);
			List<Claim> claims =
			[
				new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
				new Claim(ClaimTypes.MobilePhone,phone),
				new Claim(ClaimTypes.Role,user.IsVIP?"vip":"user")
			];
			string jwt = tokenService.CreateJWT(claims);
			return Ok(new ApiResult { Status = "redirect", Message = "登录成功" ,Data=jwt});
		}
		[Authorize]
		[HttpDelete("logout")]
		public IActionResult Logout()
		{
			string? userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
			if(string.IsNullOrEmpty(userId))
				throw new ArgumentNullException(nameof(userId));
			memoryCache.Remove($"login_{userId}");
			return Ok(new ApiResult { Status="redirect",Message="登出成功"});
		}
		/// <summary>
		/// 注册接口
		/// </summary>
		/// <param name="request">注册模型</param>
		/// <returns></returns>
		[HttpPost("register")]
		[Validated]
		public async Task<IActionResult> Register(RegisterModel request)
		{
			string phone = request.PhoneNumber!;
			string cacheKey= "verify-code_"+request.VerifyCodeId!;
			//读取缓存校验验证码
			string? verifyCode = memoryCache.Get<string>(cacheKey);
			memoryCache.Remove(cacheKey);
			if (verifyCode != request.VerifyCode)
				return Ok(new ApiResult { Status = "error", Message = "验证码错误" });
			string? smsCode = memoryCache.Get<string>($"sms-code_{phone}");
			if (string.IsNullOrEmpty(smsCode))
				return Ok(new ApiResult { Status = "error", Message = "验证码失效，请重新发送" });
			if (request.SMSCode != smsCode)
				return Ok(new ApiResult { Status = "error", Message = "短信验证码错误" });
			memoryCache.Remove($"smscode_{phone}");
			//判断用户是否存在
			var userExists = await userService.GetByPhoneNumberAsync(phone) != null;
			//存在返回手机号已存在
			if (userExists) return Ok(new ApiResult { Status = "error", Message = "手机号已注册" });
			//添加用户，注册成功，不成功抛异常由G...filter捕获并返回error
			await userService.AddUserAsync(phone, request.Password!);
			return Ok(new ApiResult { Status = "redirect", Message = "注册成功" });
		}
		/// <summary>
		/// 验证码接口
		/// </summary>
		/// <returns>验证码的GUID</returns>
		[HttpPost("verify-code")]
		public async Task<IActionResult> GenerateVerifyCode()
		{
			//生成4位验证码
			string code=CommonHelper.CreateVerifyCode(4);
			string clientId= Guid.NewGuid().ToString("N");
			string token = "verify-code_" + clientId;
			memoryCache.Set(token,code,TimeSpan.FromMinutes(10));
			Console.WriteLine($"生成验证码:{code}");
			return Ok(new ApiResult { Status="ok",Message="生成验证码成功",Data=clientId});
		}
		/// <summary>
		/// 获取验证码图片
		/// </summary>
		/// <param name="clientId">验证码的GUID</param>
		/// <returns>验证码图片</returns>

		[HttpGet("verify-code/{clientId}")]
		[Validated]
		public async Task<IActionResult> GenerateVerifyCodeImage([Required][MinLength(32)][MaxLength(32)]string clientId)
		{
			if(memoryCache.TryGetValue("verify-code_"+clientId,out string? code))
			{
				if (string.IsNullOrEmpty(code))
					return Ok(new ApiResult { Status = "error", Message = "获取验证码失败" });
				var stream = ImageFactory.BuildImage(code, 40, 100, 20, 6);
				return File(stream, "image/jpeg");
			}
			else return Ok(new ApiResult { Status = "error", Message = "验证码无效" });
		}

		[HttpPost("sms-code")]
		public IActionResult SMSCode()
		{
			return Ok(new ApiResult { Status = "error", Message = "请输入手机号" });
		}
		/// <summary>
		/// 发送短信验证码接口
		/// </summary>
		/// <param name = "phoneNumber" > 发送验证码的手机号 </ param >
		/// < returns ></ returns >
		[HttpPost("sms-code/{phoneNumber}")]
		[Validated]
		public async Task<IActionResult> SMSCode([Required][StringLength(15, MinimumLength = 10)] string phoneNumber)
		{
			string key = $"sms-code_{phoneNumber}";
			if(memoryCache.TryGetValue(key,out string? smsCode)&&smsCode is not null)
			{
				return Ok(new ApiResult { Status = "error", Message = "请求过于频繁，请稍后重试" });
			}
			//生成6位验证码
			string code = CommonHelper.CreateSMSCode(6);
			//to do:请求发送验证码服务
			memoryCache.Set($"sms-code_{phoneNumber}", code, TimeSpan.FromSeconds(60));
			//测试用：输出一下,实际应该请求短信接口
			Console.WriteLine($"手机号：{phoneNumber}发起注册，验证码:{code}");
			return Ok(new ApiResult { Status = "ok", Message = "短信验证码发送成功" });
		}
	}
}
