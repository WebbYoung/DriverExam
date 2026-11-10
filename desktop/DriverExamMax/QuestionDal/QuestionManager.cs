using DriverExam.QuestionDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverExamMax.QuestionDal
{
    public class QuestionManager : IQuestionRepository<string[],string>
    {
		private string QuestionsDirectoryPath;
		private string[] QuestionPaths;
		private int questionSize;
		public int QuestionSize { get; set; }
		public QuestionManager(string questionsDirectoryPath)
		{
			QuestionsDirectoryPath = questionsDirectoryPath;
			QuestionPaths=Directory.GetDirectories(QuestionsDirectoryPath);
			questionSize=QuestionPaths.Length;
		}
		public string[] GetRandom()
		{
			Random rand = new Random();
			return QuestionPaths.OrderBy(x => rand.Next()).ToArray();
		}
		public string[] GetRandom(int count)
		{
			if (count > QuestionPaths.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(count), "请求的题目数量超过可用题目数量");
			}
			Random rand = new Random();
			return QuestionPaths.OrderBy(x => rand.Next()).Take(count).ToArray();
		}
		public string[] GetAll()
		{
			return QuestionPaths;
		}
		public string GetById(int id)
		{
			return QuestionPaths.Where(QuestionPath => QuestionPath.EndsWith(Convert.ToString(id))).FirstOrDefault();
		}
		public string[] GetWrong()
		{

			return null;
		}
	}
}
