using Microsoft.EntityFrameworkCore;
using Myself.SmartParing.IService;
using Myself.SmartParking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Service
{
    public class RechargeSerivce : BaseService, IRechargeService
    {
        public RechargeSerivce(DbContext context) : base(context)
        {
        }

        public IEnumerable<MemberRecharge> GetRecharges(string skey, int pageSize, int pageIndex, out int totalCount)
        {
            var pResult = this.QueryPage<MemberRecharge, string>(
                    q => string.IsNullOrEmpty(skey) ||
                        q.AutoLisence.Contains(skey),

                    pageSize,
                    pageIndex,
                    order => order.AutoLisence,
                    false
                );

            totalCount = pResult.TotalCount;

            return from q in pResult.DataList
                   join fm in this.Set<BaseFeeMode>() on q.FeeModeId equals fm.FeeModeId
                   select new MemberRecharge
                   {
                       RId = q.RId,
                       AutoLisence = q.AutoLisence,
                       FeeModeId = fm.FeeModeId,
                       FeeModeName = fm.FeeModeName,
                       RechargeCount = q.RechargeCount,
                       RechargeEndTime = q.RechargeEndTime,
                       RechargeStartTime = q.RechargeStartTime,
                       CreateId = q.CreateId,
                       CreateName = q.CreateName,
                       CreateTime = q.CreateTime,
                       State = q.State
                   };
        }
    }
}
