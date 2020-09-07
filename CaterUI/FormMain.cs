using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using CaterModel.MenuModel;
using CaterBLL.MenuInfoPackage;
using CaterModel;
using CaterCommon;
using CaterBLL;
using CaterModel.DataModel;
using CaterModel.ViewModel;
using CaterModel.Enum;
namespace CaterUI
{
     

    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        public FormMain(ManagerInfo userInfo)
        {
            InitializeComponent();
            UserInfo = userInfo;
            CreateImageList();
        }

       

        public ManagerInfo UserInfo { get; set; }

       private MenuInfoBAL menuInfoBAL = new MenuInfoBAL();
       private ImageList ImgList = new ImageList();

        //窗体关闭后
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*
             *将当期应用程序退出，而不仅是关闭窗体。
             * 如果只是关闭窗体，那么存在隐藏窗体的情况将无法退出，进程会一直在后台运行。
             */
            Application.Exit();
        }

      

        private void FormMain_Load(object sender, EventArgs e)
        {
            

            LoadMenu(UserInfo.MType);
            LoadOperationInfo();
        }

        /// <summary>
        /// 从项目资源类中加载图片
        /// </summary>
        private Bitmap GetMenuImage(string imgName)
        {
            object obj = Properties.Resources.ResourceManager.GetObject(imgName, Properties.Resources.Culture);
              return (System.Drawing.Bitmap)obj;
        }

        /// <summary>
        /// 添加子菜单
        /// </summary>
        /// <param name="text">要显示的文字，如果为 - 则显示为分割线</param>
        /// <param name="cms">要添加到的子菜单集合</param>
        /// <param name="callback">点击时触发的事件</param>
        /// <returns>生成的子菜单，如果为分隔条则返回null</returns>

        ToolStripMenuItem AddContextMenu(MenuInfo menuInfo,ToolStripItemCollection cms, EventHandler callback)
        {
            //获取菜单图片
            Bitmap img = GetMenuImage(menuInfo.MenuImage);
                  
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Tag = menuInfo;
            tsmi.Image = img;
            if (callback != null) tsmi.Click += callback;
            cms.Add(tsmi);

            return tsmi;
        }

       
        /// <summary>
        /// 根据当前登录用户对应的角色类型加载相应的菜单
        /// </summary>
        private void LoadMenu(string rid)
        {      

            List<MenuInfo> list = menuInfoBAL.GetMenuInfoByRoleID(rid);

            foreach (MenuInfo menu in list)
            {
                AddContextMenu(menu, menuStrip1.Items, Menu_Click);
            }

        } // END LoadMenu（）

        /// <summary>
        /// 菜单点击事件
        /// </summary>
        public void Menu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;

            MenuInfo data = (MenuInfo)menu.Tag;

            if (data.MenuName.Contains("退出"))
            {
                Application.Exit();
                return;
            }

            if (string.IsNullOrEmpty(data.MenuPath))
            {
                MessageBox.Show("菜单没有指定窗体","提示");
                return;
            }

            //反射创建窗体对象并显示
            Form form = MethodInvoke(data.MenuPath, "Show");

            if (form is FormTableInfo)
            {
                FormTableInfo tanleInfo = form as FormTableInfo;
                tanleInfo.RefreshFrmMain += LoadOperationInfo;
            }
             
            

        } // END Menu_Click()


        /// <summary>
        /// 根据类全名调用指定方法
        /// </summary>
        /// <param name="className">类全名（命名空间+类名）</param>
        /// <param name="methodName">方法名</param>
        public Form MethodInvoke(string className, string methodName)
        {
            Type type = Type.GetType(className);

            MethodInfo method = type.GetMethod("CreateInstance");
            Form form = (Form)method.Invoke(null, null);

            if (form!=null)
            {
                form.Show();
            }
 
            return form;
        } // END 

        /// <summary>
        /// 创建餐桌空闲状态的图标
        /// </summary>
        private void CreateImageList()
        {
 
              ImgList= new ImageList();
              ImgList.ImageSize = new Size(64, 64);
              ImgList.ColorDepth = ColorDepth.Depth32Bit;
              Image img1 = GetMenuImage("desk1");
              Image img2 = GetMenuImage("desk2");

              if (img1 != null)
              {
                  ImgList.Images.Add(img1);
              }

              if (img2 != null)
              {
                  ImgList.Images.Add(img2);
              }
        
        }  // END CreateImageList（）

        /// <summary>
        /// 加载餐厅厅包餐桌运营情况
        /// </summary>
        public void LoadOperationInfo()
        {
            //重置
            if (tcHallInfo.TabPages.Count>0)
            {
                tcHallInfo.TabPages.Clear();
            }
 
            

            #region 1.查询获取厅包集合信息
            //查询获取厅包集合信息
            HallInfoBAL hallInfoBAL = new HallInfoBAL();
            List<HallInfo> listHallInfo = hallInfoBAL.GetList(); 
            #endregion

            TableInfoBAL tableInfoBAL = new TableInfoBAL();

            //遍历所有厅包信息
            foreach (HallInfo hall in listHallInfo)  
            {
                //将每个厅包创建为一个TabPage项
                TabPage tp = new TabPage(hall.HTitle);

                //生成ListView控件显示存储厅包下的餐桌信息
                ListView listView = new ListView();
                listView.Dock = DockStyle.Fill;
                listView.LargeImageList = ImgList;
                listView.DoubleClick += Orders_DoubleClick;
                tp.Controls.Add(listView);

                //获取厅包下的所有餐桌信息
                List<TableInfoView> tableInfo = tableInfoBAL.GetList(new Dictionary<string, string>() { { "HTitle", hall.HTitle } });

                //将厅包下的每个餐桌绑定到ListView中显示
                foreach (TableInfoView view in tableInfo)
                {
                    int isFree = view.TIsFree ? 0 : 1; // false取第二张(占用图片)、true取第一张(空闲图片)
                    ListViewItem item = new ListViewItem(view.TTitle, isFree); 
                    item.Tag = view.TId; // 存储餐桌ID，用于开单后修改餐桌占用情况
                    item.Name = view.OrderId <=0 ? string.Empty : view.OrderId.ToString(); //存储订单号
                    listView.Items.Add(item);
                }

                tcHallInfo.TabPages.Add(tp);
            } // END for ListHallInfo

        }// END LoadOperationInfo（）

        /// <summary>
        /// 进行开单操作 ：1.新增订单信息；2.餐桌状态改变
        /// 改善：此处需要使用事务处理。存在问题：例如步骤1下单成功了，但是对应订单的餐桌还是空闲状态，这会导致下一此可能对餐桌进行重复下单。
        /// </summary>
        void Orders_DoubleClick(object sender, EventArgs e)
        {
            ListView item = (ListView)sender;
            long orderID = -1;

            //餐桌处于空闲 ，才能进行下单
            if (item.SelectedItems[0].ImageIndex == 0)
            {
                //封装下单的数据
                OrderInfo data = new OrderInfo();
                data.ODate = DateTime.Now;
                data.IsPay = false;
                data.TableId = Convert.ToInt64(item.SelectedItems[0].Tag);  //餐桌号

                //下单数据操作
                orderID = new OrderInfoBAL().PlaceOrder(data);
                item.SelectedItems[0].Name = orderID.ToString();//存储订单编号

                //下单成功 ,改变餐桌图标状态     
                if (orderID > 0) item.SelectedItems[0].ImageIndex = 1;
               
            }
            else
            {
                string orderId = item.SelectedItems[0].Name;
                if (string.IsNullOrEmpty(orderId))
                {
                    MessageBox.Show("该餐桌没有关联的订单信息无法进行点菜，请联系管理员");
                    return;
                }
                else
                {
                    orderID = Convert.ToInt64(orderId);
                }
            }
         

            //显示“点菜窗体”
         FormOrderDish  formOrderDish =FormOrderDish.CreateInstance();
         formOrderDish.OrderID = orderID;
         formOrderDish.Show();

        } // END tcHallInfo_DoubleClick（）

    }
}
