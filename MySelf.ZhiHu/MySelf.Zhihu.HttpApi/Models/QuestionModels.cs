namespace MySelf.Zhihu.HttpApi.Models
{
    public record CreateQuestionRequest(string Title, string Description);

    public record UpdateQuestionRequest(string Title, string Description);
}
