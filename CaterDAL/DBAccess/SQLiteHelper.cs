using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Reflection;
namespace CaterDAL.DBAccess
{
   public class SQLiteHelper:DBHelper
    {
        

        public   override int ExecuteNoQuery(string sql, params DbParameter[] paras)
        {
            using (SQLiteConnection con=new SQLiteConnection(ConStr))
            {
                con.Open();
                SQLiteCommand com = new SQLiteCommand(sql,con);
                com.Parameters.AddRange(paras);
              return  com.ExecuteNonQuery();
            }
        }

        public override  DataTable GetDataTable(string sql, params DbParameter[] paras)
        {
            DataTable dt = new DataTable();

            using (SQLiteConnection con = new SQLiteConnection(ConStr))
            {
                con.Open();
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, con);
                da.SelectCommand.Parameters.AddRange(paras);
                da.Fill(dt);
            }

            return dt;
        }


        public override object ExecuteScalar(string sql, params DbParameter[] paras)
        {
            object result = null;

            using (SQLiteConnection con = new SQLiteConnection(ConStr))
            {
                con.Open();
                SQLiteCommand com = new SQLiteCommand(sql, con);
                com.Parameters.AddRange(paras);
                result= com.ExecuteScalar();
            }

            return result;
        }


       /// <summary>
       /// 根据参数化的SQL语句和类中赋值的数据生成参数化集合对象
       /// </summary>
       /// <typeparam name="T">获取参数对象取值的类型，要生成什么类型的参数化对象就指定什么类型</typeparam>
        /// <param name="sql">参数化的SQL语句</param>
        /// <param name="data">参数对象取值的类型的实例（该对象无需所有属性都要赋值，只针对需要做参数化的属性进行赋值）</param>
       /// <returns></returns>
        public   override List<DbParameter> CreateParameter<T>(string sql,T data)
        {
          
            List<DbParameter> list = new List<DbParameter>();

            if (data == null)
            {
                return null;
            }

               //根据SQL获取参数列表
            sql = sql.Replace("where", "");
            string pattern = "(?<=@)\\w+";
            MatchCollection matches = Regex.Matches(sql, pattern);

            Type type = data.GetType();
 
            foreach (Match match in matches)
            {
                string fieldName = match.Value;
               PropertyInfo prop= type.GetProperty(fieldName);
               var val = prop.GetValue(data);

               if (val == null) continue;
               
               val= Convert.ChangeType(val, prop.PropertyType);
               list.Add(new SQLiteParameter (fieldName,val));
            }

            return list;
        }



    }
}
