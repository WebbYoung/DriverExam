using DriverExam.LoginDal;
using DriverExam.QuestionDal;
using System.Diagnostics;

namespace DriverExam
{
	public partial class MainForm : Form
	{
		// 题库目录随程序输出，避免依赖开发机的绝对路径
		private static readonly string ResourceRoot = Path.Combine(AppContext.BaseDirectory, "Resources");
		public string Subject1Path = Path.Combine(ResourceRoot, "Subject1");
		public string Subject4Path = Path.Combine(ResourceRoot, "Subject4");
		public MainForm()
		{
			InitializeComponent();
		}
		private void MainForm_Load(object sender, EventArgs e)
		{
			label11.Text = LoginModel.UserName;
		}
		private void practiceButton_Click(object sender, EventArgs e)
		{
			PracticeForm practiceForm = new PracticeForm(Subject1Path, QuestionService.Mode.Normal);
			practiceForm.ShowDialog();
		}

		private void button28_Click(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{
			PracticeForm examform = new PracticeForm(Subject1Path, QuestionService.Mode.Exam);
			examform.ShowDialog();
		}

		private void button24_Click(object sender, EventArgs e)
		{
			PracticeForm practiceForm = new(Subject4Path, QuestionService.Mode.Normal);
			practiceForm.ShowDialog();
		}

		private void button23_Click(object sender, EventArgs e)
		{
			PracticeForm examform = new(Subject4Path, QuestionService.Mode.Default);
			examform.ShowDialog();
		}

		private void button12_Click(object sender, EventArgs e)
		{
			PracticeForm practiceForm = new(Subject1Path, QuestionService.Mode.Default);
			practiceForm.ShowDialog();
		}
	}
}
