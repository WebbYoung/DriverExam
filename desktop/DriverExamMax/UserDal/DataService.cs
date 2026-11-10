using MySqlConnector;
using DriverExam.LoginDal;

namespace DriverExam.UserDal
{
    class DataService
    {
		/// <summary>
		/// 加载用户答题信息
		/// </summary>
		public static async Task LoadUserData()
		{
			using (var db = new SqlManager())
			{
				string sql = "SELECT CorrectNum,CurrentNum FROM UserInfo WHERE UserName = @UserName";
				var ps = new MySqlParameter("@UserName", LoginModel.UserName);
				var reader = await db.ExecuteReaderAsync(sql, ps);
				if (await reader.ReadAsync())
				{
					LoginModel.CorrectNum = Convert.ToInt32(reader["CorrectNum"]);
					LoginModel.CurrentNum = Convert.ToInt32(reader["CurrentNum"]);
				}
			}
		}
		/// <summary>
		/// 更新用户答题信息
		/// </summary>
		public static async Task UpdateUserData()
		{
			SqlManager? db=null;
			try
			{
				db = new SqlManager();
				db.BeginTransaction();
				string sql = "UPDATE UserInfo SET CorrectNum = @CorrectNum, CurrentNum = @CurrentNum WHERE UserName = @UserName";
				MySqlParameter[] ps =
				{
					new MySqlParameter("@UserName", LoginModel.UserName),
				    new MySqlParameter("@CorrectNum", LoginModel.CorrectNum),
				    new MySqlParameter("@CurrentNum", LoginModel.CurrentNum)
			    };
				await db.ExecuteNonQueryAsync(sql, ps);
				db.CommitTransaction();
			}
			catch
			{
				db?.RollbackTransaction();
				throw;
			}
			finally
			{
				db?.Dispose();
			}
		}
	}
}
