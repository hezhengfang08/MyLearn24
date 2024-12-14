using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.UseCases.Questions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Questions
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<CreateQuestionCommand, Question>();
            CreateMap<UpdateQuestionCommand, Question>();
        }
      
    }
}
