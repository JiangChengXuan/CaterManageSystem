using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
    public class KeyValue
    {
        public int ID { get; set; }
        public string Value { get; set; }
    }

    public partial class ManagerInfo
    {

        public enum ManagerType
        {
            经理 = 1, //经理
            店员 = 0 //店员
        }

        public int MId { get; set; }

        public string MName { get; set; }
        public string MPwd { get; set; }
        public string MType { get; set; }

    }
}
