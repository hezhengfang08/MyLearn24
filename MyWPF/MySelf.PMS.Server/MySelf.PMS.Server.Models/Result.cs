namespace MySelf.PMS.Server.Models
{
    public class Result<T>
    {
        public StateEnum State { get; set; } = StateEnum.Success;
        public string Message { get; set; }
        public T Data { get; set; }
    }
    public enum StateEnum
    {
        Success=200, //成功
        Faile = 400, //失败
        Error = 500, //错误
    }
}
