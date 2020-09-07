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
namespace CaterUI
{
    public partial class FormDishInfo : Form
    {
        private FormDishInfo()
        {
            InitializeComponent();

         
        }
        private DishInfoBAL bal = new DishInfoBAL();
        List<DishInfoShow> dishInfoList = new List<DishInfoShow>();
        List<DishTypeInfo> dishInfoTypeList = new List<DishTypeInfo>();
        private long editID = -1;
        private int editIndex = -1;

        private void FormDishInfo_Load(object sender, EventArgs e)
        {
            LoadDishInfoType();
            LoadDishInfoList();
        }


        /// <summary>
        /// 加载菜品列表信息
        /// </summary>
        public void LoadDishInfoList()
        {
            Dictionary<string, string> paras = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(txtTitleSearch.Text.Trim()))
            {
                paras.Add("DTitle",txtTitleSearch.Text.Trim());
            }
            if (ddlTypeSearch.SelectedIndex!=0)
            {
                 paras.Add("DTypeId",ddlTypeSearch.SelectedValue.ToString());
            }


            dishInfoList = bal.GetList(paras);
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = dishInfoList;
            if (dgvList.Rows.Count > 0)
            {
                dgvList.Rows[0].Selected = false;
            }

        } // END LoadDishInfoList（）


        /// <summary>
        /// 格式化表格单元格数据
        /// </summary>
        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex==2) //菜品分类名称
            {
                string[] data = e.Value.ToString().Split(new char[] { '@' });
                string typeID = data[0];
                string typeName = data[1];
                e.Value = typeName;
                dgvList.Rows[e.RowIndex].Tag = typeID;
            }


        } // END LoadDishInfoList（）


        /// <summary>
        /// 加载菜品分类信息到下拉框
        /// </summary>
        private void LoadDishInfoType()
        {
            DishTypeInfoBAL balType = new DishTypeInfoBAL();
            dishInfoTypeList = balType.GetList();
            dishInfoTypeList.Insert(0, new DishTypeInfo { DId = -1, DTitle = "全部" });

              //查询的下拉框  
            ddlTypeSearch.DataSource = dishInfoTypeList;
            ddlTypeSearch.DisplayMember = "DTitle";
            ddlTypeSearch.ValueMember = "DId";

            DishTypeInfo[] copyData = new DishTypeInfo[dishInfoTypeList.Count];
            dishInfoTypeList.CopyTo(copyData);
            var ddlTypeAddData = copyData.ToList();
            ddlTypeAddData.Insert(0, new DishTypeInfo { DId = -1, DTitle = "请选择" });
            //新增的下拉框
            ddlTypeAdd.DataSource = ddlTypeAddData;
            ddlTypeAdd.DisplayMember = "DTitle";
            ddlTypeAdd.ValueMember = "DId";

        } // END LoadDishInfoType（）

        private void txtTitleSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDishInfoList();
        }

        private void ddlTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDishInfoList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtTitleSearch.Text = string.Empty;
            ddlTypeSearch.SelectedIndex = 0;
            LoadDishInfoList();
        }  



        /// <summary>
        /// 取消表单输入
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FormReset();
        }

        /// <summary>
        /// 添加菜品信息
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //封装数据对象
            DishInfo data = new DishInfo();
            data.DId = editID;
            data.DTitle = txtTitleSave.Text;

            data.DTypeId = Convert.ToInt32(ddlTypeAdd.SelectedValue);
            data.DPrice = Convert.ToDecimal(txtPrice.Text);
            data.DChar = txtChar.Text;

            CaterMessage msgEnum = CaterMessage.Default;
            bool result = bal.AddOrUpdate(data, out msgEnum);

            string msg = ReflectHelper.GetFieldDescriptionAttr<CaterMessage>(msgEnum.ToString());
            MessageBox.Show(msg);

            if (result)
            {
                LoadDishInfoList();
                this.FormReset();
                editID = -1;
            }

            if (editIndex>0)
            {
                dgvList.Rows[editIndex].Selected = true;
                editIndex = -1;
            }

        }


        /// <summary>
        /// 双击表格选中行，将数据绑定到表单上用于修改
        /// </summary>
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int did = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells[0].Value);
            DishInfoShow data = dishInfoList.SingleOrDefault(s => s.DId == did);

            editID = data.DId;
            editIndex = e.RowIndex;
            txtId.Text = data.DId.ToString();
            txtTitleSave.Text = data.DTitle;
            ddlTypeAdd.Text = data.DTypeTitle.Split('@')[1]; ;
            txtPrice.Text = data.DPrice.ToString();
            txtChar.Text = data.DChar;
            btnSave.Text = "修改";

        }

        /// <summary>
        /// 菜品名称输入框失去焦点后生成拼音首字母
        /// </summary>
        private void txtTitleSave_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitleSave.Text))
            {
                return;
            }

          string py=  PinyinHelper.GetPinyinInitial(txtTitleSave.Text.Trim());
          txtChar.Text = py;
        }

        /// <summary>
        /// 弹出菜品分类管理窗体
        /// </summary>
        private void btnAddType_Click(object sender, EventArgs e)
        {
            FormDishTypeInfo frm = new FormDishTypeInfo();
            DialogResult dr= frm.ShowDialog();

            if (dr== System.Windows.Forms.DialogResult.OK)
            {
                LoadDishInfoType();
                LoadDishInfoList();
            }

            
        }

        /// <summary>
        /// 删除菜品
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            //是否选中行
            if (dgvList.SelectedRows.Count<=0)
            {
                MessageBox.Show("请选择你要删除的行");
                return;
            }

            DialogResult dr = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.YesNo);

            if (dr!= System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            //获取删除主键
            int dId = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);

            //执行
            CaterMessage msgEnum = CaterMessage.Default;
            bool result = bal.Delete(dId,out msgEnum);
            string msg = ReflectHelper.GetFieldDescriptionAttr<CaterMessage>(msgEnum.ToString());
            MessageBox.Show(msg);

            if (result)
            {
                LoadDishInfoList(); //刷新
            }

        }  // END btnSave_Click（）

      

    }
}
