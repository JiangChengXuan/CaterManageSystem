using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
    /// <summary>
    /// 会员类型
    /// </summary>
  public   class MemberTypeInfo
    {
        

        public int MId { get; set; }
        public string MTitle { get; set; }
        public decimal MDiscount { get; set; }

        public bool MIsDelete { get; set; }

    }
}
