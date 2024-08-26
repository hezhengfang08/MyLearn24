using MySelf.PMS.Client.Common;
using MySelf.PMS.Client.IBLL;
using MySelf.PMS.Client.SystemModule.Models;
using System.Windows;

namespace MySelf.PMS.Client.SystemModule.ViewModels.Dialogs
{
    public class ModifyMenuViewModel : DialogViewModelBase
    {
        public MenuModel MenuModel { get; set; } =
                new MenuModel();

        public List<Entities.MenuEntity> ParentNodes { get; set; } =
                new List<Entities.MenuEntity>();

        IMenuService _menuService;
        public ModifyMenuViewModel(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var model = parameters.GetValue<MenuModel>("model");
            ParentNodes = parameters.GetValue<List<Entities.MenuEntity>>("parents");
            ParentNodes.Insert(0, new Entities.MenuEntity { MenuHeader = "ROOT", MenuId = "-1" });

            if (model == null)
            {
                Title = "新增菜单项";
                MenuModel.ParentId = "-1";
                MenuModel.MenuId = "";
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
            try
            {
                Entities.MenuEntity menuEntity = new Entities.MenuEntity
                {
                    MenuId = MenuModel.MenuId,
                    MenuHeader = MenuModel.MenuHeader,
                    // 需要把字体字符转成编码  \ue618
                    MenuIcon = ((int)MenuModel.MenuIcon.ToArray()[0]).ToString("x"),
                    TargetView = MenuModel.TargetView,
                    ParentId = MenuModel.ParentId,
                    State = 1
                };

                _menuService.UpdateMenu(menuEntity);

                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}