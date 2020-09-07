using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Text.RegularExpressions;
namespace CaterDAL.DBAccess
{
  public abstract  class DBHelper
    {
 

        public    string ConStr {
            get
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString;
                return conStr == null ? "" : conStr;
            }
            
        }

        public   abstract int ExecuteNoQuery(string sql,params DbParameter [] paras);

        public   abstract DataTable GetDataTable(string sql, params DbParameter[] paras);

        public   abstract object ExecuteScalar(string sql, params DbParameter[] paras);



        /// <summary>
        /// 根据参数化的SQL语句和类中赋值的数据生成参数化集合对象
        /// </summary>
        /// <typeparam name="T">获取参数对象取值的类型，要生成什么类型的参数化对象就指定什么类型</typeparam>
        /// <param name="sql">参数化的SQL语句</param>
        /// <param name="data">参数对象取值的类型的实例（该对象无需所有属性都要赋值，只针对需要做参数化的属性进行赋值）</param>
        /// <returns></returns>
        public abstract List<DbParameter> CreateParameter<T>(string sql, T data);
      

    }
}
