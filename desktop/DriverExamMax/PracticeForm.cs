using DriverExam.ViewModels;
using DriverExam.QuestionDal;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace DriverExam
{
	public partial class PracticeForm : Form
	{
		private bool isImageClicked = false;
		private readonly ExamViewModel viewModel;
		public Size originSize;
		public PracticeForm(string questionSource, QuestionService.Mode mode)
		{
			InitializeComponent();
			originSize = pictureBox1.Size;
			viewModel = new ExamViewModel(questionSource, mode);
			InitializeUI();
			LoadOptions();
		}
		private void InitializeUI()
		{
			rightLabel.Text = 0.ToString();
			wrongLabel.Text = 0.ToString();
			Title.DataBindings.Add("Text", viewModel, "CurrentQuestion.Title", true, DataSourceUpdateMode.OnPropertyChanged);
			pictureBox1.DataBindings.Add("Image", viewModel, "CurrentQuestion.Image", true, DataSourceUpdateMode.OnPropertyChanged);
			Analysis.DataBindings.Add("Text", viewModel, "CurrentQuestion.Analysis", true, DataSourceUpdateMode.OnPropertyChanged);
			answerLabel.DataBindings.Add("Text", viewModel, "CurrentQuestion.Answer", true, DataSourceUpdateMode.OnPropertyChanged);
			current.DataBindings.Add("Text", viewModel, "CurrentIndex", true, DataSourceUpdateMode.OnPropertyChanged);
			CountQuestion.Text = viewModel._questionService.GetLength().ToString();
			button3.Enabled = viewModel.CanGoPrevious;
			button4.Enabled = viewModel.CanGoNext;
			viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(viewModel.CanGoPrevious) || e.PropertyName == nameof(viewModel.CanGoNext))
				{
					button3.Enabled = viewModel.CanGoPrevious;
					button4.Enabled = viewModel.CanGoNext;
				}
			};
		}
		private void LoadOptions()
		{
			optionPanel.Controls.Clear();
			int optionIndex = 0;
			if (viewModel.CurrentQuestion.Options != null)
			{
				foreach (var option in viewModel.CurrentQuestion.Options)
				{
					var rb = new RadioButton
					{
						Text = option.Content,
						Tag = optionIndex++,
						Dock = DockStyle.Bottom,
						Margin = new Padding(5)
					};
					rb.DataBindings.Add("Checked", option, "IsSelected", true, DataSourceUpdateMode.OnPropertyChanged);
					rb.CheckedChanged += (s, e) =>
					{
						if ((s as RadioButton)?.Checked == true)
						{
							viewModel.CurrentQuestion.SelectedOption = option;
						}
					};
					optionPanel.Controls.Add(rb);
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			panel1.Show();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			panel1.Hide();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			if (isImageClicked)
			{
				pictureBox1.Size = originSize;
				isImageClicked = false;
			}
			else
			{
				pictureBox1.Size = new Size(pictureBox1.Width * 2, pictureBox1.Height * 2);
				isImageClicked = true;
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (viewModel.CurrentQuestion.SelectedOption.IsCorrect)
			{
				rightLabel.Text = (int.Parse(rightLabel.Text) + 1).ToString();
			}
			else
			{
				wrongLabel.Text = (int.Parse(wrongLabel.Text) + 1).ToString();
			}
			viewModel.GoToNextQuestion();
			LoadOptions();
			
		}

		private void button3_Click(object sender, EventArgs e)
		{
			viewModel.GoToPreviousQuestion();
			LoadOptions();
		}
	}
}
