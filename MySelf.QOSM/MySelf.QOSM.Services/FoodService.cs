using Microsoft.EntityFrameworkCore;
using MySelf.QOSM.Common;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using Org.BouncyCastle.Asn1.X9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Services
{
    internal class FoodService : BaseService, IFoodService
    {
        public bool AddFoodOrder(FoodOrderInfo foodOrderInfo, List<CustomerOrderInfo> orders)
        {
            //订单号，取单号
            return ExecuteTrans(() =>
            {
                foodOrderInfo.OrderNo = CreateOrderNo(foodOrderInfo.CustomerId);
                foodOrderInfo.PickCode = GetPicCode();
                foodOrderInfo.OrderTime = DateTime.Now;
                Insert(foodOrderInfo);
                if (foodOrderInfo.OrderId > 0)
                {
                    //保存订单菜单明细
                    orders.ForEach(f =>
                    {
                        f.OrderId = foodOrderInfo.OrderId;
                    });
                    Insert<CustomerOrderInfo>(orders);
                }
            }, () =>
            {
                Detach(foodOrderInfo);
                DetachList(orders);
            });
        }
        private string GetPicCode()
        {
            string pickCode = "";
            string strMonth = DateTime.Now.Month.ToString("00");
            string strDay = DateTime.Now.Day.ToString("00");
            var codes = Query<FoodOrderInfo>(f => f.OrderNo.Substring(6, 2) == strDay).OrderByDescending(f => f.OrderId).Select(f => f.PickCode).ToList();
            if (codes.Count > 0)
            {
                string lastCode = codes[0];
                int count = lastCode.Split('-')[1].GetInt();//最后一个尾号的整数值
                pickCode = strMonth + strDay + "-" + (count + 1).ToString("0000");
            }
            else
                pickCode = strMonth + strDay + "-" + "0001";//当天的第一个取餐号
            return pickCode;
        }
        private string CreateOrderNo(int customerId)
        {
            string orderNo = string.Empty;
            string strTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string strCustId = customerId.ToString();
            int len = strCustId.Length;
            int totalWidth = 5;
            char paddingChar = '0';
            strCustId = strCustId.PadLeft(totalWidth, paddingChar);
            orderNo = strTime + strCustId;
            return orderNo;
        }
        public bool DeleteFood(int foodId)
        {
            return UpdateFoodDelState(foodId, 0, true);
        }

        public bool ExistFood(string foodName)
        {
            return Query<FoodInfo>(f => f.FoodName == foodName && !f.IsDeleted).Any();
        }
        private bool UpdateFoodDelState(int foodId, int delType, bool isDeleted)
        {
            var food = Find<FoodInfo>(foodId);
            bool res = false;
            if (delType == 0)
            {
                food.IsDeleted = isDeleted;
                dbContext.Entry(food).State = EntityState.Modified;
                Commit();
                res = true;
                Detach(food);
            }
            else
            {
                Delete(food);
                res = true;
            }
            return res;
        }

        public PageModel<ViewFoodInfo> GetFoodList(string keywords, int fType, bool showDel, int startIndex, int pageSize)
        {
            PageModel<ViewFoodInfo> res = new PageModel<ViewFoodInfo>();
            var foodList = Query<ViewFoodInfo>(f => f.IsDeleted == showDel);
            if (foodList.Any())
            {
                if (!string.IsNullOrEmpty(keywords))
                {
                    foodList = foodList.Where(f => f.FoodName.Contains(keywords) || f.FtypeName.Contains(keywords) || f.Remark.Contains(keywords));
                }
                if (fType > 0)
                {
                    foodList = foodList.Where(f => f.FtypeId == fType);
                }
                if (foodList.Any())
                {
                    res.DataList = foodList.Skip(startIndex - 1).Take(pageSize).ToList();
                    res.TotalCount = foodList.Count();
                }
            }
            return res;
        }

        public List<UIFoodItem> GetOnlineFoodList(int fType)
        {
            return Query<FoodInfo>(f => f.IsOn && f.FtypeId == fType && !f.IsDeleted).Select(f => new UIFoodItem()
            {
                FoodId = f.FoodId,
                FoodName = f.FoodName,
                FoodImg = f.FoodImg,
                FoodPrice = f.FoodPrice,
                FTypeId = fType,
                FoodAmount = f.PackAmount
            }).ToList();
        }

        public bool RecoveryFood(int foodId)
        {
            return UpdateFoodDelState(foodId, 0, false);
        }

        public bool RemoveFood(int foodId)
        {
            return UpdateFoodDelState(foodId, 1, true);
        }
        public bool SaveFood(FoodInfo foodInfo)
        {
            var res = false;
            if ((foodInfo.FoodId == 0))
            {
                Insert(foodInfo);
                if (foodInfo.FoodId > 0)
                    res = true;
            }
            else
            {
                Update(foodInfo);
                if (dbContext.Entry(foodInfo).State == EntityState.Unchanged)
                {
                    res = true;
                }
            }
            Detach(foodInfo);
            return res;
        }

        public bool SetFoodOnline(List<int> foodIds, bool isOnline)
        {
            var foods = Query<FoodInfo>(f => foodIds.Contains(f.FoodId) && !f.IsDeleted).ToList();
            return ExecuteTrans(() =>
            {
                foods.ForEach(f =>
            {
                f.IsOn = isOnline;
                dbContext.Entry(f).State = EntityState.Modified;

            });
            }, () => { DetachList(foods); });
        }
    }
}
