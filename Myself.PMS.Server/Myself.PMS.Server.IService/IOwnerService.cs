using Myself.PMS.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.IService
{
    public interface IOwnerService
    {
        OwnerEntity[] GetOwners(ConditionEntity[] conditionss, int pageIndex, int pageSize, ref int totalCount);

        QuarterEntity[] GetQuarters();
        BuildingEntity[] GetBuildings();
    }
}
