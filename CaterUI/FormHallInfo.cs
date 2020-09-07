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
using CaterCommon;
namespace CaterUI
{
    public partial class FormHallInfo : Form
    {
        public FormHallInfo()
        {
            InitializeComponent();
        }

  

        private HallInfoBAL bal = new HallInfoBAL();
        private List<HallInfo> HallInfoList = new List<HallInfo>();
        private long editID = -1;
        private int editIndex = -1;

        public event Action LoadParentHandle;

        private void FormHallInfo_Load(object sender, EventArgs e)
        {
            LoadHallInfoList();
        }

        /// <summary>
        /// 加载包厢信息到表格控件
        /// </summary>
        private void LoadHallInfoList()
        {
            HallInfoList = bal.GetList();
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = HallInfoList;
            if (dgvList.Rows.Count>0)
            {
                dgvList.Rows[0].Selected = false;
            }

        }// END LoadHallInfoList（）


        /// <summary>
        /// 添加 或 修改
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //封装数据对象
            HallInfo data = new HallInfo();
            data.HId = editID;
            data.HIsDelete = false;
            data.HTitle = txtTitle.Text;

            CaterMessage msgEnum = CaterMessage.Default;
            bool result = bal.AddOrUpdate(data,out msgEnum);

            string msg = ReflectHelper.GetFieldDescriptionAttr<CaterMessage>(msgEnum.ToString());
            MessageBox.Show(msg);

            if (result)
            {
                LoadHallInfoList();
                this.FormReset();
                LoadParentHandle();
            
            }

            if (editIndex>0)
            {
                dgvList.Rows[editIndex].Selected = true;
                editIndex = -1;
                editID = -1;
            }

        }


        /// <summary>
        /// 双击选中要修改的数据行，将行数据绑定到表单
        /// </summary>
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
                    //获取主键
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);

            //根据主键获取数据对象
            HallInfo data = HallInfoList.SingleOrDefault(s => s.HId == id);
            editID = data.HId;
            editIndex = e.RowIndex;
            txtId.Text = data.HId.ToString();
            txtTitle.Text = data.HTitle;
            btnSave.Text = "修改";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            editID = -1;
            editIndex = -1;
            this.FormReset();
        }

        /// <summary>
        /// 删除包厢信息
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
                //是否选中行
            if (dgvList.SelectedRows.Count<=0)
            {
                MessageBox.Show("请选择要删除的行");
                return;
            }

            //确认删除
            DialogResult dr = MessageBox.Show("确认要删除吗？", "提示", MessageBoxButtons.YesNo);
            if (dr!= System.Windows.Forms.DialogResult.Yes)
            {
                     return;
            }

            //获取要删除的ID
            int hId = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            CaterMessage msgEnum = CaterMessage.Default;
            bool result = bal.Delete(hId, out msgEnum);

            string msg = ReflectHelper.GetFieldDescriptionAttr<CaterMessage>(msgEnum.ToString());
            MessageBox.Show(msg);

            if (result)
            {
                LoadHallInfoList(); //刷新表格
                LoadParentHandle();
            
            }

        }  


    }
}
