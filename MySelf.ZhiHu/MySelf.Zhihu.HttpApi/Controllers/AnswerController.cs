using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.HttpApi.Infrastructure;
using MySelf.Zhihu.HttpApi.Models;
using MySelf.Zhihu.SharedKernel.Paging;
using MySelf.Zhihu.UseCases.Answers;
using MySelf.Zhihu.UseCases.Answers.Commands;
using MySelf.Zhihu.UseCases.Answers.Queries;

namespace MySelf.Zhihu.HttpApi.Controllers
{
    [Route("api/question/{questId:int}/[controller]")]
    public class AnswerController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(int questionId, CreateAnswerRequest request)
        {
            var result = await Sender.Send(new CreateAnswerCommand(questionId, request.Content));
            return  ReturnResult(result);
        }
        [HttpDelete("{answerId:int}")]
        public async Task<IActionResult> Delete(int questionId, int answerId)
        {
            var result= await Sender.Send(new  DeleteAnswerCommand(questionId, answerId));
            return ReturnResult(result);
        }

        [HttpPut("{answerId:int}")]
        public async Task<IActionResult> Update(int questionId, int answerId, UpdateAnswerRequest request)
        {
            var result = await Sender.Send(new UpdateAnswerCommand(questionId, answerId, request.Content));
            return ReturnResult(result);
        }
        [HttpGet("{answerId:int")]
        public async Task<IActionResult> Get(int questionId, int answerId)
        {
            var result = await Sender.Send(new GetAnswerWithQuestionQuery(questionId, answerId));
            return ReturnResult(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList(int questionId, [FromQuery] Pagination pagination)
        {
            var result =
                await Sender.Send(new GetAnswersQuery(questionId, pagination));

            return ReturnResult(result);
        }

        [HttpPost("{answerId:int}/like")]
        public async Task<IActionResult> CreateLike(int questionId, int answerId, bool isLike)
        {
            var result = await Sender.Send(new CreateAnswerLikeCommand(questionId, answerId, isLike));

            return ReturnResult(result);
        }

        [HttpPut("{answerId:int}/like")]
        public async Task<IActionResult> UpdateLike(int questionId, int answerId, bool isLike)
        {
            var result = await Sender.Send(new UpdateAnswerLikeCommand(questionId, answerId, isLike));

            return ReturnResult(result);
        }

        [HttpDelete("{answerId:int}/like")]
        public async Task<IActionResult> DeleteLike(int questionId, int answerId)
        {
            var result = await Sender.Send(new DeleteAnswerLikeCommand(questionId, answerId));

            return ReturnResult(result);
        }
    }
}
