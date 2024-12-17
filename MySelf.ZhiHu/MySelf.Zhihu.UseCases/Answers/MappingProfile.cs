using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.UseCases.Answers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Answers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAnswerCommand, Answer>();
            CreateMap<UpdateAnswerCommand, Answer>();
        }
    }
}
