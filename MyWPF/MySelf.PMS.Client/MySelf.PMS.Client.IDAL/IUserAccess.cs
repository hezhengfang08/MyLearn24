

namespace MySelf.PMS.Client.IDAL
{
    public interface IUserAccess : IWebAccess
    {
        string Login(string username, string password);
        string UpdatePassword(int id, string opd, string npd);

        string GetUsers(string kye);
    }
}
