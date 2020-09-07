using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;
using CaterModel.MemberInfoModel;
using CaterDAL.MemberInfoPackage;
using System.Reflection;
namespace CaterBLL.MemberInfoPackage
{
    public class MemberInfoBAL
    {
        public MemberInfoBAL()
        {
            dal = new MemberInfoDAL();
        }

        private MemberInfoDAL dal { get; set; }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        public List<MemberInfoTableShow> GetDataList(Dictionary<string, string> paras = null)
        {
            return dal.GetDataList(paras);
        }

        /// <summary>
        /// 添加会员
        /// </summary>
        public bool Add(CaterModel.MemberInfoModel.MemberInfo data, out string msg)
        {
            msg = "处理成功";

            int rowCount = dal.Add(data);

            if (rowCount<=0)
            {
                msg = "处理失败";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 将展示数据实体(联合查询结果集)从的数据取出复制到操作对象上
        /// </summary>
        /// <param name="tableShow"></param>
        /// <returns>操作(添加、修改。。。)对象</returns>
        private CaterModel.MemberInfoModel.MemberInfo TableShowToMemberInfo(MemberInfoTableShow tableShow)
        {
          
            Type type = typeof(CaterModel.MemberInfoModel.MemberInfo);
            object obj = Activator.CreateInstance(type);
            PropertyInfo[] props = type.GetProperties();

            Type type_ts = typeof(MemberInfoTableShow);

            foreach (PropertyInfo prop in props)
            {
               PropertyInfo prop_ts=  type_ts.GetProperty(prop.Name);
               var setVal = Convert.ChangeType(prop_ts.GetValue(tableShow), prop.PropertyType);
               prop.SetValue(obj, setVal);
            }

            return (CaterModel.MemberInfoModel.MemberInfo)obj;
        } // END TableShowToMemberInfo（）


        
       /// <summary>
       /// 修改会员
       /// </summary>
        public bool Edit(CaterModel.MemberInfoModel.MemberInfo data,out string msg)
        {
            msg = "处理成功";

            int rowCount = dal.Edit(data);

            if (rowCount<=0)
            {
                msg = "处理失败";
                return false;
            }

            return true;
        }  // END Edit（）


        /// <summary>
        /// 删除会员
        /// </summary>
        public bool Delete(int mid, out string msg)
        {
            msg = "删除成功";

            int rowCount = dal.EditMIsDelete(mid);

            if (rowCount <= 0)
            {
                msg = "删除失败";
                return false;
            }

            return true;
        }  // END Edit（）


             /// <summary>
       /// 指定编号或者手机号码查询会员信息，两个条件匹配一项即可返回会员信息
       /// </summary>
       /// <param name="mId">会员编号</param>
       /// <param name="phone">手机号码</param>
       /// <returns>会员信息</returns>
        public MemberInfoTableShow GetMemberInfoByIdOrPhone(long mId, string phone)
        {
            return dal.GetMemberInfoByIdOrPhone(mId, phone);
        }


    }
}
