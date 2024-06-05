using MySelf.QOSM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.SM
{
    public class RoleModel:ModelBase
    {
        public RoleModel() { }

        public RoleModel(RoleInfo roleInfo  )
        {
            Role = roleInfo;
            IsDel = roleInfo.IsAdmin ? false : true;
            ShowDel = GetVisibility(roleInfo.IsDeleted, true);
            ShowEdit = GetVisibility(roleInfo.IsDeleted, true);
            ShowRecovery = GetVisibility(roleInfo.IsDeleted, false);
            ShowRemove = GetVisibility(roleInfo.IsDeleted, false);
        }
        #region 属性
        private bool  isDel;

        public bool IsDel
        {
            get { return isDel; }
            set { isDel = value;

                OnPropertyChanged();
            }
        }

        private RoleInfo role;

        public RoleInfo Role
        {
            get { return role; }
            set { role = value;
                OnPropertyChanged();
            }
        }


        #endregion
    }
}
