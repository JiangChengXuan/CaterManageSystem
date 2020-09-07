using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;
using CaterCommon;
using CaterModel.Enum;
using CaterDAL.MenuInfoPackage;
using CaterModel.MenuModel;
namespace CaterBLL.MenuInfoPackage
{
  public  class MenuInfoBAL
    {
      private MenuInfoDAL menuInfoDAL { get; set; }
      public MenuInfoBAL()
      {
          menuInfoDAL = new MenuInfoDAL();
      }

        /// <summary>
        /// 根据角色类型获取对应的菜单
        /// </summary>
        /// <returns></returns>
      public List<MenuInfo> GetMenuInfoByRoleID(string roleId)
      {
          return menuInfoDAL.GetMenuInfoByRoleID(roleId);
        
      }


    }
}
