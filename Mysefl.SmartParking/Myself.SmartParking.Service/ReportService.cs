using Microsoft.EntityFrameworkCore;
using Myself.SmartParing.IService;
using Myself.SmartParking.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Service
{
    public class ReportService : BaseService, IReportService
    {
        public ReportService(DbContext context) : base(context)
        {
        }

        public List<ReportInfo> GetParkingReport(DateTime st, DateTime et)
        {
            // 符合时间条件的数据过滤出来
            var all = this.Query<OrderInfo>(q => true).ToList();
            all = all.Where(
                q =>
                DateTime.ParseExact(
                    q.LeaveTime.ToString(),
                    "yyyyMMddHHmmss",
                    CultureInfo.InvariantCulture) >= st &&
                DateTime.ParseExact(
                    q.LeaveTime.ToString(),
                    "yyyyMMddHHmmss",
                    CultureInfo.InvariantCulture) <= et
            ).ToList();

            all = all.Select(s => new OrderInfo
            {
                LeaveTime = DateTime.ParseExact(
                      s.LeaveTime.ToString(),
                      "yyyyMMddHHmmss",
                      CultureInfo.InvariantCulture)
                      .ToString("yyyy-MM-dd"),
                Payable = s.Payable,
                Payment = s.Payment
            }).ToList();

            // 按时间分组  GroupBy   string   年月日时分秒    按天汇总
            //如果需要做支付类型区分，
            return all.GroupBy(s => new { s.LeaveTime })
                   .Select(s => new ReportInfo
                   {
                       DateTime = s.Key.LeaveTime,
                       OrderCount = s.Count(),
                       Payable = (double)s.Sum(s => s.Payable),
                       PaymentCash = 0.0,
                       PaymentElec = (double)s.Sum(s => s.Payable)
                   }).ToList();
        }
    }
}
