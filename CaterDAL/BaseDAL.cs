using CaterDAL.DBAccess;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CaterModel.Enum;
using System.Data.Common;
using System.ComponentModel;
namespace CaterDAL
{
   public class BaseDAL
    {

        protected DBHelper dbHelper { get; set; }

        public BaseDAL()
        {
            dbHelper = DBFactory.GetDBHelperByReflection();
        }

         /// <summary>
         /// 将枚举中的枚举项目生成键值对集合
         /// </summary>
         /// <typeparam name="T">指定的枚举类型</typeparam>
         /// <param name="isDesc">是否将枚举名转换成描述</param>
         /// <returns></returns>
        public List<KeyValue> GetEnumData<T>(bool isDesc=false)
        {
            List<KeyValue> list = new List<KeyValue>();

            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields();

            foreach (FieldInfo field in fields)
            {
                if (!field.IsSpecialName)
                {
                    string val = field.Name;
                    int id = Convert.ToInt32(field.GetRawConstantValue());

                    if (isDesc)
                    {
                        DescriptionAttribute attr = field.GetCustomAttribute(typeof(DescriptionAttribute)) 
                            as DescriptionAttribute;
                        val = attr.Description;
                    }

                    list.Add(new KeyValue { ID = id, Value = val });
                }
            }

            return list;
        } // END LoadManagerType（）

       
       /// <summary>
        /// 将查询出的DataTable转换成指定类型的集合。
        /// 集合中类型的列来源于实体的属性，有多少属性就往DataTable获取只来为属性赋值
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="dt"></param>
       /// <param name="list"></param>
        public static void DataTableToListColByModel<T>(DataTable dt, List<T> list)
        {
            Type type = typeof(T);
            PropertyInfo[] pros = type.GetProperties();

            foreach (DataRow row in dt.Rows)
            {
                T obj = (T)Activator.CreateInstance(type);
                foreach (PropertyInfo pro in pros)
                {
                    if (row[pro.Name] == null || row[pro.Name] == DBNull.Value) continue;

                    var val = Convert.ChangeType(row[pro.Name], pro.PropertyType);
                    pro.SetValue(obj, val);
                }
                list.Add(obj);
            }
        } // END DataTableBindingToList（）


       /// <summary>
        /// 将查询出的DataTable转换成指定类型的集合。
        /// 集合中类型的列来源于DataTable的列，DataTable有多少列就创建多少属性 
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="dt"></param>
       /// <param name="list"></param>
        public static void DataTableToListColByDataTable<T>(DataTable dt, List<T> list)
        {
            foreach (DataRow row in dt.Rows)
            {
                Type type = typeof(T);
                T obj = (T)Activator.CreateInstance(type);

                foreach (DataColumn col in dt.Columns)
                {
                    if (row[col.ColumnName] == null || row[col.ColumnName] == DBNull.Value) continue;

                    var val = Convert.ChangeType(row[col.ColumnName], col.DataType);
                    PropertyInfo prop = type.GetProperty(col.ColumnName);
                    prop.SetValue(obj, val);
                }
                list.Add(obj);
            } // foreach
        } // END DataTableToListColByDataTable（）


        /// <summary>
        /// 将查询出的DataTable转换成指定类型的集合。
        /// 集合中类型的列来源于DataTable的列，DataTable有多少列就创建多少属性 
        /// </summary>
        private List<T> DTToListColByDT<T>(DataTable dt, List<T> list)
        {
            foreach (DataRow row in dt.Rows)
            {
                Type type = typeof(T);
                T obj = (T)Activator.CreateInstance(type);

                foreach (DataColumn col in dt.Columns)
                {
                    if (row[col.ColumnName] == null || row[col.ColumnName] == DBNull.Value) continue;

                    var val = Convert.ChangeType(row[col.ColumnName], col.DataType);
                    PropertyInfo prop = type.GetProperty(col.ColumnName);
                    prop.SetValue(obj, val);
                }
                list.Add(obj);
            } // foreach

            return list;
        } // END DataTableToListColByDataTable（）






       
       /// <summary>
       /// 查询数据
       /// </summary>
       /// <typeparam name="T">查询的目标类型，约定类型名称对应表名</typeparam>
       /// <param name="data">查询条件值</param>
       /// <param name="where"></param>
       /// <returns></returns>
        public List<T> Select<T>(T data,params string [] where)
        {
            List<T> list = new List<T>();

            //生成SQL语句
            SQLBuilder<T> sqlBuilder = new SQLBuilder<T>(
                new SelectTemplate() {  Type= SelectType.AllWhere ,WhereItems=where.ToList()});
            string sql = sqlBuilder.CreateSQL(SQLType.Select);

            //参数化
            List<DbParameter> paras = dbHelper.CreateParameter(sql, data);

            //数据操作
             DataTable dt=   dbHelper.GetDataTable(sql,paras.ToArray());

            //将数据绑定到实体对象上
            return  DTToListColByDT(dt, list);

        }  // END SelectAll()


       


