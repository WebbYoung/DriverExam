using Driver.Common;
using Driver.DTO;
using Driver.IService;
using Driver.Service.Entities;
using Driver.Service.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Service.Services
{
	public class QuestionService:IQuestionService
	{
		public static QuestionDTO ToDTO(Question quest)
		{
			QuestionDTO dto = new QuestionDTO()
			{
				Id = quest.Id,
				CreateDate = quest.CreateDate,
				Title = quest.Title,
				QuestionType=quest.QuestionType.ToString(),
				Options= [.. quest.Options.Select(e=>new OptionDTO(e.OptionChar,e.Content,e.IsAnswer))]
			};
			return dto;
		}
		/// <summary>
		/// 该方法被弃用
		/// </summary>
		/// <param name="questionId"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Stream?> GetPicStreamAsync(long questionId)
		{
			using (var context = new DriverDbContext())
			{
				var question = await context.Questions.FindAsync(questionId);
				if (question == null)
					throw new ArgumentNullException(nameof(question), "找不到题目");
				if (string.IsNullOrEmpty(question.PicPath))
					return null;
				if (!Path.Exists(question.PicPath))
					throw new ArgumentException("找不到图片");
				byte[] pic = await System.IO.File.ReadAllBytesAsync(question.PicPath);
				if (pic == null) throw new ArgumentException("读取图片失败");
				var ms = new MemoryStream(pic);
				return ms;
			}
		}
		public async Task<string?> GetPicPathAsync(long questionId)
		{
			using (var context = new DriverDbContext())
			{
				var question = await context.Questions.FindAsync(questionId);
				if (question == null)
					throw new ArgumentNullException(nameof(question), "找不到题目");
				if (string.IsNullOrEmpty(question.PicPath))
					return null;
				if (!Path.Exists(question.PicPath))
					throw new ArgumentException("找不到图片");
				return question.PicPath;
			}
		}
		public async Task<bool> CheckAnswerAsync(long questionId, char[]? answers)
		{
			if (answers == null)
				throw new ArgumentNullException(nameof(answers), "答案为空");
			using(var context=new DriverDbContext())
			{
				var question = await context.Questions.AsNoTracking()
					.Where(e => e.Id == questionId)
					.Include(e=>e.Options)
					.SingleOrDefaultAsync();
				if (question == null)
					throw new ArgumentNullException(nameof(question), "找不到题目");
				var correctAnswer = question.Options.Where(e => e.IsAnswer)
					.Select(e=>e.OptionChar).ToList();
				if (answers.Length != correctAnswer.Count)
				{
					return false;
				}
				foreach (char answer in correctAnswer)
				{
					if (!answers.Contains(answer))
						return false;
				}
				return true;
			}
		}
		public async Task<string> GetCorrectAnswers(long questionId)
		{
			using(var context=new DriverDbContext())
			{
				var question = await context.Questions.AsNoTracking()
					.Where(e => e.Id == questionId)
					.Include(e => e.Options)
					.SingleOrDefaultAsync();
				if (question == null)
					throw new ArgumentNullException(nameof(question), "找不到题目");
				var correctAnswer = question.Options.Where(e => e.IsAnswer)
					.Select(e => e.OptionChar).ToArray();
				return string.Join(',', correctAnswer);
			}
		}
		public async Task<QuestionDTO?> GetRandomAsync(long[]? answeredQuestionIds=null)
		{
			using(var context=new DriverDbContext())
			{
				Question? question;
				int questionCount = await context.Questions.CountAsync();
				if (answeredQuestionIds == null)
				{
					question = await context.Questions
						.OrderBy(e=>Guid.NewGuid())
						.FirstOrDefaultAsync();
				}
				else
				{
					var unansweredQuestions = context.Questions
						.Where(e => !answeredQuestionIds.Contains(e.Id));
					//豆包给出了个更简单的方法
					question = await unansweredQuestions.OrderBy(e => Guid.NewGuid()).FirstOrDefaultAsync();
					////下面我自己的思路，但是我自己都感觉实在是太扯淡了
					////拿到第一个和最后一个的Id
					//long beginId = await unansweredQuestions.Select(e => e.Id).FirstOrDefaultAsync();
					//long endId = await unansweredQuestions.Select(e => e.Id).OrderDescending().FirstOrDefaultAsync();
					////随机从中抽一个数
					//long selectedId = Random.Shared.NextInt64(beginId, endId + 1);
					////找到id对应的题，如果没有往后找，后面没有就取他前面的那个
					//question = await context.Questions
					//	.Where(e => e.Id >= selectedId).FirstOrDefaultAsync()
					//	?? await context.Questions.Where(e => e.Id < selectedId).OrderByDescending(e => e.Id).FirstOrDefaultAsync();
				}
				return question == null ? null : ToDTO(question);
			}	
		}
		public async Task<QuestionDTO?> GetFirstOneAsync()
		{
			using (var context = new DriverDbContext())
			{
				Question? question = await context.Questions
					.OrderBy(e=>e.Id)
					.FirstOrDefaultAsync();
				return question == null ? null : ToDTO(question);
			}
		}
		public async Task<QuestionDTO?> GetPrevOneAsync(long currId)
		{
			using(var context=new DriverDbContext())
			{
				var question = await context.Questions
					.Where(e => e.Id < currId)
					.OrderByDescending(e => e.Id)
					.FirstOrDefaultAsync();
				return question == null ? null : ToDTO(question);
			}
		}
		public async Task<QuestionDTO?> GetNextOneAsync(long currId)
		{
			using(var context= new DriverDbContext())
			{
				Question? question = await context.Questions
						.Where(e => e.Id > currId)
						.OrderBy(e=>e.Id)
						.FirstOrDefaultAsync();
				return question == null ? null : ToDTO(question);
			}
		}
		/// <summary>
		/// 获取指定Id的题目 已弃用
		/// </summary>
		/// <param name="id">题目Id</param>
		/// <returns>QuestionDTO,找不到为null</returns>
		public async Task<QuestionDTO?> GetByIdAsync(long id)
		{
			//这里我发现了一个问题，如果数据库删了几行，主键不从1开始或者主键不连续，那么就会有问题
			using (var context = new DriverDbContext())
			{
				var question =await context.Questions.AsNoTracking()
					.Include(e=>e.Options)
					.SingleOrDefaultAsync(e => e.Id == id);
				return question==null?null:ToDTO(question);
			}
		}
		public Task<long> GetQuestionCountAsync()
		{
			using(var context=new DriverDbContext())
			{
				return context.Questions.LongCountAsync();
			}
		}
	}
}
