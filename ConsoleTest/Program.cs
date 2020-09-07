using CaterDAL;
using CaterDAL.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using System.Data.OracleClient;
using System.Text.RegularExpressions;
using CaterModel.Enum;
using CaterCommon;
using Microsoft.International.Converters.PinYinConverter;
using CaterModel;
using CaterBLL.DishInfoPackage;
using CaterModel.DishInfoModel;
using CaterBLL;
using CaterModel.DataModel;
namespace ConsoleTest
{
    
    class Program
    {
 
        static void Main(string[] args)
        {
            string conStr = "Data Source=ORCL;uid=PBOC_GAC_HGQ;pwd=PBOC_GAC_HGQ;";

            using (OracleConnection con=new OracleConnection(conStr))
            {
                con.Open();
                OracleCommand com = new OracleCommand("sp_DataPaging", con);
                com.CommandType = CommandType.StoredProcedure;

                OracleParameter[] paras = new OracleParameter[] { 
                new OracleParameter("tableName","pboc_general_info"),
                    new OracleParameter("orderByFiled","PBOC_PKID"),
                      new OracleParameter("pageIndex",2),
                              new OracleParameter("pageSize",2),
                  new OracleParameter("rowtotal", OracleType.Int32),
                          new OracleParameter("pageCount", OracleType.Int32)
                };

                paras[4].Direction =ParameterDirection.Output;
                paras[5].Direction = ParameterDirection.Output;

                DataTable dt = new DataTable();
                OracleDataAdapter da = new OracleDataAdapter(com);
                da.Fill(dt);

            }


            string debug = string.Empty;
        }

        
         

        

    }
}
