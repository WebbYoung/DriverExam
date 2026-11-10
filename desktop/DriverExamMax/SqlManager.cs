using System.Data;
using Microsoft.VisualBasic.ApplicationServices;
using MySqlConnector;
using System.Text.Json;

namespace DriverExam
{
	/// <summary>
	/// SQL数据库操作类
	/// </summary>
    public class SqlManager:IDisposable
    {
		private readonly string? connectionString;
		private MySqlConnection? connection;
		private MySqlTransaction? transaction;
		private bool disposed;
		public SqlManager(string ConnectionString)
		{
			connectionString = ConnectionString??throw new ArgumentNullException(nameof(connection));
			connection = new MySqlConnection(connectionString);
		}
		/// <summary>
		/// 默认连接串。凭据不写进源码，读取顺序：
		/// 1) 环境变量 DRIVEREXAM_DB
		/// 2) 可执行文件目录下 appsettings.json 的 ConnectionStrings:DriverExamDb
		/// 3) 抛异常提示如何配置（示例见仓库内 appsettings.Example.json）
		/// </summary>
		public SqlManager()
		{
			connection = new MySqlConnection(ResolveConnectionString());
		}

		private static string ResolveConnectionString()
		{
			string? fromEnv = Environment.GetEnvironmentVariable("DRIVEREXAM_DB");
			if (!string.IsNullOrWhiteSpace(fromEnv))
			{
				return fromEnv;
			}

			string configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
			if (File.Exists(configPath))
			{
				try
				{
					using JsonDocument doc = JsonDocument.Parse(File.ReadAllText(configPath));
					if (doc.RootElement.TryGetProperty("ConnectionStrings", out JsonElement section) &&
						section.TryGetProperty("DriverExamDb", out JsonElement value) &&
						value.GetString() is { Length: > 0 } conn)
					{
						return conn;
					}
				}
				catch (JsonException)
				{
					// 配置损坏时继续走下面的报错分支，避免静默连到错误库
				}
			}

			throw new InvalidOperationException(
				"未配置数据库连接。请设置环境变量 DRIVEREXAM_DB，或在程序目录的 appsettings.json 中写入 "
				+ "ConnectionStrings:DriverExamDb（示例见仓库 appsettings.Example.json）。");
		}
		/// <summary>
		/// 打开数据库连接
		/// </summary>
		private void OpenConnection()
		{
			if (connection?.State == ConnectionState.Closed)
			{
				connection.Open();
			}
		}
		/// <summary>
		/// 开始事务
		/// </summary>
		public void BeginTransaction()
		{
			OpenConnection();
			transaction = connection?.BeginTransaction();
		}
		/// <summary>
		/// 提交事务
		/// </summary>
		public void CommitTransaction()
		{
			transaction?.Commit();
			transaction = null;
		}
		/// <summary>
		/// 回滚事务
		/// </summary>
		public void RollbackTransaction()
		{
			transaction?.Rollback();
			transaction = null;
		}
		/// <summary>
		/// 创建MySqlCommand对象，绑定事务和连接
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <param name="parameters">SQL参数</param>
		/// <returns>MySqlCommand对象</returns>
		public MySqlCommand CreateCommand(string sql, MySqlParameter[] parameters) 
		{
			var command = new MySqlCommand(sql, connection);
			if(transaction != null)
			{
				command.Transaction = transaction;
			}
			if (parameters != null)
			{
				command.Parameters.AddRange(parameters);
			}
			return command;
		}
		/// <summary>
		/// 执行非查询语句
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <param name="parameters">参数数组</param>
		/// <returns>受影响的行数</returns>
		public int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
		{
			OpenConnection();
			var command = CreateCommand(sql, parameters);
			return command.ExecuteNonQuery();
		}
		/// <summary>
		/// 异步执行非查询语句
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <param name="parameters">参数数组</param>
		/// <returns>受影响的行数</returns>
		public async Task<int> ExecuteNonQueryAsync(string sql, params MySqlParameter[] parameters)
		{
			OpenConnection();
			var command = CreateCommand(sql, parameters);
			return await command.ExecuteNonQueryAsync();
		}
		/// <summary>  
		/// 执行查询SQL语句  
		/// </summary>  
		/// <param name="sql">SQL语句</param>  
		/// <param name="parameters">参数数组</param>  
		/// <returns>查询得到的标量</returns>  
		public object? ExecuteScalar(string sql, params MySqlParameter[] parameters)
		{
			OpenConnection();
			var command = CreateCommand(sql, parameters);
			return command.ExecuteScalar();  
		}
		/// <summary>  
		/// 异步执行查询SQL语句  
		/// </summary>  
		/// <param name="sql">SQL语句</param>  
		/// <param name="parameters">参数数组</param>  
		/// <returns>查询得到的标量</returns>  
		public async Task<object?> ExecuteScalarAsync(string sql, params MySqlParameter[] parameters)
		{
			OpenConnection();
			var command = CreateCommand(sql, parameters);
			return await command.ExecuteScalarAsync();
		}
		/// <summary>
		/// 执行数据读取
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <param name="parameters">参数数组</param>
		/// <returns>包含查询结果的DataTable</returns>
		public DataTable ExecuteDataTable(string sql, params MySqlParameter[] parameters)
		{
			OpenConnection();
			var dataTable = new DataTable();
			var command = CreateCommand(sql, parameters);
			var dataAdapter = new MySqlDataAdapter(command);
			dataAdapter.Fill(dataTable);
			return dataTable;
		}
		/// <summary>
		/// 执行数据读取
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <param name="parameters">参数数组</param>
		/// <returns>包含结果的DataReader</returns>
		public MySqlDataReader ExecuteReader(string sql, params MySqlParameter[] parameters)
		{
			OpenConnection();
			var command = CreateCommand(sql, parameters);
			return command.ExecuteReader(CommandBehavior.CloseConnection);
		}
		/// <summary>
		/// 异步执行数据读取
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <param name="parameters">参数数组</param>
		/// <returns>包含结果的DataReader</returns>
		public async Task<MySqlDataReader> ExecuteReaderAsync(string sql, params MySqlParameter[] parameters)
		{
			OpenConnection();
			var command = CreateCommand(sql, parameters);
			return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
		}
		/// <summary>
		/// 手动调用Dispose方法
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		/// <summary>
		/// 释放资源
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposed) return;
			if (disposing)
			{
				transaction?.Dispose();
				connection?.Close();
				connection?.Dispose();
			}
			disposed = true;
		}
		/// <summary>
		/// 析构函数调用Dispose方法
		/// </summary>
		~SqlManager()
		{
			Dispose(false);
		}
	}
}
