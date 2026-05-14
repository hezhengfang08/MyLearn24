using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.IDAL;
using Myself.PMS.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.BLL
{
    public class OwnerService : BaseService, IOwnerService
    {
        IOwnerAccess _ownerAccess;
        public OwnerService(IOwnerAccess ownerAccess)
        {
            _ownerAccess = ownerAccess;
        }

        public int DeleteOwner(int id)
        {
            string json = _ownerAccess.DeleteOwner(id);
            return this.GetResult<int>(json);
        }

        public BuildingEntity[] GetBuildings()
        {
            string json = _ownerAccess.GetBuildings();
            return this.GetResult<BuildingEntity[]>(json);
        }

        public PageEntity<OwnerEntity[]> GetOwners(ConditionEntity[] keys, int index, int size)
        {
            string json = keys.Serialize();
            json = _ownerAccess.GetOwners(json, index, size);
            return this.GetResult<PageEntity<OwnerEntity[]>>(json);
        }

        public QuarterEntity[] GetQuarters()
        {
            string json = _ownerAccess.GetQuarters();
            return this.GetResult<QuarterEntity[]>(json);
        }

        public int UpdateOwner(OwnerEntity owner)
        {
            string json = _ownerAccess.UpdateOwner(owner.Serialize());
            return this.GetResult<int>(json);
        }
    }
}
