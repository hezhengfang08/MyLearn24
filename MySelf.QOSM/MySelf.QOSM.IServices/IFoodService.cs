using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.IServices
{
    public interface IFoodService
    {
        PageModel<ViewFoodInfo> GetFoodList(string keywords,int fType,bool showDel,int startIndex,int pageSize);
        bool DeleteFood(int foodId);
        bool RecoveryFood(int foodId);
        bool RemoveFood(int foodId);
        bool SaveFood(FoodInfo foodInfo);
        bool ExistFood(string foodName);
        bool SetFoodOnline(List<int> foodIds, bool isOnline);
        List<UIFoodItem> GetOnlineFoodList(int fType);
        bool AddFoodOrder(FoodOrderInfo foodOrderInfo,List<CustomerOrderInfo> orders);
    }
}
