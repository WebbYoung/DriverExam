namespace DriverExam
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginForm loginForm = new LoginForm();
            Application.Run(loginForm);
			if (loginForm.DialogResult == DialogResult.OK)
			{
				Application.Run(new MainForm());
			}
		}
    }
}
