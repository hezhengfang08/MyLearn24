using Myself.PMS.Client.Common;
using Myself.PMS.Client.SystemModule.Models;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.SystemModule.ViewModels.Dialogs
{
    public class ModifyMenuViewModel : DialogViewModelBase
    {
        public MenuModel MenuModel { get; set; } =
               new MenuModel();
        public List<Entities.MenuEntity> ParentNodes { get; set; } =
                new List<Entities.MenuEntity>();
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var model = parameters.GetValue<MenuModel>("model");
            ParentNodes = parameters.GetValue<List<Entities.MenuEntity>>("parents");
            ParentNodes.Insert(0, new Entities.MenuEntity { MenuHeader = "ROOT", MenuId = "-1" });

            if (model == null)
            {
                Title = "新增菜单项";
                MenuModel.ParentId = "-1";
            }
            else
            {
                Title = "编辑菜单项";

                MenuModel.MenuId = model.MenuId;
                MenuModel.MenuHeader = model.MenuHeader;
                MenuModel.ParentId = model.ParentId;
                MenuModel.TargetView = model.TargetView;
                MenuModel.MenuIcon = model.MenuIcon;
            }
        }
        public override void DoSave()
        {
            base.DoSave();
        }
    }
}
