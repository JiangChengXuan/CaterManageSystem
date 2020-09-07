using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;
using System.Data;
using CaterCommon;
using System.Data.SQLite;
using System.Data.Common;
namespace CaterDAL.MemberTypeInfoPackage
{

    /// <summary>
    /// 会员类型数据访问类
    /// </summary>
    public class MemberTypeInfoDAL : BaseDAL
    {

        /// <summary>
        /// 获取所有未删除的会员类型
        /// </summary>
        /// <returns></returns>
        public List<MemberTypeInfo> GetMemberTypeInfoList()
        {
            List<MemberTypeInfo> list = new List<MemberTypeInfo>();

            string sql = "select *  from MemberTypeInfo where MIsDelete=0";
            DataTable dt = dbHelper.GetDataTable(sql);

            DataTableToListColByModel<MemberTypeInfo>(dt, list);
            return list;
        }// END GetMemberTypeInfoList（）


        /// <summary>
        /// 添加会员类型
        /// </summary>
        /// <returns></returns>
        public int AddMemberTypeInfo(MemberTypeInfo data)
        {
            string sql = "insert into MemberTypeInfo(MTitle,MDiscount,MIsDelete) values(@MTitle,@MDiscount,0)";

            List<DbParameter> paras = dbHelper.CreateParameter(sql, data);

            return dbHelper.ExecuteNoQuery(sql, paras.ToArray());

        } // END AddMemberTypeInfo（）

        /// <summary>
        /// 根据会员类型名称获取会员类型信息
        /// </summary>
        /// <returns></returns>
        public MemberTypeInfo GetMemberTypeInfoByName(string MTitle)
        {
            List<MemberTypeInfo> list = new List<MemberTypeInfo>();
        
            string sql = "select * from MemberTypeInfo where MTitle=@MTitle";

            DataTable dt = dbHelper.GetDataTable(sql, new SQLiteParameter("MTitle", MTitle));

            if (dt.Rows.Count <= 0) return null;

            DataTableToListColByModel<MemberTypeInfo>(dt, list);

            if (list.Count<=0) return null;
             

            return list[0];
        } // END GetMemberTypeInfoByName（）


        /// <summary>
        /// 会员类型修改
        /// </summary>
        public int Edit(MemberTypeInfo data)
        {
            string sql = "update MemberTypeInfo set MTitle=@MTitle,MDiscount=@MDiscount where MId=@MId";

            List<DbParameter> paras = dbHelper.CreateParameter(sql, data);

            return dbHelper.ExecuteNoQuery(sql, paras.ToArray());
        }  // END Edit（）


        /// <summary>
        /// 修改逻辑删除字段
        /// </summary>
        public int EditMIsDelete(int mid)
        {
            string sql = " update MemberTypeInfo set MIsDelete=1 where MId=@MId";

            return dbHelper.ExecuteNoQuery(sql, new SQLiteParameter("MId", mid));

        } // END EditMIsDelete（）


    }
}
