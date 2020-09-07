using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel.MenuModel
{
 public partial  class MenuInfo
    {
 

        public int Mid { get; set; }
        public string MenuName { get; set; }
        public int MenuParent { get; set; }

        public string MenuImage { get; set; }
        public string MenuPath { get; set; }
    }
}
