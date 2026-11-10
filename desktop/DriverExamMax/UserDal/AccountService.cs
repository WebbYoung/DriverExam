namespace DriverExam.LoginDal
{
    public class AccountService
    {
		/// <summary>
		/// 暂存用户名密码
		/// </summary>
		public static void SaveUserAccount()
		{
			var UserNameTxtPath = Environment.CurrentDirectory;
			// 这里是登录，不做账号是否记住判断，只管记住就行了
			string filePath = Path.Combine(UserNameTxtPath, $"Remember.txt");
			File.WriteAllText(filePath, LoginModel.UserName+"\r\n"+LoginModel.UserPwd);
		}
		/// <summary>
		/// 读取记住的用户名密码
		/// </summary>
		/// <returns>如果有返回用户名密码，没有返回空字符串</returns>
		public static (string,string) ReadUserAccount()
		{
			var UserNameTxtPath = Environment.CurrentDirectory;
			string filePath = Path.Combine(UserNameTxtPath, "Remember.txt");
			if (File.Exists(filePath))
			{
				string[] lines = File.ReadAllLines(filePath);
				return (lines[0], lines[1]);
			}
			return (string.Empty,string.Empty);
		}
		/// <summary>
		/// 删除记住的用户名密码
		/// </summary>
		public static void DeleteAccount()
		{
			var UserNameTxtPath = Environment.CurrentDirectory;
			string filePath = Path.Combine(UserNameTxtPath, "Remember.txt");
			File.Delete(filePath);
		}
	}
}
