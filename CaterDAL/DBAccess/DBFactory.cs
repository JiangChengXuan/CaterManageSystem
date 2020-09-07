using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDAL.DBAccess;
using System.Configuration;
using System.Reflection;
namespace CaterDAL
{
    public enum DBType
    { 
        SQLite,SQLServer,Oracle
    }

    /// <summary>
    /// 配置文件+反射+简单工厂 ，实现可配置可扩展架构
    /// </summary>
   public class DBFactory
    {

        private static string DBConfig = ConfigurationManager.AppSettings["DBConfig"];

       private static string DBReflectionConfig = ConfigurationManager.AppSettings["DBReflectionConfig"];

       public static DBHelper GetDBHelperByConfig()
       {
           DBHelper dbHelper = null;

           DBType dbType = (DBType)Enum.Parse(typeof(DBType), DBConfig);

           switch (dbType)
           {
               case DBType.SQLite: dbHelper = new SQLiteHelper();
                   break;
               case DBType.SQLServer:
                   break;
               case DBType.Oracle:
                   break;
               default:
                   break;
           }

           return dbHelper;
       }  // END GetDBHelperByConfig（）

       public static DBHelper GetDBHelperByReflection()
       {
           DBHelper dbHelper = null;

           string assemblyName = DBReflectionConfig.Substring(0, DBReflectionConfig.LastIndexOf(","));
           string className = DBReflectionConfig.Substring(DBReflectionConfig.LastIndexOf(",") + 1);

           Assembly asm = Assembly.Load(assemblyName);
           Type type = asm.GetType(className);

           dbHelper = (DBHelper)Activator.CreateInstance(type);

           return dbHelper;
       }  // END GetDBHelperByReflection（）
       
      


    }
}
