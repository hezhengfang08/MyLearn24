namespace Myself.PMS.Server.Models
{
    public class Result<T>
    {
        public int State { get; set; } = 200;
        public string ExceptionMessage { get; set; }
        public T Data { get; set; }
    }
}
