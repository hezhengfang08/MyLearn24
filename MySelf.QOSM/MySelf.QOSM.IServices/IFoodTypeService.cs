using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.IServices
{
    public interface IFoodTypeService
    {
        PageModel<FoodTypeInfo> GetFoodType(string keywords,bool showDel,int startIndex, int pageSize);
        List<UIFType> GetCboFType();
        bool DeleteFType(FoodTypeInfo foodTypeInfo);
        bool RecoveryFType(FoodTypeInfo foodTypeInfo);  
        bool RemoveFType(FoodTypeInfo fType);
        bool ExistFType(string fTypeName);
        bool SaveFType(FoodTypeInfo foodTypeInfo);
        bool IsHasFoods(int fTypeId);
    }
}
