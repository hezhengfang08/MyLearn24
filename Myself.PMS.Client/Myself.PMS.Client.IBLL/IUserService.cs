namespace Myself.PMS.Client.IBLL
{
    public interface IUserService
    {
        bool Login(string username, string password);
    }
}
