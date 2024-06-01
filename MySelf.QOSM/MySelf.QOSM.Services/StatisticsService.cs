using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Services
{
    public class StatisticsService : IStatisticsService
    {
        public List<UIPayWayStatItem> StaticsAmountByPayWay(string startTime, string endTime)
        {
            throw new NotImplementedException();
        }

        public List<UIFTypeStatItem> StaticsFoodCountByFType(string startTime, string endTime)
        {
            throw new NotImplementedException();
        }

        public List<UIFoodCountItem> StaticsFoodSaleCountList(int fTypeId, string startTime, string endTime)
        {
            throw new NotImplementedException();
        }
    }
}
