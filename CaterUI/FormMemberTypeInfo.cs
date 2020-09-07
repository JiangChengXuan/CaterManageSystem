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
using CaterBLL.MemberTypeInfoPackage;
namespace CaterUI
{
    public partial class FormMemberTypeInfo : Form
    {
        public FormMemberTypeInfo()
        {
            InitializeComponent();
        }

        public Action LoadParentHandle;
        private List<MemberTypeInfo>memberTypeInfoList=new List<MemberTypeInfo>();
        private MemberTypeInfoBAL bal = new MemberTypeInfoBAL();
        private int editID = -1;

        private void FormMemberTypeInfo_Load(object sender, EventArgs e)
        {
            LoadMemberTypeInfoList();
            LoadDiscountInfo();
        }


        /// <summary>
        /// 加载所有会员类型信息
        /// </summary>
        private void LoadMemberTypeInfoList()
        {
            memberTypeInfoList = bal.GetMemberTypeInfoList();
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = memberTypeInfoList;
            dgvList.Rows[0].Selected = false;

            LoadParentHandle();//执行父窗体的委托函数
        } // END GetMemberTypeInfoList（）


        /// <summary>
        /// 加载折扣信息到下拉框
        /// </summary>
        private void LoadDiscountInfo()
        {
            comboBoxDiscount.DataSource = bal.GetDiscountInfo();
            comboBoxDiscount.ValueMember = "ID";
            comboBoxDiscount.DisplayMember = "Value";
        }


        /// <summary>
        /// 设置单元格数据格式化为特定形式
        /// </summary>
        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //折扣列
            if (e.ColumnIndex==2)
            {
                decimal discountNum = Convert.ToDecimal(e.Value);
                discountNum = discountNum * 10;
                DiscountType type = (DiscountType)discountNum;
                e.Value = type.ToString();
            }

        }


        /// <summary>
        /// （添加 or 修改）会员类型信息
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            MemberTypeInfo data = new MemberTypeInfo();
            data.MDiscount = Convert.ToDecimal(comboBoxDiscount.SelectedValue);
            data.MId = editID;
            data.MTitle = txtTitle.Text.Trim();

            string msg = string.Empty;
            bool result = false;
            if (editID<0) //新增
            {
                result=bal.AddMemberTypeInfo(data, out msg);
            }
            else if  (editID>0) //修改
            {
                result = bal.Edit(data,out msg);
            }

            MessageBox.Show(msg, "提示");

            if (result) // 成功
            {
                //刷新页面
                FormReset();
                LoadMemberTypeInfoList();
            }

 

        } // END dgvList_CellFormatting（）


        /// <summary>
        /// 窗体表达重置
        /// </summary>
        private void FormReset()
        {
            editID = -1;
            txtId.Text = "添加时无编号";
            txtTitle.Text = string.Empty;
            btnSave.Text = "添加";
            comboBoxDiscount.SelectedIndex = 0;
        }// FormReset（）


        /// <summary>
        /// 双击表格行后将行数据绑定到表单上，用于修改操作
        /// </summary>
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
                //获取选中行的主键ID
            int mid = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells[0].Value);

            //查询出主键ID对应的对象
         MemberTypeInfo data=  memberTypeInfoList.SingleOrDefault(s => s.MId == mid);

            //将对象数据绑定到表单上用于修改操作
         btnSave.Text = "修改";
         txtId.Text = data.MId.ToString();
         editID = data.MId;
         txtTitle.Text = data.MTitle;

            //选中折扣绑定到下拉框
         for (int i = 0; i < comboBoxDiscount.Items.Count; i++)
         {
             KeyValue kv = comboBoxDiscount.Items[i] as KeyValue;
             if (kv == null) continue;

             decimal temp = Convert.ToDecimal(kv.ID) / 10;
             if (temp == data.MDiscount)
             {
                 comboBoxDiscount.SelectedIndex = i;
             }

         }// for

        }


        /// <summary>
        /// 删除选中行数据
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            //判断是否选中行
            if (dgvList.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择你要删除的行", "提示");
                return;
            }

            //删除确认
          DialogResult confirm=  MessageBox.Show("你确定要删除吗？", "提示", MessageBoxButtons.YesNo);
          if (confirm != DialogResult.Yes) return;
  
            //获取会员类型主键
          int mid = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);

          string msg = string.Empty;
         bool result=  bal.Delete(mid,out msg);

           MessageBox.Show(msg,"提示");

           if (result)
           {
               LoadMemberTypeInfoList();
           }
        }  // END dgvList_CellDoubleClick（）


    }
}
