using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;
using CaterModel.MemberInfoModel;
using CaterCommon;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;
using System.Reflection;
namespace CaterDAL.MemberInfoPackage
{
    /// <summary>
    /// 会员信息数据访问类
    /// </summary>
   public class MemberInfoDAL:BaseDAL
    {

       /// <summary>
       /// 获取会员信息
       /// </summary>
       public List<MemberInfoTableShow> GetDataList(Dictionary<string,string>whereDic=null)
       {
           List<MemberInfoTableShow> list = new List<MemberInfoTableShow>();
           List<SQLiteParameter> para = new List<SQLiteParameter>(); 

           StringBuilder sql = new StringBuilder();
           sql.Append("  select  t1.MId,t1.MName,t2.MTitle,t1.MPhone,t1.MMoney ");
           sql.Append(" from MemberInfo t1 inner join  MemberTypeInfo t2 ");
           sql.Append(" on t1.MTypeId=t2.MID  where t1.MIsDelete=0");

           //生成查询条件
           if (whereDic != null && whereDic.Count>0)
           {
               foreach (KeyValuePair<string, string> item in whereDic)
               {
                   sql.AppendFormat(" and {0}  like @{1}",item.Key,item.Key);
                   para.Add(new SQLiteParameter(item.Key,"%"+item.Value+"%"));
               }
           }

           DataTable dt = dbHelper.GetDataTable(sql.ToString(), para.ToArray());

           DataTableToListColByDataTable(dt, list);

           return list;
       } // end GetDataList()

       /// <summary>
       /// 添加会员
       /// </summary>
       public int Add(CaterModel.MemberInfoModel.MemberInfo data)
       {
           StringBuilder sql = new StringBuilder();
           sql.Append("insert into MemberInfo(MTypeId,MName,MPhone,MMoney,MIsDelete) ");
           sql.Append("values(@MTypeId,@MName,@MPhone,@MMoney,0)");

           List<DbParameter> paras = dbHelper.CreateParameter(sql.ToString(), data);

           return dbHelper.ExecuteNoQuery(sql.ToString(), paras.ToArray());
 
       }  // END Add


       /// <summary>
       /// 修改会员
       /// </summary>
       public int Edit(CaterModel.MemberInfoModel.MemberInfo data)
       {
           StringBuilder sql = new StringBuilder("update MemberInfo set MTypeId=@MTypeId,");
           sql.Append("MName=@MName,MPhone=@MPhone,MMoney=@MMoney");
           sql.Append("  where MId=@MId");

           List<DbParameter> paras = dbHelper.CreateParameter(sql.ToString(), data);

           return dbHelper.ExecuteNoQuery(sql.ToString(), paras.ToArray());
       }// end Edit

       /// <summary>
       /// 修改删除状态
       /// </summary>
       public int EditMIsDelete(int MId)
       {
           string sql = "update MemberInfo set MIsDelete=1 where MId=@MId ";

           return dbHelper.ExecuteNoQuery(sql, new SQLiteParameter("MId", MId));

       }// END EditMIsDelete（）


       /// <summary>
       /// 根据会员类型查询对应的会员信息
       /// </summary>
       public int QueryCountByTypeId(int typeId)
       {
           string sql = "select count(MId) from MemberInfo where MTypeId=@MTypeId";
           object count = dbHelper.ExecuteScalar(sql, new SQLiteParameter("MTypeId", typeId));

           return Convert.ToInt32(count);
       }// END QueryCountByTypeId()

      
       /// <summary>
       /// 指定编号或者手机号码查询会员信息，两个条件匹配一项即可返回会员信息
       /// </summary>
       /// <param name="mId">会员编号</param>
       /// <param name="phone">手机号码</param>
       /// <returns>会员信息</returns>
       public MemberInfoTableShow GetMemberInfoByIdOrPhone(long mId,string phone)
       {
           StringBuilder sb = new StringBuilder("select m1.MId,m1.MName,m1.MPhone,m1.MMoney,");
           sb.Append("m2.MTitle,m2.MDiscount  from MemberInfo m1 ");
           sb.AppendLine(" inner join MemberTypeInfo m2 on m1.MTypeId=m2.MId");
           sb.AppendLine(" where (m1.MId =@MId or m1.MPhone=@MPhone)");
           string sql=sb.ToString();

           List<DbParameter> paras = dbHelper.CreateParameter(sql, new MemberInfoTableShow() { MId = mId, MPhone = phone });

           DataTable dt = dbHelper.GetDataTable(sql, paras.ToArray());

           List<MemberInfoTableShow> list = new List<MemberInfoTableShow>();
           DataTableToListColByDataTable(dt, list);

           if (list.Count>0)
           {
               return list[0];
           }

           return null;
       } // END GetMemberInfoByIdOrPhone（）



    }
}
