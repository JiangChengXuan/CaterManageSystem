using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDAL.ManagerInfoPackage;
using CaterModel;
using CaterCommon;
using CaterModel.Enum;
namespace CaterBLL.ManagerInfoPackage
{
  public  class ManagerInfoBLL
    {
      public ManagerInfoBLL()
      {
          ManagerInfoDal = new ManagerInfoDAL();
      }

      private ManagerInfoDAL ManagerInfoDal { get; set; }


      public List<ManagerInfo> GetManagerInfoList()
      {
          return ManagerInfoDal.GetManagerInfoList();
      }


      /// <summary>
      /// 加载管理员类型
      /// </summary>
      public List<KeyValue> GetManagerTypeList()
      {
          return ManagerInfoDal.GetEnumData<ManagerInfo.ManagerType>();
      }

      /// <summary>
      /// 添加管理员
      /// </summary>
      public bool AddManagerInfo(ManagerInfo managerInfo,out string errorMsg)
      {
          int rowCount = 0;
          errorMsg = string.Empty;

          //添加之前需要检查用户名是否存在，避免重复
          ManagerInfo checkObj = ManagerInfoDal.GetManagerInfoByMName(managerInfo.MName);

          if (checkObj==null) //不存在，可以添加
          {
              //执行新增数据操作
              rowCount = ManagerInfoDal.AddManagerInfo(managerInfo, Encryption.GetHashValue);
          }
          else  //存在，不能重复添加
          {
              errorMsg = "添加失败，用户名已经存在";
          }

 

        return rowCount > 0;
      }

      /// <summary>
      /// 修改管理员
      /// </summary>
      public bool EditManagerInfo(ManagerInfo managerInfo)
      {
          int rowCount = 0;
          rowCount = ManagerInfoDal.EditManagerInfo(managerInfo, Encryption.GetHashValue);
          return rowCount > 0;
      }

      /// <summary>
      /// 删除管理员
      /// </summary>
      public bool DeleteManagerInfo(int MId)
      {
          int rowCount = 0;
          rowCount = ManagerInfoDal.DeleteManagerInfo(MId);
          return rowCount > 0;
      }


      /// <summary>
      /// 登录系统
      /// </summary>
      public LoginState Login(string userName,string pwd,out ManagerInfo userInfo)
      {
            userInfo = ManagerInfoDal.GetManagerInfoByMName(userName);

          //用户名错误
          if (userInfo == null) 
          {
              return LoginState.UnameError;
          }

          //密码错误
          if (Encryption.GetHashValue(pwd) != userInfo.MPwd)
          {
              return LoginState.PwdError;
          }

          return LoginState.Success;
      }// END Login()



    }
}
