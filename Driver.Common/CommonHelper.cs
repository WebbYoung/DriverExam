using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Common
{
	public static class CommonHelper
	{
		public static string CalcMD5(this string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			return CalcMD5(bytes);
		}
		public static string CalcMD5(byte[] bytes)
		{
			using var md5 = MD5.Create();
			byte[] computeBytes = md5.ComputeHash(bytes);
			string result = string.Empty;
			for (int i = 0; i < computeBytes.Length; ++i)
			{
				result += computeBytes[i].ToString("X").Length == 1 ? "0" + computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
			}
			return result;
		}
		public static string CalcMD5(this Stream stream)
		{
			using var md5 = MD5.Create();
			byte[] computeBytes = md5.ComputeHash(stream);
			string result = string.Empty;
			for (int i = 0; i < computeBytes.Length; ++i)
			{
				result += computeBytes[i].ToString("X").Length == 1 ? "0" + computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
			}
			return result;
		}
		public static string CreateVerifyCode(int len)
		{
			char[] data = { 'a', 'c', 'd', 'e', 'f', 'g', 'k', 'm', 'p', 'r', 's', 't', 'w', 'x', 'y', '3', '4', '5', '7', '8' };
			StringBuilder sbCode = new StringBuilder();
			Random rand = new Random();
			for (int i = 0; i < len; ++i)
			{
				char ch = data[rand.Next(data.Length)];
				sbCode.Append(ch);
			}
			//勤测
			return sbCode.ToString();
		}
		public static string CreateSMSCode(int len)
		{
			return Random.Shared.Next((int)Math.Pow(10, len - 1), (int)Math.Pow(10, len)).ToString();
		}
	}
}
