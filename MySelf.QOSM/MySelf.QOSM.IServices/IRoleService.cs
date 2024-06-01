using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.IServices
{
    public interface IRoleService
    {
        List<RoleInfo> GetRoleList(bool isDel);
        List<UIRole> GetCboRoles();
        bool DeleteRole(RoleInfo roleInfo);
        bool RecoveryRole(RoleInfo roleInfo);
        bool RemoveRole(RoleInfo roleInfo);
        bool ExistRole(string roleName);    
        bool SaveRole(RoleInfo roleInfo);
        bool HasRoleUsers(int roleId);
        bool SaveRolePermissionSet(List<int> menuIds, int roleId);

    }
}
