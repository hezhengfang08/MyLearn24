using System.Numerics;

namespace Myself.MudBus.Communication.Labary
{
    /// <summary>
    /// 数据bool ushort  float
    /// </summary>
    public class Result<T>
    {
        public bool Status { get; set; }=true;
        public string Message { get; set; } = string.Empty; 
        public List<T> Datas { get; set; } = new List<T>();
        public Result() : this(true, "OK") { }
        public Result(bool state, string msg) : this(state, msg, new List<T>()) { }
        public Result(bool state, string msg, List<T> datas)
        {
            Status = state;
            Message = msg;
            Datas = datas;
        }   
    }
}
