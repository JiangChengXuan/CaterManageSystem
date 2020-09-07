using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System.Data;
using System.Collections;
using NPOI.XSSF.UserModel;
namespace CaterCommon
{
  public  class NPOIHelper
    {
      
 
      List<string> ColumnTitle = new List<string>();
 
       
      /// <summary>
      /// 将对象集合导出为Excel文件
      /// </summary>
      /// <typeparam name="T">指定导出的数据对象的类型</typeparam>
      /// <param name="dataSource">导出数据源</param>
      /// <param name="savePath">导出文件保存的地址</param>
      public void Export<T>(List<T> dataSource, string savePath)
      {
          //根据文件扩展名实例化工作簿对象
          IWorkbook workBook = null;
          string fileExt = Path.GetExtension(savePath).ToLower();
          if (fileExt==".xls")
          {
              workBook = new HSSFWorkbook();
          }
          else if (fileExt == ".xlsx")
          {
              workBook = new XSSFWorkbook();
          }
          if (workBook == null) return;

          //创建工作页
          ISheet sheet = workBook.CreateSheet();
 
          //创建标题行
          CreateColmunsTitle<T>(sheet, workBook);

           //创建数据列
          for (int i = 0; i < dataSource.Count; i++)
          {
              T data = dataSource[i];
              IRow row = sheet.CreateRow((i+1));
              
              for (int a = 0; a < ColumnTitle.Count; a++)
              {
                  string titleName = ColumnTitle[a];
                  ICell cell = row.CreateCell(a);
                  Type type = data.GetType();
                  PropertyInfo prop= type.GetProperty(titleName);
                  var cellVal = prop.GetValue(data).ToString();
                  cell.SetCellValue(cellVal);
              }

          }

          using (Stream fsWrite = new FileStream(savePath,FileMode.Create,FileAccess.Write))
          {
              workBook.Write(fsWrite);
          }

      } // END Export（）

       /// <summary>
      /// 将Excel文件导入到DataTable
       /// </summary>
       /// <param name="filePath">要导入的Excel文件路径</param>
      /// <returns>DataTable</returns>
      public DataTable ImportToDataTable(string filePath)
      {
          DataTable dt = new DataTable(); // result

          #region 将Excel读取到文件流，并实例化工作簿对象

          string fileExt = Path.GetExtension(filePath).ToLower();  //文件扩展名     
          Stream fsRead = new FileStream(filePath, FileMode.Open, FileAccess.Read);
          IWorkbook workBook = null;
          if (fileExt == ".xls")
          {
              workBook = new HSSFWorkbook(fsRead);
          }
          else if (fileExt == ".xlsx")
          {
              workBook = new XSSFWorkbook(fsRead);
          }
          if (workBook == null) return dt;
          fsRead.Dispose();       

          #endregion
         
          //获取工作簿的第一个sheet页
          ISheet sheet = workBook.GetSheetAt(0);

          //sheet对象获取读取行的迭代器，用于遍历所有行
          IEnumerator rows = sheet.GetRowEnumerator();
   
          bool isReadTitle = false;  //是否读取生成了DataTable的标题列

          while (rows.MoveNext())
          {
              #region 获取当前遍历的行对象
              IRow row = null;
              object currentRow = rows.Current;
              if (currentRow is HSSFRow)
              {
                  row = currentRow as HSSFRow;
              }
              else if (currentRow is XSSFRow)
              {
                  row = currentRow as XSSFRow;
              }
              if (row == null) continue; 
              #endregion

              DataRow dtRow = dt.NewRow(); //创建数据行
    
              for (int i = 0; i < row.LastCellNum; i++)  //循环Excel行所有的单元格
              {
                  ICell cell = row.GetCell(i);
                  if (!isReadTitle)// 将首行生成为标题列
                  {                 
                      dt.Columns.Add(cell.ToString(), typeof(string));
                  }
                  else//非首行作为DataTable数据行
                  {               
                      dtRow[i] = cell.ToString();
                  }
              } // END for
         
              if (!isReadTitle)
              {
                  isReadTitle = true;
              }
              else
              {
                  dt.Rows.Add(dtRow);
              }

          } // END while（）

          return dt;
      }  // END Import（）



      /// <summary>
      /// 创建表格列标题
      /// </summary>
      private void CreateColmunsTitle<T>(ISheet sheet, IWorkbook workBook)
      {
          //创建首行，即为标题行
          IRow row = sheet.CreateRow(0);

          //根据实体模型的属性创建标题项
          Type type = typeof(T);
          PropertyInfo[] props = type.GetProperties();

 
          for (int i = 0; i < props.Count(); i++)
          {
              PropertyInfo prop = props[i];
              ICell cell = row.CreateCell(i);

              //设置样式
              cell.CellStyle = CreateTitleStyle(workBook);

              var cellVal = prop.Name;
              cell.SetCellValue(cellVal);
              ColumnTitle.Add(cellVal);
          }

      } // END HSSFWorkbook（）


      /// <summary>
      /// 创建表格标题的样式
      /// </summary>
      /// <returns></returns>
      private ICellStyle CreateTitleStyle(IWorkbook workBook)
      {
          ICellStyle style = workBook.CreateCellStyle();

          IFont font = workBook.CreateFont();
          font.Boldweight = (short)FontBoldWeight.BOLD; //加粗
          style.SetFont(font);

          return style;
      } // END CreateTitleStyle（）


    }
}
