using DriverExam.LoginDal;
using ImageMagick;

namespace DriverExam
{
	class QuizSystem
	{
		private string[]? QuestionsPath;
		private string[]? Question;
		private string[]? QuestionPath;
		public int allQuestions;
		public QuizSystem(string Resource)
		{
			QuestionsPath = Directory.GetDirectories(Resource);
			allQuestions = QuestionsPath.Length;
		}
		public void LoadQuestion(int index)
		{
			// 加载题目
			if (QuestionsPath == null || index < 0 || index >= QuestionsPath.Length)
			{
				throw new ArgumentOutOfRangeException("index", "题目不存在");
			}
			QuestionPath = Directory.GetFiles(QuestionsPath[index]);
			if (QuestionPath == null)
			{
				throw new InvalidOperationException("题目路径未找到");
			}
			foreach (string question in QuestionPath)
			{
				if (question.Contains(".txt"))
				{
					Question = File.ReadAllText(question).Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
					break;
				}
			}
			if (Question == null)
			{
				throw new InvalidOperationException("题目文件未找到或内容为空");
			}
			for (int i = 0; i < Question.Length; i++)
			{
				Question[i] = Question[i].Trim();
			}
		}
		public string GetTitle()
		{
			if (Question != null)
			{
				return Question[0];
			}
			else
			{
				throw new InvalidOperationException("题目未加载");
			}
		}
		public List<string> GetOptions()
		{
			List<string> Options = new List<string>();
			if (Question != null)
			{
				for (int i = 1; i < Question.Length - 2; i++)
				{
					if (string.IsNullOrWhiteSpace(Question[i])) continue;
					if (Question[i].Contains("答案："))
					{
						if (Question[i].Contains("对"))
						{
							Options.Add("F-对");
							Options.Add("错");
						}
						else
						{
							Options.Add("对");
							Options.Add("F-错");
						}
						continue;
					}
					Options.Add(Question[i]);
				}
				return Options;
			}
			else
			{
				throw new InvalidOperationException("题目未加载");
			}
		}
		public string GetAnalysis()
		{
			if (Question != null)
			{
				return Question[Question.Length - 2];
			}
			else
			{
				throw new InvalidOperationException("题目未加载");
			}
		}
		public Image? GetImage()
		{
			string ImagePath = string.Empty;
			if (QuestionPath == null)
			{
				throw new InvalidOperationException("题目路径未加载");
			}
			foreach (string question in QuestionPath)
			{
				if (question.Contains(".png"))
				{
					ImagePath = question;
					break;
				}
			}
			if (Question != null)
			{
				if (File.Exists(ImagePath))
				{
					try
					{
						using (MagickImage imagemagick = new(ImagePath))
						{
							using (MemoryStream ms = new())
							{
								imagemagick.Write(ms, MagickFormat.Png);
								ms.Position = 0;
								return Image.FromStream(ms);
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return null;
					}
				}
				else
				{
					return null;
				}
			}
			else
			{
				throw new InvalidOperationException("题目未加载");
			}
		}
	}
}
