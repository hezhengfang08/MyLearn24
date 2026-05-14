using Myself.PMS.Server.Entities;
using Myself.PMS.Server.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Service
{
    public class OwnerService : IOwnerService
    {
        ISqlSugarClient _client;
        public OwnerService(ISqlSugarClient client)
        {
            _client = client;
        }

        public int DeleteOwner(int id)
        {
            return _client.Deleteable<OwnerEntity>().In(id).ExecuteCommand();
        }

        public BuildingEntity[] GetBuildings()
        {
            return _client.Queryable<BuildingEntity>().ToArray();
        }


        public OwnerEntity[] GetOwners(ConditionEntity[] conditions, int pageIndex, int pageSize, ref int totalCount)
        {
            var query = _client.Queryable<OwnerEntity>();

            // 第一种方案
            //List<IConditionalModel> conditionals = new List<IConditionalModel>();
            //foreach (var pair in conditions)
            //{
            //    conditionals.Add(new ConditionalModel { FieldName = pair.Name, ConditionalType = ConditionalType.Like, FieldValue = pair.Value });
            //}

            // 第二种方案    二选一
            var c = conditions.FirstOrDefault(c => c.Name == "HouseHolder");
            if (c != null)
                query.Where(o => o.HouseHolder.Contains(c.Value));

            c = conditions.FirstOrDefault(c => c.Name == "IdNumber");
            if (c != null)
                query.Where(o => o.IdNumber.Contains(c.Value));

            c = conditions.FirstOrDefault(c => c.Name == "Phone");
            if (c != null)
                query.Where(o => o.Phone.Contains(c.Value));

            c = conditions.FirstOrDefault(c => c.Name == "RoomNum");
            if (c != null)
                query.Where(o => o.RoomNum.Contains(c.Value));


            return query.Clone()
                .ToPageList(pageIndex, pageSize, ref totalCount)
                .ToArray();
        }


        public QuarterEntity[] GetQuarters()
        {
            return _client.Queryable<QuarterEntity>().ToArray();
        }

        public int UpdateOwner(OwnerEntity owner)
        {
            if (owner.OwnerId == 0)
            {
                return _client.Insertable(owner).ExecuteCommand();
            }
            else
            {
                return _client.Updateable(owner).ExecuteCommand();
            }
        }
    }
}