       /// <summary>
       /// 查询统计
       /// </summary>
       /// <typeparam name="T">进行数据查询的数据类型</typeparam>
        /// <param name="countInfo">
        /// 查询统计需要的信息：[0] count(列)、[1] where的条件列
        /// </param>
        public  int Count<T>(string[] countInfo,T data)
        {
            List<T> list = new List<T>();

            //1.生成SQL语句
            SelectTemplate temp = new SelectTemplate();
            temp.Type = SelectType.Count;
            temp.CountInfo = countInfo;
            SQLBuilder<T> sqlBuilder = new SQLBuilder<T>(temp);
            string sql = sqlBuilder.CreateSQL(SQLType.Select);

            //2.参数化对象
            List<DbParameter> paras = dbHelper.CreateParameter(sql, data);

            //3.数据操作
            object rowCount = dbHelper.ExecuteScalar(sql, paras.ToArray());

           return Convert.ToInt32(rowCount);
        }  // END SelectAll()

        
       /// <summary>
       /// 新增数据
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="data"></param>
       /// <param name="sqlType">会根据此数据生成对应的SQL语句类型</param>
       /// <returns></returns>

        public int InsertOrUpdate<T>(T data, SQLType sqlType)
        {
            //1.生成SQL语句
            SQLBuilder<T> sqlBuilder = new SQLBuilder<T>();
            string sql = sqlBuilder.CreateSQL(sqlType);

            //2.参数化对象
            List<DbParameter> paras = dbHelper.CreateParameter(sql, data);

            //3.数据操作
            return dbHelper.ExecuteNoQuery(sql, paras.ToArray());

        }  // END Insert（）

       /// <summary>
       /// 新增后返回主键
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="data"></param>
       /// <param name="sqlType"></param>
       /// <returns></returns>
        public long InsertReturnID<T>(T data)
        {
            //1.生成SQL语句
            SQLBuilder<T> sqlBuilder = new SQLBuilder<T>();
            string sql = sqlBuilder.CreateSQL(SQLType.Insert);
            sql += ";"+Environment .NewLine+ "select last_insert_rowid()";
            
            //2.参数化对象
            List<DbParameter> paras = dbHelper.CreateParameter(sql, data);

            //3.数据操作
             object id= dbHelper.ExecuteScalar(sql, paras.ToArray());
             return Convert.ToInt64(id);
        }  // END Insert（）


       /// <summary>
       /// 
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="data"></param>
       /// <param name="sqlType"></param>
       /// <param name="column">指定要修改的列名</param>
       /// <returns></returns>
        public int InsertOrUpdate<T>(T data, SQLType sqlType,params string[] column)
        {
            //1.生成SQL语句
            SQLBuilder<T> sqlBuilder = new SQLBuilder<T>();
            string sql = sqlBuilder.CreateSQL(sqlType, column);

            //2.参数化对象
            List<DbParameter> paras = dbHelper.CreateParameter(sql, data);

            //3.数据操作
            return dbHelper.ExecuteNoQuery(sql, paras.ToArray());

        }  // END Insert（）


        /// <summary>
        /// 执行数据修改操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="updateIno">key：where 条件列、value：set修改列</param>
        /// <returns></returns>
        public int Update<T>(T data, Dictionary<List<string>, List<string>> updateIno)
        {
            //1.生成SQL语句
            SQLBuilder<T> sqlBuilder = new SQLBuilder<T>();
            string sql = sqlBuilder.CreateSQL(SQLType.UpdatePart, updateIno);

            //2.参数化对象
            List<DbParameter> paras = dbHelper.CreateParameter(sql, data);

            //3.数据操作
            return dbHelper.ExecuteNoQuery(sql, paras.ToArray());

        }  // END Update（）


       /// <summary>
       /// 进行数据删除操作
       /// </summary>
       /// <typeparam name="T">操作的类型</typeparam>
       /// <param name="data">删除需要的数据</param>
       /// <param name="whereCols">指定删除的筛选条件的列</param>
       /// <returns></returns>
        public int Delete<T>(T data, params string [] whereCols)
        {
            //1.生成SQL语句
            SQLBuilder<T> sqlBuilder = new SQLBuilder<T>();
            string sql = sqlBuilder.CreateDelete(data.GetType(), whereCols).ToString();

            //2.参数化对象
            List<DbParameter> paras = dbHelper.CreateParameter(sql, data);

            //3.数据操作
            return dbHelper.ExecuteNoQuery(sql, paras.ToArray());

        }  // END Update（）



    }
}
