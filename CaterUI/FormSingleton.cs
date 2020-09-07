using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CaterUI
{
    /*
     *此文件实现部分窗体的单例模式
     */

    public partial class FormMemberInfo
    {
        private static FormMemberInfo Instance = null;
        private static object obj = new object();

        public static FormMemberInfo CreateInstance()
        {
            if (Instance == null || Instance.IsDisposed)
            {
                lock (obj)
                {
                    if (Instance == null || Instance.IsDisposed)
                    {
                        Instance = new FormMemberInfo();
                    }
                }
            }
            return Instance;
        }

    }  // END FormMemberInfo CLASS

    public partial class FormDishInfo
    {
        private static FormDishInfo Instance = null;
        private static object obj=new object();

            public static FormDishInfo CreateInstance()
            {
                if (Instance == null || Instance.IsDisposed)
                {
                    lock (obj)
                    {
                        if (Instance == null || Instance.IsDisposed)
                        {
                            Instance = new FormDishInfo();
                        }
                    }
                }
                return Instance;
            }

    }  // END class FormDishInfo


    /// <summary>
    /// 餐桌管理
    /// </summary>
    public partial class FormTableInfo
    {
        private static FormTableInfo Instance = null;
        private static object obj = new object();

        public static FormTableInfo CreateInstance()
        {
            if (Instance==null||Instance.IsDisposed)
            {
                lock (obj)
                {
                    if (Instance == null || Instance.IsDisposed)
                    {
                        Instance = new FormTableInfo();
                    }
                }
            }


            return Instance;
        }  // END CreateInstance（）


    }  //end CLASS FormTableInfo()

    /// <summary>
    /// 点菜窗体类
    /// </summary>

    public partial class FormOrderDish
    {
        private static FormOrderDish Instance = null;
        private static object obj = new object();

        public static FormOrderDish CreateInstance()
        {
            if (Instance == null || Instance.IsDisposed)
            {
                lock (obj)
                {
                    if (Instance == null || Instance.IsDisposed)
                    {
                        Instance = new FormOrderDish();
                    }
                }
            }
            return Instance;
        }

    }   // END class  FormOrderDish


    public partial class FormOrderPay
    {
        private static FormOrderPay Instance = null;
        private static object obj = new object();

        public static FormOrderPay CreateInstance()
        {
            if (Instance==null||Instance.IsDisposed)
            {
                lock (obj)
                {
                    if (Instance == null || Instance.IsDisposed)
                    {
                        Instance = new FormOrderPay();
                    }
                }
            }

            if ( !Instance.GetOrderId())
            {
                Instance = null;        
            }

            return Instance;
        }




    
    }


}
