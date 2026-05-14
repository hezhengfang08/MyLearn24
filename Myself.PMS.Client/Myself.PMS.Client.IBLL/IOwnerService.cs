using Myself.PMS.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.IBLL
{
    public interface IOwnerService
    {
        PageEntity<OwnerEntity[]> GetOwners(ConditionEntity[] keys, int index, int size);

        QuarterEntity[] GetQuarters();
        BuildingEntity[] GetBuildings();
        int UpdateOwner(OwnerEntity owner);
        int DeleteOwner(int id);

    }
}
