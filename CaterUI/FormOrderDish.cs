using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaterModel.DishInfoModel;
using CaterBLL.DishInfoPackage;
using CaterModel.Enum;
using CaterBLL.DishTypeInfoPackage;
using CaterModel.DishTypeInfoModel;
using CaterCommon;
using CaterBLL;
using CaterModel.DataModel;
namespace CaterUI
{
    public partial class FormOrderDish : Form
    {
        private FormOrderDish()
        {
            InitializeComponent();
        }

        private DishInfoBAL bal = new DishInfoBAL();
        private ShoppingCartBAL shoppingCartBAL = new ShoppingCartBAL();
        private OrderDetailBAL orderDetailBAL = new OrderDetailBAL();

        List<DishInfoShow> dishInfoList = new List<DishInfoShow>();
        List<DishTypeInfo> dishInfoTypeList = new List<DishTypeInfo>();
        List<ShoppingCart> ShoppingCartList = new List<ShoppingCart>();

        public long OrderID { get; set; }

        /// <summary>
        /// 点菜窗体加载
        /// </summary>
        private void FormOrderDish_Load(object sender, EventArgs e)
        {
 
            LoadDishInfoType();
            LoadDishInfoList();
            LoadShoppingCart(this.OrderID);  //加载订单的购物车菜品信息
        }

          
        /// <summary>
        /// 加载当前订单点的菜品信息
        /// </summary>
        /// <param name="orderId"></param>
        private void LoadShoppingCart(long orderId)
        {
            #region Step1-数据查询

            //根据订单编号查询购物车菜品信息
            ShoppingCartList = shoppingCartBAL.GetListBy(orderId);     
            #endregion



            #region Step2-关联信息查询

            //查询出菜品的关联信息，并生成用于绑定购物车表格的数据源        
            var list = from datas in ShoppingCartList
                       select new
                       {
                           DTitle = dishInfoList.SingleOrDefault(s => s.DId == datas.DishId).DTitle,
                           DPrice = datas.Count * dishInfoList.SingleOrDefault(s => s.DId == datas.DishId).DPrice,
                           SCId = datas.SCId,
                           Count = datas.Count,
                           IsConfirm = orderDetailBAL.DishIsConfirm(datas.OrderId,datas.DishId)
                       };

            if (list == null || list.Count() <= 0) return; 

            #endregion

            #region Step3-控件绑定

            dgvOrderDetail.AutoGenerateColumns = false;
            dgvOrderDetail.DataSource = list.ToList();
            if (dgvOrderDetail.Rows.Count > 0)
            {
                dgvOrderDetail.Rows[0].Selected = false;
            } 

            #endregion

            //Step4-计算并显示消费总金额
            lblMoney.Text = list.Sum(s => s.DPrice).ToString();

        } // END LoadShoppingCart（）


        /// <summary>
        /// 加载菜品分类信息到下拉框
        /// </summary>
        private void LoadDishInfoType()
        {
            DishTypeInfoBAL balType = new DishTypeInfoBAL();
            dishInfoTypeList = balType.GetList();
            dishInfoTypeList.Insert(0, new DishTypeInfo { DId = -1, DTitle = "全部" });
            
            //查询的下拉框  
            ddlType.DataSource = dishInfoTypeList;
            ddlType.DisplayMember = "DTitle";
            ddlType.ValueMember = "DId";
 

        } // END LoadDishInfoType（）

        /// <summary>
        /// 加载菜品列表信息
        /// </summary>
        public void LoadDishInfoList()
        {
            string firstName = txtTitle.Text.Trim().ToUpper();

            Dictionary<string, string> paras = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(firstName))  //菜品首字母
            {
                paras.Add("DChar", firstName);
            }
            if (ddlType.SelectedIndex != 0) //菜品分类
            {
                paras.Add("DTypeId", ddlType.SelectedValue.ToString());
            }
            

            dishInfoList = bal.GetList(paras);
            dgvAllDish.AutoGenerateColumns = false;
            dgvAllDish.DataSource = dishInfoList;
            if (dgvAllDish.Rows.Count > 0)
            {
                dgvAllDish.Rows[0].Selected = false;
            }

        }

