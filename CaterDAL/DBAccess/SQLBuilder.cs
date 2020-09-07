using CaterModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
namespace CaterDAL.DBAccess
{
    /// <summary>
    /// 数据库查询类型
    /// </summary>
    public enum SelectType
    {
        All, Count, AllWhere
    }

  public  class SelectTemplate
    {
        public string PKName { get; set; }
        public SelectType Type { get; set; }

        private readonly string tempSQL = "select @cols from @table  ";

     
        public List<string> WhereItems { get; set; }

        /// <summary>
        /// 查询统计需要的信息：
        /// [0] count(列)、[1] where 条件列
        /// </summary>
        public  string[] CountInfo=new string[2];


      /// <summary>
      /// 创建查询SQL语句
      /// </summary>
      /// <param name="type">表名</param>
      /// <returns></returns>
        public StringBuilder CreateSelect(Type type)
        {
            StringBuilder selectSQL = new StringBuilder(tempSQL);
            string tableName=type.Name;
            selectSQL = selectSQL.Replace("@table ", tableName);

            if (Type == SelectType.AllWhere)
            {
                selectSQL = selectSQL.Replace("@cols", "*");

                if (WhereItems.Count>0)
                {
                    selectSQL.AppendFormat(" where  {0}=@{0} ", WhereItems[0]);
                }
                WhereItems.RemoveAt(0);

                foreach (string where in WhereItems)
                {
                    selectSQL.AppendFormat(" and {0}=@{0} ", where);
                }
            }
            else if (Type== SelectType.Count)
            {
                selectSQL = selectSQL.Replace("@cols", "count(" + CountInfo[0] + ")");
                selectSQL.AppendFormat("where {0}=@{0}", CountInfo[1]);
            }

            return selectSQL;
        }  // END CreateSelect（）

    }

    public class SQLBuilder<T>
    {

       public SQLBuilder() { }
       public SQLBuilder(SelectTemplate temp) {

           temp.PKName = GetAutoColumn();
           SelectTemp = temp;
       }

       /// <summary>
       /// 是否使用参数化查询
       /// </summary>
        public bool  IsParameter { get; set; }

       /// <summary>
       /// 查询模板对象
       /// </summary>
        public SelectTemplate SelectTemp { get; set; }

        [Obsolete("此方法已过时，请使用新的重载")]
        public string CreateSQL(SQLType sqlType,params string [] column)
        {
            StringBuilder sql = new StringBuilder();
            Type type = typeof(T);

            if (sqlType == SQLType.Select)
            {
                sql = SelectTemp.CreateSelect(type);
            }
            else if (sqlType== SQLType.Insert)
            {
                sql = CreateInsert(type);
            }
            else if (sqlType== SQLType.Update)
            {
                sql = CreateUpdate(type, column);
            }
        


            return sql.ToString();

        } // END CreateSQL（）


        public string CreateSQL(SQLType sqlType, Dictionary<List<string>, List<string>> updateIno)
        {
            StringBuilder sql = new StringBuilder();
            Type type = typeof(T);

            if (sqlType == SQLType.Select)
            {
                sql = SelectTemp.CreateSelect(type);
            }
            else if (sqlType == SQLType.Insert)
            {
                sql = CreateInsert(type);
            }    
            else if (sqlType == SQLType.UpdatePart)
            {
                sql = CreateUpdate(type, updateIno);
            }
         


            return sql.ToString();
        }


        /// <summary>
        /// 创建删除的SQL语句
        /// </summary>
        /// <param name="type">操作的类型</param>
        /// <param name="whereCols">where 条件列，可指定多个列作为条件</param>
        /// <returns></returns>
        public StringBuilder CreateDelete(Type type, params string[] whereCols)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" delete from {0}  where ",type.Name);

            if (whereCols.Length>0)
            {
                sql.AppendFormat("   {0}=@{0}", whereCols[0]);

                foreach (string col in whereCols)
                {
                    sql.AppendFormat(" and {0}=@{0}", col);
                }

            }

