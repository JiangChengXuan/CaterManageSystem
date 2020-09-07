using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaterModel;
using CaterModel.Enum;
using CaterBLL;
using CaterModel.DataModel;
using CaterBLL.MemberInfoPackage;
using CaterModel.MemberInfoModel;
using CaterCommon;
namespace CaterUI
{
    /// <summary>
    /// 结账付款窗体
    /// </summary>
    public partial class FormOrderPay : Form
    {
        public FormOrderPay()
        {
            InitializeComponent();
        }

        private OrderInfoBAL orderInfoBAL = new OrderInfoBAL();
        private OrderDetailBAL OrderDetailBAL = new OrderDetailBAL();

        private OrderInfo OrderInfoData = null;
        private MemberInfoTableShow memberInfo  =null;

        /// <summary>
        /// 餐桌编号
        /// </summary>
        public int TableId { private get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public int OrderId { private get; set; }

     /// <summary>
        /// 结账前进行条件判断，条件符合的话则打开付款窗体，并获取订单信息
     /// </summary>
     /// <returns>是否符合条件</returns>
        private  bool GetOrderId()
        {
            //获取主窗体对象
            if (Application.OpenForms["FormMain"]  is FormMain)
            {
                FormMain frmian = Application.OpenForms["FormMain"] as FormMain;
                ListView listView=   frmian.tcHallInfo.SelectedTab.Controls[0] as ListView;

                if (listView.SelectedItems.Count<=0)
                {
                    MessageBox.Show("请选择需要结账的餐桌");
                    return false;
                }

                if (listView.SelectedItems[0].ImageIndex==0) // 餐桌为空闲状态
                {
                    MessageBox.Show("空闲餐桌无需进行结账");
                    return false;
                }

                //赋值
                this.OrderId = Convert.ToInt32(listView.SelectedItems[0].Name);
                this.TableId = Convert.ToInt32(listView.SelectedItems[0].Tag);

                //如果该订单没任下单过任何菜品（订单详情表中没有菜品）则不能进行结账
               var checkData= OrderDetailBAL.GetListBy(OrderId);
               if (checkData==null||checkData.Count<=0)
               {
                    MessageBox.Show("该订单没有下单过任何菜品，无需进行结账");
                    return false;
               }

            }

            return true;
        } // END GetOrderId()


        /// <summary>
        /// 窗体加载
        /// </summary>
        private void FormOrderPay_Load(object sender, EventArgs e)
        {
   
            //加载付款信息
            LoadPayInfo();
        }

        /// <summary>
        /// 加载付款信息
        /// </summary>
        private void LoadPayInfo()
        {
            //获取订单信息
            this.OrderInfoData = orderInfoBAL.GetOrderInfoById(this.OrderId);
            lblPayMoney.Text = OrderInfoData.OMoney.ToString(); //消费金额
            lblPayMoneyDiscount.Text = OrderInfoData.OMoney.ToString(); //应收金额

        }// END LoadPayInfo（）

        /// <summary>
        /// 选择会员付款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbkMember_CheckedChanged(object sender, EventArgs e)
        {
            //会员信息的使用  根据 是否选中会员的按钮开启或关闭
            gbMember.Enabled = cbkMember.Checked;

            //取消会员付款
            if (!gbMember.Enabled)
            {
                this.FormReset();
                memberInfo = null;
                cbkMoney.Checked = false;
                lblPayMoneyDiscount.Text = this.OrderInfoData.OMoney.ToString();
            }
           



        }

     
   


        /// <summary>
        /// 根据输入会员信息（手机号或者会员编号） 
        /// 根据输入的信息获取会员对象，并显示会员付款信息
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {

            #region Step1-获取输入信息

            if (string.IsNullOrEmpty(txtId.Text.Trim())&&string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                MessageBox.Show("请输入会员编号或者手机号");
                return;
            }

            //获取输入信息
            long mId=0;
            string phone=string.Empty;
            if (!string.IsNullOrEmpty(txtId.Text.Trim()))
            {
                mId = Convert.ToInt64(txtId.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                 phone = txtPhone.Text.Trim();
            }
        
         
            #endregion

            #region Step2-根据输入信息查询出会员对象

            //根据输入信息查询出会员对象
            MemberInfoBAL memberInfoBAL = new MemberInfoBAL();
            this.memberInfo = memberInfoBAL.GetMemberInfoByIdOrPhone(mId, phone);

            if (memberInfo == null)
            {
                MessageBox.Show("未查到会员信息");
                return;
            }

            #endregion

            #region Step3-会员结账信息输出

            //将会员涉及到结账的信息输出到界面
            lblMoney.Text = memberInfo.MMoney.ToString();
            lblTypeTitle.Text = memberInfo.MTitle;
            lblDiscount.Text = Convert.ToInt32(memberInfo.MDiscount * 10) + "折";  // 折扣

            //根据会员折扣计算出应收金额
            decimal account = this.OrderInfoData.OMoney * memberInfo.MDiscount;
            lblPayMoneyDiscount.Text = account.ToString();

            //当会员余额小于消费的总金额，禁用余额付款
            if (memberInfo.MMoney<account)
            {
                cbkMoney.Checked = false;
            }

            #endregion

        }


        /// <summary>
        /// 暂不结账
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 点击按钮进行结账
        /// </summary>
        private void btnOrderPay_Click(object sender, EventArgs e)
        {
            //如果选择会员结账，但是未查到会员信息时，拒绝进行结账
            if (memberInfo == null && cbkMember.Checked)
            {
                MessageBox.Show("没有指派任何会员信息，无法进行结账");
                return;
            }

            bool IsBalancePay = cbkMoney.Checked;
            CaterMessage msg= CaterMessage.Default;
            bool result = orderInfoBAL.OrderCheckOut(this.OrderInfoData, this.memberInfo, IsBalancePay, out msg);

 
            if (result) //执行成功
            {
                FormMain frmian = Application.OpenForms["FormMain"] as FormMain;
                frmian.LoadOperationInfo();
                this.Close();
            }

            MessageBox.Show(msg.ToDesc());

        }  


    }
}
