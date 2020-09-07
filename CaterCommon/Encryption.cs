using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaterCommon
{
    /// <summary>
    /// 加密类
    /// </summary>
   public class Encryption
    {

       /// <summary>
       /// 获取字符串的消息摘要（MD5加密）
       /// </summary>

       public static string GetHashValue(string msg)
       {
           StringBuilder sb = new StringBuilder();

           MD5 md5 = MD5.Create();

           //将字符串转换成字节数组
           byte[] byteMsg = Encoding.UTF8.GetBytes(msg);

           //计算字节数组的哈希值，即消息摘要
           byte[] byteHashVal = md5.ComputeHash(byteMsg);

           //将摘要转换成16进制表示的字符串，一个字节占2个字符
           foreach (byte bt in byteHashVal)
           {
               sb.Append(bt.ToString("x2"));
           }

           return sb.ToString();

       } // END GetHashValue（）

    }
}
