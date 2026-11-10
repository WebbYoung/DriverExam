using DriverExam.LoginDal;
using DriverExam.MD5;
using DriverExam.UserDal;
using System.Reflection;
using System.Windows.Forms.PropertyGridInternal;

namespace DriverExam
{
	public partial class LoginForm : Form
	{
		public LoginForm()
		{
			InitializeComponent();
		}
		/// <summary>
		/// 点击退出按钮关闭程序
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		///<summary>
		/// 点击注册按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void register_Click(object sender, EventArgs e)
		{
			if(string.IsNullOrEmpty(UserName.Text.Trim()) || string.IsNullOrEmpty(UserPwd.Text.Trim()))
			{
				MessageBox.Show("请输入用户名和密码", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else if (IAgree.Checked == false)
			{
				MessageBox.Show("请先同意用户协议", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			//else if (!MD5Service.CheckPassword(UserPwd.Text.Trim()))
			//{
			//	MessageBox.Show(@"您的密码必须在8位以上且包含大小写，数字和特殊字符(/*&@)", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//}
			else
			{
				var Result = RegisterService.RegisterUser(UserName.Text, UserPwd.Text);
				if (Result != 0)
				{
					MessageBox.Show("注册成功，请登录", "温馨提示", MessageBoxButtons.OK);
					LoginModel.UserName = UserName.Text;
				}
				else
				{
					MessageBox.Show("您已注册，请登录", "温馨提示", MessageBoxButtons.OK);
				}
				UserName.Clear();
				UserPwd.Clear();
			}
		}
		/// <summary>
		/// 登录按钮点击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void login_Click(object sender, EventArgs e)
		{
			if(string.IsNullOrEmpty(UserName.Text.Trim()) || string.IsNullOrEmpty(UserPwd.Text.Trim()))
			{
				MessageBox.Show("请输入用户名和密码", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else if (IAgree.Checked == false)
			{
				MessageBox.Show("请先同意用户协议", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				LoginService loginService = new LoginService();
				var LoginResult = loginService.Login(UserName.Text, UserPwd.Text);
				if (LoginResult == 1)
				{
					this.DialogResult = DialogResult.OK;
					LoginModel.UserName = UserName.Text;
					LoginModel.UserPwd = UserPwd.Text;
					_=DataService.LoadUserData(); 
					if (RememberMe.Checked)
					{
						AccountService.SaveUserAccount();
					}
					else
					{
						AccountService.DeleteAccount();
					}
					this.Close();
				}
				else if (LoginResult <= 0)
				{
					// 这里是检查账号是否存在的
					MessageBox.Show("该用户不存在", "信息提示", MessageBoxButtons.OK);
					UserName.Clear();
					UserPwd.Clear();
				}
			}
		}
		/// <summary>
		/// 登录窗体加载事件，读取记住的用户名和密码并回填
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoginForm_Load(object sender, EventArgs e)
		{
			var userAccount=AccountService.ReadUserAccount();
			if(userAccount != (string.Empty, string.Empty))
			{
				UserName.Text = userAccount.Item1;
				UserPwd.Text = userAccount.Item2;
				RememberMe.Checked = true;
				IAgree.Checked = true;
			}
		}
		/// <summary>
		/// 点击隐私政策，显示提示文本
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Reminderterms form = new Reminderterms(Path.Combine(AppContext.BaseDirectory, "Resources", "UsageAgreement.txt"));
			form.ShowDialog();
		}
		/// <summary>
		/// 点击用户使用协议，显示提示文本
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Reminderterms form = new Reminderterms(Path.Combine(AppContext.BaseDirectory, "Resources", "PrivacyPolicy.txt"));
			form.ShowDialog();
		}
	}
}
