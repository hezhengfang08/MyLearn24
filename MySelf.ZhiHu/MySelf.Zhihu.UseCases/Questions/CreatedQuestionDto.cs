using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Questions
{
    public record CreatedQuestionDto
    {
        public int Id { get; set; } 
        public  CreatedQuestionDto(int id)
        {
            Id = id;
        }
    }
}
