using DriverExam.ViewModels;
using DriverExamMax.QuestionDal;
using ImageMagick;
using System.Collections.ObjectModel;

namespace DriverExam.QuestionDal
{
    public class QuestionService
	{
		private QuestionManager _questionManager;
		private string[] _questionPaths;
		public QuestionService(QuestionManager questionManager,Mode mode)
		{
			_questionManager = questionManager;
			_questionPaths = GetQuestionPaths(mode);
		}
		public enum Mode 
		{
			Exam,Normal,Default,WrongBank
		}
		public int GetLength() => _questionPaths.Length;
		public string[] GetQuestionPaths(Mode mode)
		{
			_questionPaths = mode switch
			{
				Mode.Exam => _questionManager.GetRandom(100),
				Mode.Normal => _questionManager.GetAll(),
				Mode.Default => _questionManager.GetRandom(),
				Mode.WrongBank => _questionManager.GetWrong(),
				_ => throw new ArgumentOutOfRangeException(nameof(mode), "未知模式")
			};
			return _questionPaths;
		}
		public Question GetQuestion(int index)
		{
			if (index < 0 || index >= _questionPaths.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(index), "题目索引超出范围");
			}
			string questionPath = _questionPaths[index];
			if (string.IsNullOrEmpty(questionPath))
			{
				throw new ArgumentException("题目数据未找到", nameof(questionPath));
			}
			return GetQuestionByPath(questionPath);
		}
		public Question GetById(int id)
		{
			string questionPath = _questionManager.GetById(id);
			if (string.IsNullOrEmpty(questionPath))
			{
				throw new ArgumentException("题目数据未找到", nameof(questionPath));
			}
			return GetQuestionByPath(questionPath);
		}
		public Question GetQuestionByPath(string _questionFilePath)
		{
			if (string.IsNullOrEmpty(_questionFilePath))
			{
				throw new ArgumentException("题目数据未找到", nameof(_questionFilePath));
			}
			string? _questionpath = Directory.GetFiles(_questionFilePath).FirstOrDefault(x => x.EndsWith(".txt"));
			string? _imagepath = Directory.GetFiles(_questionFilePath).FirstOrDefault(x => x.EndsWith(".webp"));
			if (string.IsNullOrEmpty(_questionpath))
			{
				throw new FileNotFoundException("题目文本不存在", _questionFilePath);
			}
			string[] context = File.ReadAllText(_questionpath).Split('[', ']');
			if (context == null || context.Length < 2)
			{
				throw new FileNotFoundException("题目文本内容无效", _questionFilePath);
			}
			List<Option> options = new List<Option>();
			for (int i = 2; i < context.Length - 2; i++)
			{
				if (string.IsNullOrEmpty(context[i]) || string.IsNullOrWhiteSpace(context[i])) continue;
				if (context[i].Contains("答案"))
				{
					Option[] opt =
					{
						new Option(content: "对", isCorrect: context[i].Contains("对")),
						new Option(content: "错", isCorrect: context[i].Contains("错"))
					};
					options.AddRange(opt);
					break;
				}
				options.Add(new Option(
					content: context[i].TrimStart('F', '-'),
					isCorrect: context[i].Contains("F-")
					)
				);
			}
			var correctOption = options.FirstOrDefault(x => x.IsCorrect);
			var question = new Question()
			{
				QuestionId = _questionFilePath.GetHashCode(),
				Title = context[1],
				Analysis = context[context.Length - 2],
				Options = options,
				Image = GetImage(_imagepath),
				Answer = correctOption.Content.Contains("、")
					? correctOption.Content.Split('、')[0]
					: correctOption.Content
			};
			return question;
		}
		public Image? GetImage(string? ImagePath)
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
					MessageBox.Show(ex.Message, "未知错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return null;
				}
			}
			else
			{
				return null;
			}
		}
	}
}
