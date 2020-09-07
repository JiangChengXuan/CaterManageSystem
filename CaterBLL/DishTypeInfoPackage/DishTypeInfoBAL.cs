using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel.DishTypeInfoModel;
using CaterDAL.DishTypeInfoPackage;
using CaterModel.Enum;
namespace CaterBLL.DishTypeInfoPackage
{

    /// <summary>
    /// 菜品分类业务逻辑类
    /// </summary>
    public class DishTypeInfoBAL
    {

        private DishTypeInfoDAL dal = new DishTypeInfoDAL();

        /// <summary>
        /// 获取所有菜品分类
        /// </summary>
        public List<DishTypeInfo> GetList(Dictionary<string, string> dicWhere = null)
        {
            return dal.GetList();

        }


        /// <summary>
        /// 新增分类
        /// </summary>
        public bool Add(DishTypeInfo data,out CaterMessage msg)
        {
            msg = CaterMessage.Success;

            int exist = dal.GetDishTypeInfoByName(data.DTitle);

            if (exist>0)
            {
                msg = CaterMessage.IsExist;
                return false;
            }

            int rowCount = dal.Add(data);
            if (rowCount<=0)
            {
                msg = CaterMessage.Fail;
                return false;
            }

            return true;
        } // END Add()


          /// <summary>
     /// 修改菜品分类
     /// </summary>
        public bool Edit(DishTypeInfo data, out CaterMessage msg)
        {
            msg = CaterMessage.Success;

            int rowcount = dal.Edit(data);
            if (rowcount<=0)
            {
                msg = CaterMessage.Fail;
                return false;
            }

            return true;
        } // END Edit（）


        /// <summary>
        /// 删除菜品分类
        /// </summary>
        public bool Delete(int id,out CaterMessage msg)
        {
            msg = CaterMessage.Success;

            int rowCount = dal.EditFieldDIsDelete(DeleteState.Deleted, id);

            if (rowCount<=0)
            {
                msg = CaterMessage.Fail;
                return false;
            }

            return true;
        }// END DId（）


     


    }
}