        private void dgvAllDish_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3) //菜品分类名称
            {
                string[] data = e.Value.ToString().Split(new char[] { '@' });
                string typeID = data[0];
                string typeName = data[1];
                e.Value = typeName;
                dgvAllDish.Rows[e.RowIndex].Tag = typeID;
            }
        }


        /// <summary>
        /// 选择下拉框 中的菜品分类进行查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDishInfoList();
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            LoadDishInfoList();
        }


        /// <summary>
        /// 双击表格单元格——进行点菜
        /// </summary>
        private void dgvAllDish_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
                    //获取选中行的菜品信息
            int dishID = Convert.ToInt32(dgvAllDish.Rows[e.RowIndex].Cells[0].Value);  //菜品ID
         

            //数据操作，订单的详情表新增数据
            CaterMessage msgEnum= CaterMessage.Default;
           bool result= shoppingCartBAL.OrderDishes(this.OrderID, dishID,out msgEnum);

           MessageBox.Show(msgEnum.ToDesc());
           if (result) // 点菜成功
           {
                //刷新订单详情信息
               LoadShoppingCart(this.OrderID);
           }

        }// END LoadDishInfoList（）

     

        /// <summary>
        /// 点击下单按钮——进行点菜提交，处理订单详情信息
        /// </summary>
        private void btnOrder_Click(object sender, EventArgs e)
        {
            //1-检查购物车是否存在菜品
            if (dgvOrderDetail.Rows.Count<=0)
            {
                MessageBox.Show("你没有点任何的菜品，请求选择菜品后再进行下单");
                return;
            }

              decimal total=Convert.ToDecimal(lblMoney.Text); //根据当前选择的菜品价格计算的消费总金额
              CaterMessage msgEnum = CaterMessage.Default;

            //执行数据操作
              bool result = orderDetailBAL.SubmitDishes(ShoppingCartList, total, out msgEnum);

              MessageBox.Show(msgEnum.ToDesc());

              if (result) //刷新订单菜品详情信息
              {
                  LoadShoppingCart(this.OrderID);
              }

        }// END btnOrder_Click()


        /// <summary>
        /// 退菜（删掉已选择或已下单的菜品）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
 
            #region Step-1-检查选中行
            if (dgvOrderDetail.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要删除的菜品");
                return;
            } 
            #endregion


            #region Step-2-确认删除
            DialogResult dr = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.YesNo);
            if (dr != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            } 
            #endregion

            #region Step-3-判断菜品是否已经下单

            long scId = Convert.ToInt64(dgvOrderDetail.SelectedRows[0].Cells[0].Value);
            var shopDish = ShoppingCartList.SingleOrDefault(s => s.SCId == scId);

            //判断菜品是否已经下单
            string isConfirm = orderDetailBAL.DishIsConfirm(this.OrderID, shopDish.DishId);
            if (isConfirm == "是")
            {
                MessageBox.Show("该菜品已经下单厨房正在制作不能进行删除，如果要强行删除请使用退菜功能");
                return;
            }

            #endregion

            int delMaxNum = Convert.ToInt32(shopDish.Count); //删除数量的上限
       
            //显示出输入数量的窗体，并在该窗体上进行删除
            FormOrderDetailDel frm = new FormOrderDetailDel();
            frm.DeleteDishHandle += DeleteDish;
            frm.delMaxNum = delMaxNum;
            DialogResult frmDr= frm.ShowDialog();

          

        }    //end btnRemove_Click（）

        /// <summary>
        ///  删除购物车中某个菜品
        /// </summary>
        private void DeleteDish(int delNum)
        {

            #region Step-1-操作数据获取

            //购物车ID
            long scId = Convert.ToInt64(dgvOrderDetail.SelectedRows[0].Cells[0].Value);

            var shopDish=   ShoppingCartList.SingleOrDefault(s => s.SCId ==scId);
            long nowNum = shopDish.Count;        //菜品选购数量

            #endregion
 
            #region Step-3-执行数据操作

            CaterMessage msg = CaterMessage.Default;
            bool result = shoppingCartBAL.Delete(scId, delNum, nowNum, out msg);

            MessageBox.Show(msg.ToDesc()); 

            #endregion

            //Step-4-处理成功刷新购物车
            if (result) LoadShoppingCart(this.OrderID);
         
        } // END DeleteDish（）

    }
}
