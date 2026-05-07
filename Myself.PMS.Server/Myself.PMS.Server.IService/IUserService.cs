namespace Myself.PMS.Server.IService
{
    public interface IUserService
    {
        bool CheckLogin(string username, string password);
    }
}
