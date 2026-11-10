using System.Collections.ObjectModel;

namespace DriverExam.QuestionDal
{
	/// <summary>
	/// 接口：题目仓储
	/// </summary>
	public interface IQuestionRepository<T,U>
    {
		T GetRandom(int count);
        T GetWrong();
        U GetById(int id);
		T GetAll();
	}
}
