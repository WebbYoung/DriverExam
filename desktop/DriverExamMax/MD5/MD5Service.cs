using System.Text;

namespace DriverExam.MD5
{
	public static class MD5Service
	{
		/// <summary>  
		/// 使用 MD5 哈希加密字符串  
		/// </summary>  
		/// <param name="input">要加密的字符串</param>  
		/// <returns>加密后的字符串</returns>  
		public static string MD5(string input)
		{
			using (var md5 = System.Security.Cryptography.MD5.Create())
			{
				byte[] inputBytes = Encoding.UTF8.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(inputBytes);

				StringBuilder sb = new StringBuilder();
				foreach (byte b in hashBytes)
				{
					sb.Append(b.ToString("x2"));
				}
				return sb.ToString();
			}
		}
	}
}
