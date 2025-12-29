using Driver.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.IService
{
	public interface IQuestionService
	{
		Task<QuestionDTO?> GetByIdAsync(long id);
		Task<bool> CheckAnswerAsync(long questionId, char[]? answers);
		Task<long> GetQuestionCountAsync();
		Task<Stream?> GetPicStreamAsync(long questionId);
		Task<string> GetCorrectAnswers(long questionId);
		Task<QuestionDTO?> GetRandomAsync(long[]? answeredQuestionIds=null);
		Task<QuestionDTO?> GetNextOneAsync(long currId);
		Task<QuestionDTO?> GetPrevOneAsync(long currId);
		Task<QuestionDTO?> GetFirstOneAsync();
		Task<string?> GetPicPathAsync(long questionId);
	}
}
