using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.International.Converters.PinYinConverter;
using System.Text.RegularExpressions;
namespace CaterCommon
{
  public  class PinyinHelper
    {

      private const string pattern = "[\u4E00-\u9FA5]+";

        /// <summary>
        /// 获取汉字拼音的首字母
        /// </summary>
        /// <returns></returns>
        public static string GetPinyinInitial(string hanzi)
        {
            string result = string.Empty;
             

            //检验字符串是否是汉字
            if (!Regex.IsMatch(hanzi, pattern))
            {
                return result;
            }

            foreach (char str in hanzi)
            {
                ChineseChar cc = new ChineseChar(str);
                var list = cc.Pinyins;
                result += list[0][0];
            }

            return result;
        } // END GetPinyinInitial（）
         

    }
}
