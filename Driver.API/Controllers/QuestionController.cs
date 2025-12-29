using Driver.API.Attributes;
using Driver.API.Models;
using Driver.Common;
using Driver.DTO;
using Driver.IService;
using Driver.Service;
using Driver.Service.Entities;
using Driver.Service.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Threading.Tasks;

namespace Driver.API.Controllers
{
	[Route("api/[controller]")]
	public class QuestionController : ControllerBase
	{
		private readonly IQuestionService questionService;
		public QuestionController(IQuestionService questionService)
		{
			this.questionService = questionService;
		}
		[HttpGet("order/info")]
		public async Task<IActionResult> GetInitInfor()
		{
			//这里先写成0以后再改动
			var questionInfo = new QuestionInfoModel()
			{
				TotalCount = await questionService.GetQuestionCountAsync(),
				CorrectCount = 0,
				WrongCount = 0,
				CorrectRate=0,
				Progress=0
			};
			return Ok(new ApiResult { Status="ok",Data = questionInfo });
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetQuestionById(long id)
		{
			//只用这个方法当id不存在时不是预期的
			var question=await questionService.GetByIdAsync(id);
			if (question is null)
				return Ok(new ApiResult { Status = "error", Message = "没有这道题" });
			return Ok(new ApiResult { Status="ok",Data=question});
		}
		[HttpGet("order")]
		public async Task<IActionResult> GetCurrentQuestion()
		{
			var question = await questionService.GetFirstOneAsync();
			if (question is null)
				return Ok(new ApiResult { Status="ok",Message="找不到题目"});
			var questionModel = new QuestionModel();
			questionModel.Question = question;
			questionModel.CurrentIndex = 1;
			return Ok(new ApiResult { Status="ok",Data=questionModel});
		}
		[HttpGet("order/prev")]
		[Validated]
		public async Task<IActionResult> GetPreviousQuestion(QuestionProgressModel model)
		{
			if(model.CurrentIndex<=1)
				return Ok(new ApiResult { Status = "out-of-range", Message = "已经是第一题了" });
			var question = await questionService.GetPrevOneAsync(model.CurrentId);
			if (question==null)
				return Ok(new ApiResult { Status = "error", Message = "找不到题目" });
			var questionModel = new QuestionModel();
			questionModel.Question = question;
			questionModel.CurrentIndex = model.CurrentIndex - 1;
			return Ok(new ApiResult { Status = "ok", Data = questionModel });
		}
		[HttpGet("order/next")]
		[Validated]
		public async Task<IActionResult> GetNextQuestion(QuestionProgressModel model)
		{
			if (model.CurrentIndex >= model.TotalCount)
				return Ok(new ApiResult { Status = "out-of-range", Message = "已经是最后一题了" });
			var question = await questionService.GetNextOneAsync(model.CurrentId);
			if (question==null)
				return Ok(new ApiResult { Status = "error", Message = "找不到题目" });
			var questionModel = new QuestionModel();
			questionModel.Question = question;
			questionModel.CurrentIndex = model.CurrentIndex + 1;
			return Ok(new ApiResult { Status="ok",Data = questionModel});
		}
		[HttpGet("{id}/image")]
		public async Task<IActionResult> GetPicture(long id)
		{
			bool questionExsit = await questionService.GetByIdAsync(id)!=null;
			if (!questionExsit)
				return Ok(new ApiResult { Status = "error", Message = "找不到这道题" });

			//这是我原先使用的方式先转成流，感觉有点绕看看有没有更直接的
			//var picStream = await questionService.GetPicStreamAsync(id);
			//if (picStream is null)
			//	return Ok(new ApiResult { Status = "notfound", Message = "未找到该题配图" });
			//后面据我观察ControllerBase下是有这么一个方法，其实File也行但据我观察第一个参数是VirtualPath也就是虚拟路径，经我测试直接用下面的路径是不行的,所有就是另一个
			string? picPath = await questionService.GetPicPathAsync(id);
			if (string.IsNullOrEmpty(picPath))
				return Ok(new ApiResult { Status = "notfound", Message = "未找到该题配图" });
			return PhysicalFile(picPath, "image/jpeg");
		}
		[HttpPost]
		[Validated]
		public async Task<IActionResult> DetermineAnswer(AnswerModel model)
		{
			bool questionExsit = await questionService.GetByIdAsync(model.QuestionId) != null;
			if (!questionExsit)
				return Ok(new ApiResult { Status = "error", Message = "找不到这道题" });
			bool isCorrect = await questionService.CheckAnswerAsync(model.QuestionId, model.AnswerChars);
			var determineQuestionModel = new DetermineQuestionModel();
			determineQuestionModel.IsCorrect = isCorrect;
			if (isCorrect)
			{
				determineQuestionModel.CorrectCount = model.CorrectCount + 1;
				determineQuestionModel.WrongCount = model.WrongCount;
			}
			else
			{
				string correctAnswers = await questionService.GetCorrectAnswers(model.QuestionId);
				determineQuestionModel.CorrectAnswer = correctAnswers;
				determineQuestionModel.WrongCount = model.WrongCount + 1;
				determineQuestionModel.CorrectCount = model.CorrectCount;
			}
			return Ok(new ApiResult { Status = "ok", Data = determineQuestionModel });
		}
	}
}
