using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaterModel.ViewModel;
using CaterBLL;
using CaterModel.Enum;
using CaterModel;
using CaterModel.DataModel;
using CaterCommon;
namespace CaterUI
{
    public partial class FormTableInfo : Form
    {
        private FormTableInfo()
        {
            InitializeComponent();
        }
        public event Action RefreshFrmMain; 
        private TableInfoBAL bal = new TableInfoBAL();
        private List<TableInfoView> tableInfoViewList = new List<TableInfoView>();
        private int editId = -1;
        private int editIndex = -1;

        private void FormTableInfo_Load(object sender, EventArgs e)
        {
            LoadTableState();
            LoadHallInfo();
            LoadTableInfo();
        }

        private void LoadTableInfo()
        {
            //查询条件
            Dictionary<string, string> where = new Dictionary<string, string>();
            if (ddlHallSearch.SelectedIndex>0)
            {
                where.Add("HTitle", ddlHallSearch.Text);
            }
            if (ddlFreeSearch.SelectedIndex>0)
            {
                where.Add("TIsFree", ddlFreeSearch.SelectedValue.ToString());
            }
 
            tableInfoViewList = bal.GetList(where);
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = tableInfoViewList;

            if (dgvList.Rows.Count>0)
            {
                dgvList.Rows[0].Selected = false;
            }

        }


        /// <summary>
        /// 加载厅包信息到下拉框
        /// </summary>
        private void LoadHallInfo()
        {
            HallInfoBAL hBal = new HallInfoBAL();
            List<HallInfo> list = hBal.GetList();
 
            //设置用于查询的
            HallInfo defaultObj = new HallInfo { HId = -1, HTitle = "全部" };
            list.Insert(0, defaultObj);
            ddlHallSearch.DataSource = list;
            ddlHallSearch.DisplayMember = "HTitle";
            ddlHallSearch.ValueMember = "HId";


            //设置用于添加的
            defaultObj.HTitle = "请选择";
            HallInfo[] arr = new HallInfo[list.Count];
            list.CopyTo(arr);
            arr.ToList().Insert(0,defaultObj);
            ddlHallAdd.DataSource = arr.ToList();
            ddlHallAdd.DisplayMember = "HTitle";
            ddlHallAdd.ValueMember = "HId";
            
        }  // END LoadHallInfo（）

        /// <summary>
        /// 加载餐桌状态到下拉框
        /// </summary>
        private void LoadTableState()
        {
            List<KeyValue>list= bal.GetTableState();
            list.Insert(0, new KeyValue { ID = -1, Value = "全部" });
            ddlFreeSearch.DataSource = list;
            ddlFreeSearch.DisplayMember = "Value";
            ddlFreeSearch.ValueMember = "ID";
        }   // END LoadTableState（）


        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex==3)
            {
                e.Value = Convert.ToBoolean(e.Value) ? "是" : "否";
            }
        }

        /// <summary>
        /// 选择下拉框的值进行条件查询
        /// </summary>
        private void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTableInfo();
        }

        /// <summary>
        /// 显示全部
        /// </summary>
        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            ddlFreeSearch.SelectedIndex = 0;
            ddlHallSearch.SelectedIndex = 0;
            LoadTableInfo();
        }


        /// <summary>
        /// 添加或修改 餐桌
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
                    //1.封装数据对象
            TableInfo data = new TableInfo();
            data.TId = editId;
            data.TTitle = txtTitle.Text;
            data.THallId = Convert.ToInt32(ddlHallAdd.SelectedValue);
            bool isFree = rbFree.Checked ? true : false;
            data.TIsFree = isFree;

            CaterMessage msgEnum = CaterMessage.Default;
            bool result = bal.AddOrUpdate(data, out msgEnum);

            string msg = msgEnum.ToDesc();
            MessageBox.Show(msg);

            if (result)
            {
                LoadTableInfo();
                this.FormReset();
                RefreshFrmMain();
            }

            if (editId>0)
            {
                dgvList.Rows[editIndex].Selected = true;
                editIndex = -1;
                editId = -1;
            }

        }

        /// <summary>
        /// 重置表单操作
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FormReset();
            editId = -1;
            editIndex = -1;
        }

        /// <summary>
        /// 双击选中要修改的行数据并将数据绑定到表单上
        /// </summary>
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //获取选中主键
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);

            //获取对象
            var data = tableInfoViewList.SingleOrDefault(s=>s.TId==id);

            //绑定数据
            btnSave.Text = "修改";
            txtId.Text = id.ToString();
            editId = id;
            editIndex = e.RowIndex;
            txtTitle.Text = data.TTitle;
            ddlHallAdd.Text = data.HTitle;
            rbFree.Checked = data.TIsFree;
            rbUnFree.Checked = !data.TIsFree;
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            //是否选中行
            if (dgvList.SelectedRows.Count<=0)
            {
                MessageBox.Show("请选中你要删除的行");
                return;
            }

            //确认
            DialogResult dr = MessageBox.Show("确认要删除吗？","提示",MessageBoxButtons.YesNo);

            if (dr!= System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            //数据操作
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            CaterMessage msgEnum = CaterMessage.Default;
            bool result = bal.Delete(id, out msgEnum);
            MessageBox.Show(msgEnum.ToDesc());

            if (result)
            {
                LoadTableInfo();
                RefreshFrmMain();
            }

        }

        /// <summary>
        /// 打开厅包管理窗体
        /// </summary>
        private void btnAddHall_Click(object sender, EventArgs e)
        {
            FormHallInfo frm = new FormHallInfo();
            frm.LoadParentHandle += () => {
                LoadHallInfo();
                LoadTableInfo();
                RefreshFrmMain();
            };
            frm.ShowDialog();
        }  // END btnSave_Click（）


    }
}
