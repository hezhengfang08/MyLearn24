using MySelf.QOSM.Models.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.IServices
{
    public  interface IStatisticsService
    {
        /// <summary>
        /// 按支付类别统计销售额合支付笔数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        List<UIPayWayStatItem> StaticsAmountByPayWay(string startTime, string endTime);
        /// <summary>
        /// 按类别统计菜品销量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        List<UIFTypeStatItem> StaticsFoodCountByFType(string startTime, string endTime);
        /// <summary>
        /// 统计指定类别中各菜品的效率
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        List<UIFoodCountItem> StaticsFoodSaleCountList(int fTypeId, string startTime, string endTime);


    }
}
