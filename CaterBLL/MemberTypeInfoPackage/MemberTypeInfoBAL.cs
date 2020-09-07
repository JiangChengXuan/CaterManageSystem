using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDAL;
using CaterModel;
using CaterDAL.MemberTypeInfoPackage;
using CaterModel.Enum;
using CaterDAL.MemberInfoPackage;
namespace CaterBLL.MemberTypeInfoPackage
{

    /// <summary>
    /// 会员分类业务逻辑类
    /// </summary>
  public  class MemberTypeInfoBAL
    {

         public MemberTypeInfoBAL()
      {
          memberTypeInfoDAL = new MemberTypeInfoDAL();
      }

         private MemberTypeInfoDAL memberTypeInfoDAL { get; set; }


        /// <summary>
        /// 获取所有未删除的会员信息
        /// </summary>
        /// <returns></returns>
         public List<MemberTypeInfo> GetMemberTypeInfoList()
         {
             return memberTypeInfoDAL.GetMemberTypeInfoList();
         }


        /// <summary>
        /// 获取会员折扣信息
        /// </summary>
        /// <returns></returns>
         public List<KeyValue> GetDiscountInfo()
         {
             return memberTypeInfoDAL.GetEnumData<DiscountType>();
         } // END GetDiscountInfo（）


        /// <summary>
         /// 添加会员类型
        /// </summary>
         public bool AddMemberTypeInfo(MemberTypeInfo data,out string msg)
         {
             msg = "处理成功";

             MemberTypeInfo memberTypeInfo = memberTypeInfoDAL.GetMemberTypeInfoByName(data.MTitle);

             if (memberTypeInfo != null) {
                 msg = "该会员已经存在不能重复添加";
                 return false;
             }

             data.MDiscount = data.MDiscount / 10;  //转为运算数值（小数）
             int rowCount = memberTypeInfoDAL.AddMemberTypeInfo(data);

             if (rowCount<=0)
             {
                 msg = "处理失败";
             }

             return true;
         }  // END AddMemberTypeInfo（）


            
        /// <summary>
        /// 会员类型修改
        /// </summary>
         public bool Edit(MemberTypeInfo data,out string msg)
         {
             msg = "处理成功";
             data.MDiscount = Convert.ToDecimal(data.MDiscount) / 10;

              int rowCount= memberTypeInfoDAL.Edit(data);

              if (rowCount <= 0) {
                  msg = "处理失败";
                  return false;
              }

              return true;
         }  // end Edit


        /// <summary>
        /// 逻辑删除会员类型
        /// </summary>
         public bool Delete(int mid,out string msg)
         {
             msg = "删除成功";

             //删除前判断会员类型是否存在会员信息
             MemberInfoDAL mdal=new MemberInfoDAL();
             int count = mdal.QueryCountByTypeId(mid);
             if (count>0)
             {
                 msg = "删除失败，会员类型还存在会员信息";
                 return false;
             }
             int rowCount = memberTypeInfoDAL.EditMIsDelete(mid);

             if (rowCount<=0)
             {
                 msg = "删除失败";
                 return false;
             }

             return true;
         } // end Delete（）
        

    }
}
