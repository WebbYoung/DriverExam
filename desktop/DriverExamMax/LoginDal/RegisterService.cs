using DriverExam.MD5;
using MySqlConnector;

namespace DriverExam.LoginDal
{
    public class RegisterService
    {
		/// <summary>
		/// 注册用户方法
		/// </summary>
		/// <param name="username">用户名</param>
		/// <param name="password">密码</param>
		/// <returns>如果用户已存在返回0，否则返回插入影响的行数</returns>
		public static int RegisterUser(string username, string password)
        {
            using(var db=new SqlManager())
            {
				var NewPassword = MD5Service.MD5(password);
				string sql = "SELECT COUNT(*) FROM UserInfo WHERE UserName = @UserName AND UserPwd = @UserPwd";
				string insertQuery = "INSERT INTO UserInfo (UserName, UserPwd,CorrectNum,CurrentNum) VALUES (@UserName, @UserPwd,0,0)";
				MySqlParameter[] ps =
				{
                    new MySqlParameter("@UserName", username),
				    new MySqlParameter("@UserPwd", NewPassword)
			    };
				var Result = Convert.ToInt32(db.ExecuteScalar(sql, ps));
				if (Result == 0)
				{
					return db.ExecuteNonQuery(insertQuery, ps);
				}
			}
			return 0;
        }
    }
}
