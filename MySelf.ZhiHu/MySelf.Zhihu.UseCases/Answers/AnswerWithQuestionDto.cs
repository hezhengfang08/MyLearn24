using MySelf.Zhihu.UseCases.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Answers
{
    public record AnswerWithQuestionDto
    {

        public AnswerDto Answer { get; init; } = null!;

        public QuestionDto Question { get; init; } = null!;
    }
}
