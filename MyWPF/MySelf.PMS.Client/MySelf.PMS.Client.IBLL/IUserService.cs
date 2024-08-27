using MySelf.PMS.Client.Entities;


namespace MySelf.PMS.Client.IBLL
{
    public interface IUserService
    {
        EmployeeEntity Login(string username, string password);
        bool UpdatePassword(int id, string opd, string npd);

        EmployeeEntity[] GetUsers(string key);
    }
}
