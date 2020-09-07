using CaterModel.DataModel;
using CaterModel.MemberInfoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;
using CaterModel.Enum;
namespace CaterDAL
{
  public  class OrderInfoDAL:BaseDAL
    {



        /// <summary>
        /// 订单进行结账
        /// </summary>
        /// <param name="orderInfo">订单对象</param>
        /// <param name="member">会员对象</param>
        /// <returns>处理结果</returns>
      public int OrderCheckOut(OrderInfo orderInfo, MemberInfoTableShow member, bool IsBalancePay)
        {
            
            string sql = string.Empty;
            List<DbParameter> paras = null;
            int rowCount = 0;
            SQLiteTransaction tran = null;

            using (SQLiteConnection con = new SQLiteConnection(dbHelper.ConStr))
            using(SQLiteCommand com=new SQLiteCommand(con))
            {
                try
                {
                    con.Open();
                      tran = con.BeginTransaction();
                    com.Transaction = tran;

                    #region Step1-订单信息处理
                    if (member != null)
                    {
                        sql = "update OrderInfo set MemberId=@MemberId, IsPay=1,Discount=@Discount where OId=@OId";
                    }
                    else
                    {
                        sql = "update  OrderInfo set   IsPay=1  where OId=@OId";
                    }
                    paras = dbHelper.CreateParameter(sql, new OrderInfo{ MemberId=member.MId, Discount=member.MDiscount, OId=orderInfo.OId});
                    com.CommandText = sql;
                    com.Parameters.AddRange(paras.ToArray());
                    rowCount += com.ExecuteNonQuery();

                    com.Parameters.Clear();
                    #endregion


                    #region Step2-餐桌信息处理
                
                    sql = "update   TableInfo  set TIsFree=1 where TId=@TId ";
                    paras = dbHelper.CreateParameter(sql, new TableInfo { TId = orderInfo.TableId });
                    com.CommandText = sql;
                    com.Parameters.AddRange(paras.ToArray());
                    rowCount += com.ExecuteNonQuery();

                    com.Parameters.Clear();
                    #endregion


                    #region Step-3会员扣款
                    if (member != null && IsBalancePay)
                    {
                     
                        decimal money = member.MMoney - orderInfo.OMoney*member.MDiscount;
                        sql = "update MemberInfo set MMoney=@MMoney where MId=@MId";
                        paras = dbHelper.CreateParameter(sql, new MemberInfo { MId = member.MId, MMoney = money });
                        com.CommandText = sql;
                        com.Parameters.AddRange(paras.ToArray());
                        rowCount += com.ExecuteNonQuery();
                    }
                    #endregion

                    tran.Commit();


                }
                catch (Exception)
                {
                    rowCount = 0;
                    tran.Rollback();
                }
                finally
                {
                    if (tran != null) tran.Dispose();
                }

            } // END using

            return rowCount;
        } // END OrderCheckOut（）

    }
}
