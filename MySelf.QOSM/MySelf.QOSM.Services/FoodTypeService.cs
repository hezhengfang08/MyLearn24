using Microsoft.EntityFrameworkCore;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Services
{
    public class FoodTypeService : BaseService, IFoodTypeService
    {

        public PageModel<FoodTypeInfo> GetFoodTypeList(string keywords, bool showDel, int startIndex, int pageSize)
        {
            PageModel<FoodTypeInfo> result = new PageModel<FoodTypeInfo>();
            var typeList = Query<FoodTypeInfo>(t => t.IsDeleted == showDel);
            if (typeList.Any())
            {
                if (!string.IsNullOrEmpty(keywords))
                {
                    typeList = typeList.Where(t => t.FtypeName.Contains(keywords) || t.Remark.Contains(keywords));
                }
                if (typeList.Any())
                {
                    result.DataList = typeList.Skip(startIndex - 1).Take(pageSize).ToList();
                    result.TotalCount = typeList.Count();
                }
            }
            return result;
        }

        public List<UIFType> GetCboFTypes()
        {
            var types = Query<FoodTypeInfo>(t => t.IsDeleted == false).Select(t => new UIFType()
            {
                FtypeId = t.FtypeId,
                FtypeName = t.FtypeName
            }).ToList();
            return types;
        }

        public bool DeleteFoodType(FoodTypeInfo type)
        {
            return UpdateFTypeDelState(type, 0, true);
        }

        public bool RecoverFoodType(FoodTypeInfo type)
        {
            return UpdateFTypeDelState(type, 0, false);
        }

        public bool RemoveFoodType(FoodTypeInfo type)
        {
            return UpdateFTypeDelState(type, 1, true);
        }

        /// <summary>
        /// 类别信息的删除、恢复、真删除
        /// </summary>
        /// <param name="type"></param>
        /// <param name="delType"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        private bool UpdateFTypeDelState(FoodTypeInfo type, int delType, bool isDeleted)
        {
            bool blDel = false;
            if (delType == 0)//删除或恢复
            {
                type.IsDeleted = isDeleted;
                Update(type);
                blDel = true;
                Detach(type);
            }
            else
            {
                Delete(type);
                blDel = true;
            }
            return blDel;
        }





        public bool IsHasFoods(int typeId)
        {
            var type = Query<FoodTypeInfo>(t => t.FtypeId == typeId).Include(t => t.Foods).First();
            if (type != null)
            {
                if (type.Foods.Count > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public List<UIFType> GetCboFType()
        {
            throw new NotImplementedException();
        }

        public bool DeleteFType(FoodTypeInfo foodTypeInfo)
        {
            throw new NotImplementedException();
        }

        public bool RecoveryFType(FoodTypeInfo foodTypeInfo)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFType(FoodTypeInfo fType)
        {
            throw new NotImplementedException();
        }

        public bool ExistFType(string fTypeName)
        {
            return Query<FoodTypeInfo>(t => t.FtypeName == fTypeName && t.IsDeleted == false).Any();
        }

        public bool SaveFType(FoodTypeInfo foodTypeInfo)
        {
            bool blSave = false;
            if (foodTypeInfo.FtypeId == 0)
            {
                Insert(foodTypeInfo);
                if (foodTypeInfo.FtypeId > 0)
                    blSave = true;
            }
            else
            {
                Update(foodTypeInfo);
                if (dbContext.Entry(foodTypeInfo).State == EntityState.Unchanged)
                    blSave = true;
            }
            Detach(foodTypeInfo);
            return blSave;
        }
    }
}
