using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.Zhihu.HttpApi.Infrastructure;
using MySelf.Zhihu.HttpApi.Models;
using MySelf.Zhihu.UseCases.Questions.Commands;
using MySelf.Zhihu.UseCases.Questions.Queries;

namespace MySelf.Zhihu.HttpApi.Controllers
{
    [Route("api/question")]
    public class QuestionController : ApiControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionRequest request)
        {
            var result = await Sender.Send(new CreateQuestionCommand(request.Title, request.Description));

            return ReturnResult(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Sender.Send(new DeleteQuestionCommand(id));

            return ReturnResult(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateQuestionRequest request)
        {
            var result = await Sender.Send(new UpdateQuestionCommand(id, request.Title, request.Description));

            return ReturnResult(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await Sender.Send(new GetQuestionQuery(id));

            return ReturnResult(result);
        }
    }
}
