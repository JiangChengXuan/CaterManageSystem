using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CaterModel;
using CaterDAL.DBAccess;
using CaterCommon;
using System.Reflection;
using System.Data.SQLite;
using System.Data.Common;
using CaterModel.MenuModel;
namespace CaterDAL.MenuInfoPackage
{
    public class MenuInfoDAL : BaseDAL
    {
        /// <summary>
        /// 根据角色类型获取对应的菜单
        /// </summary>
        /// <returns></returns>
        public List<MenuInfo> GetMenuInfoByRoleID(string roleId)
        {
            List<MenuInfo> list = new List<MenuInfo>();

            string sql = @"select  m.Mid,m.MenuName,m.MenuParent,m.MenuImage,m.MenuPath 
from RoleAndMenuRelation r
inner join MenuInfo m on r.menuId=m.Mid
where r.roleId=@roleId order by Mid";

            SQLiteParameter para = new SQLiteParameter("roleId", roleId);

            DataTable dt = dbHelper.GetDataTable(sql, para);

             DataTableToListColByModel<MenuInfo>(dt, list);

            return list;
        }  //END GetManagerInfoList（）

    }
}
