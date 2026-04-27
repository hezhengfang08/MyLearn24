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
    public class AutoService : BaseService, IAutoService
    {
        public AutoService(DbContext context)
                   : base(context)
        {
        }
        public IEnumerable<AutoRegister> GetAutoList(string key, int pageSize, int pageIndex, out int totalCount)
        {
            var pResult = this.QueryPage<AutoRegister, string>(q =>
                                                  string.IsNullOrEmpty(key) ||
                                                  q.AutoLicense.Contains(key),

                                                  pageSize,
                                                  pageIndex,
                                                  order => order.AutoLicense,
                                                  false
                                                  );
            totalCount = pResult.TotalCount;

            return from a in pResult.DataList
                   join lc in this.Set<BaseLicenseColor>() on a.LicenseColorId equals lc.ColorId
                   join lt in this.Set<BaseLicenseType>() on a.LicenseType equals lt.TypeId
                   join ac in this.Set<BaseAutoColor>() on a.AutoColorId equals ac.ColorId
                   join fm in this.Set<BaseFeeMode>() on a.FeeModeId equals fm.FeeModeId
                   select new AutoRegister
                   {
                       AutoId = a.AutoId,
                       AutoLicense = a.AutoLicense,
                       LicenseColorId = a.LicenseColorId,
                       LicenseColorName = lc.ColorName,
                       LicenseType = a.LicenseType,
                       LicenseTypeName = lt.TypeName,
                       AutoColorId = a.AutoColorId,
                       AutoColorName = ac.ColorName,
                       FeeModeId = a.FeeModeId,
                       FeeModeName = fm.FeeModeName,
                       Description = a.Description,
                       ValidCount = a.ValidCount,
                       ValidEndTime = a.ValidEndTime,
                       ValidStartTime = a.ValidStartTime,
                       State = a.State
                   };
        }
    
    }
}
