using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaterModel.MemberInfoModel;
using CaterBLL.MemberInfoPackage;
using CaterModel.Enum;
using CaterBLL.MemberTypeInfoPackage;
using CaterModel;
namespace CaterUI
{
     
    public partial class FormMemberInfo : Form
    {
        private FormMemberInfo()
        {
            InitializeComponent();
        }

        private List<MemberInfoTableShow> list = new List<MemberInfoTableShow>();
        private MemberInfoBAL bal = new MemberInfoBAL();
        private int editID = -1; //修改操作时存储会员主键ID，并且用于修改或新增操作的判断
        private int editIndex = -1;

        private void FormMemberInfo_Load(object sender, EventArgs e)
        {
            LoadMemberInfo();
            LoadMemberType();
        }  // FormMemberInfo_Load（）


        /// <summary>
        /// 加载会员信息
        /// </summary>
        private void LoadMemberInfo(FormLoadType loadType= FormLoadType.All)
        {
            //组装查询条件
            Dictionary<string, string> paras = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(txtNameSearch.Text))
            {
                paras.Add("MName", txtNameSearch.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtPhoneSearch.Text))
            {
                paras.Add("MPhone", txtPhoneSearch.Text.Trim());
            }

            if (loadType ==FormLoadType.All) //查所有
            {
                paras = null;
            }

            list = bal.GetDataList(paras);
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = list;

            if (list.Count>0)
            {
                  dgvList.Rows[0].Selected = false;
            }

        } // END LoadMemberInfo（）

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            LoadMemberInfo(FormLoadType.where);
        }

        /// <summary>
        /// 显示所有会员信息
        /// </summary>
        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            LoadMemberInfo();
            txtNameSearch.Text = string.Empty;
            txtPhoneSearch.Text = string.Empty;
        }

        /// <summary>
        /// 打开会员类型管理窗体
        /// </summary>
        private void btnAddType_Click(object sender, EventArgs e)
        {
            /*
             * 【使用委托使两个窗体直接进行操作】
             *主窗体创建委托变量存储用于刷新会员类型下拉框的方法，然后将该变量传给子窗体(会员类型管理窗体)的委托字段
             * 子窗体每当完成操作时就对父窗体的下拉框进行刷新
             */

            Action action_LoadMemberType = () => {
                LoadMemberInfo();
                LoadMemberType();
            };
          
            FormMemberTypeInfo frm = new FormMemberTypeInfo();
            frm.LoadParentHandle = action_LoadMemberType;
            frm.ShowDialog();




        } // btnSearchAll_Click（）


        /// <summary>
        /// 加载所有会员类型到下拉框
        /// </summary>
        private void LoadMemberType()
        {
            MemberTypeInfoBAL bal = new MemberTypeInfoBAL();
            List<MemberTypeInfo> list = bal.GetMemberTypeInfoList();

            list.Insert(0, new MemberTypeInfo() { MId = -1, MTitle = "请选择" });
            ddlType.DataSource = list;
            ddlType.ValueMember = "MId";
            ddlType.DisplayMember = "MTitle";
          
            ddlType.SelectedIndex = 0;

        }// END LoadMemberType（）


        /// <summary>
        /// 添加会员
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //数据效验

            //封装数据对象 
             MemberInfo data = new  MemberInfo();
            data.MId = editID;
            data.MName = txtNameAdd.Text;
            data.MTypeId = Convert.ToInt64(ddlType.SelectedValue);
            data.MPhone = txtPhoneAdd.Text;
            data.MMoney = Convert.ToDecimal(txtMoney.Text);

            string msg = string.Empty;
            bool result = false;
            if (editID==-1) //新增
            {
                result=bal.Add(data, out msg);
            }
            else //修改
            {
                result = bal.Edit(data, out msg);
               
            }

            MessageBox.Show(msg, "提示");
           
            if (result)
            {
                //刷新表格
                LoadMemberInfo();

                //重置表单
                FormReset(() => {
                    if (editIndex>-1)
                    {
                         dgvList.Rows[editIndex].Selected = true;
                         editIndex = -1;
                    }
                });
            }

             
        }  // END btnSave_Click（）


        /// <summary>
        /// 重置表单
        /// </summary>
        private void FormReset(Action callBack=null)
        {
            btnSave.Text = "添加";
            txtId.Text = "添加时无编号";
            txtNameAdd.Text = string.Empty;
            ddlType.SelectedIndex = 0;
            txtPhoneAdd.Text = string.Empty;
            txtMoney.Text = string.Empty;
            editID = -1;

            callBack();
        }

        /// <summary>
        /// 双击行绑定数据到表单，用于修改
        /// </summary>
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Text = "修改";
            //获取选中行的主键
            int mid = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells[0].Value);
            editID = mid;
            editIndex = e.RowIndex;
            //使用主键查出对象
         MemberInfoTableShow data=  list.SingleOrDefault(s => s.MId == mid);

            //数据绑定
         txtId.Text = mid.ToString();
         txtNameAdd.Text = data.MName;
         ddlType.Text = data.MTitle;
         txtPhoneAdd.Text = data.MPhone;
         txtMoney.Text = data.MMoney.ToString();

        }

        //取消表单操作
        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormReset();
        }

        /// <summary>
        /// 删除会员
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {

            if (  dgvList.SelectedRows.Count<=0)
            {
                MessageBox.Show("请选中你要删除的行");
                return;
            }

          DialogResult dr=  MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.YesNo);

          if (dr!= System.Windows.Forms.DialogResult.Yes)
          {
              return;
          }

          string msg = string.Empty;
          bool result = false;
          int mid = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
          result=bal.Delete(mid, out msg);

          MessageBox.Show(msg);

          if (result)
          {
               LoadMemberInfo();
          }

        }// END FormReset()


    }
}