            return sql;
        } // END CreateDelete（）


        private StringBuilder CreateUpdate(Type type, Dictionary<List<string>, List<string>> updateIno)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("Update  {0} set ", type.Name);

            foreach (KeyValuePair<List<string>, List<string>> item in updateIno)
            {
                List<string> columns=item.Value;
                foreach (string col in columns)
                {
                    sql.AppendFormat("{0}=@{0},", col); 
                }

                //去掉多出的  , 符合
                sql = sql.Remove(sql.Length - 1, 1);

                List<string> wheres = item.Key;
                if (wheres.Count>0)
                {
                    sql.AppendFormat(" where {0}=@{0} ", wheres[0]);
                    wheres.RemoveAt(0);
                    foreach (string where in wheres)
                    {
                        sql.AppendFormat(" and {0}=@{0} ", where);
                    }
                }
             

     
            }

            return sql;
        }  // END CreateUpdate()


        /// <summary>
        /// 根据类的属性生成新增的SQL
        /// </summary>
        private StringBuilder CreateUpdate(Type type,params string [] column)
        {
            StringBuilder InsertSQL = new StringBuilder();
            InsertSQL.AppendFormat("Update  {0} set ", type.Name);
            PropertyInfo[] props = type.GetProperties();

            if (column.Length>0) // 指定了部分列进行修改
            {
                foreach (string col in column)
                {                  
                      InsertSQL.AppendFormat("{0}=@{0},", col);
                }
            }
            else  //未指定修改列， 根据所有属性生成要修改的列
            {         
                foreach (PropertyInfo prop in props)
                {
                    if (IsAutoColumn(prop)) continue;  //自增长列不作为修改对象
                    InsertSQL.AppendFormat("{0}=@{0},", prop.Name);
                }
            }
         
            //去掉多出的  , 符合
            InsertSQL = InsertSQL.Remove(InsertSQL.Length - 1, 1);

            //获取自增长的列作为where条件
            string pkName = GetAutoColumn(props);
            if (!string.IsNullOrEmpty(pkName))
            {
                InsertSQL.AppendFormat(" where {0}=@{0}",pkName);
            }

            return InsertSQL;
        } // END CreateInsert（）


       /// <summary>
       /// 获取主键属性名称
       /// </summary>
        private string GetAutoColumn(PropertyInfo[] props)
        {        
            foreach (PropertyInfo prop in props)
            {
                Attribute attr = prop.GetCustomAttribute(typeof(DescriptionAttribute));
                if (attr is DescriptionAttribute)
                {
                    DescriptionAttribute desc = attr as DescriptionAttribute;
                    if (desc.Description=="AUTOINCREMENT")
                    {
                        return prop.Name;
                    }
                }
            }
            return string.Empty;
        }   // END GetAutoColumn（）


        /// <summary>
        /// 获取主键属性名称
        /// </summary>
        private string GetAutoColumn()
        {
            Type type = typeof(T);

            PropertyInfo[] props = type.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                Attribute attr = prop.GetCustomAttribute(typeof(DescriptionAttribute));
                if (attr is DescriptionAttribute)
                {
                    DescriptionAttribute desc = attr as DescriptionAttribute;
                    if (desc.Description == "AUTOINCREMENT")
                    {
                        return prop.Name;
                    }
                }
            }
            return string.Empty;
        }   // END GetAutoColumn（）


       /// <summary>
       /// 根据类的属性生成新增的SQL
       /// </summary>
        private StringBuilder CreateInsert(Type type)
        {
            StringBuilder InsertSQL = new StringBuilder();
            InsertSQL.AppendFormat("insert into {0}",type.Name);
            PropertyInfo[] props = type.GetProperties();
           
         
            string cols = "(";
            string vals = " values(";
            foreach (PropertyInfo prop in props)
            {
                //判断是否自增长列
                if (IsAutoColumn(prop))
                {
                    continue;
                }
                cols += prop.Name + ",";
                vals += "@" + prop.Name + ",";
            }
            cols = cols.Remove(cols.Length-1)+")";
            vals = vals.Remove(vals.Length - 1) + ")";

            InsertSQL.Append(cols).Append(vals);
            

            return InsertSQL;
        } // END CreateInsert（）


       /// <summary>
       /// 判断属性在数据库中是自增长
       /// </summary>
        private bool IsAutoColumn(PropertyInfo prop)
        {

            Attribute attr=prop.GetCustomAttribute(typeof(DescriptionAttribute));
            if (attr==null)
            {
                  return false;
            }

            if (attr is DescriptionAttribute)
            {
                DescriptionAttribute descAttr = attr as DescriptionAttribute;
                if (descAttr==null||descAttr.Description!="AUTOINCREMENT")
                {
                    return false;
                }
            }

            return true;
        } // END PropertyInfo（）


         




    }
}
