using MySqlConnector;
using DriverExam.MD5;

namespace DriverExam.LoginDal
{
    public class LoginService
    {
		/// <summary>
		/// 登录验证
		/// </summary>
		/// <param name="username">登录用户名</param>
		/// <param name="password">登录密码</param>
		/// <returns>数据库查询返回匹配账户数量</returns>
		public int Login(string username, string password)
        {
			SqlManager db = new SqlManager();
			string sql = "SELECT count(*) FROM UserInfo WHERE UserName = @UserName and UserPwd = @UserPwd";
			var HashPassword = MD5Service.MD5(password);
			MySqlParameter[] ps =
			{
				new MySqlParameter("@UserName", username),
				new MySqlParameter("@UserPwd", HashPassword)
			};
			return Convert.ToInt32(db.ExecuteScalar(sql, ps));
        }
    }
}
