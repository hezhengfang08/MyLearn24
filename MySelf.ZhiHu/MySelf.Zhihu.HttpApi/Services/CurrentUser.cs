
using System.Security.Claims;

namespace MySelf.Zhihu.HttpApi.Services
{
    public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
    {
        public readonly ClaimsPrincipal? User = httpContextAccessor.HttpContext?.User;
        public string? Username => User?.FindFirstValue(ClaimTypes.Name);
        public int? Id
        {
            get
            {
                var id = User?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (id is null) return null;

                return Convert.ToInt32(id);
            }
        }
    }
}
