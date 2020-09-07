using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaterModel.DishTypeInfoModel;
using CaterBLL.DishTypeInfoPackage;
using CaterModel.Enum;
using CaterCommon;
namespace CaterUI
{
    public partial class FormDishTypeInfo : Form
    {
        public FormDishTypeInfo()
        {
            InitializeComponent();
        }

        DishTypeInfoBAL bal = new DishTypeInfoBAL();
        List<DishTypeInfo> list = new List<DishTypeInfo>();
        private int editID = -1;
        private int editedIndex = -1;
        private DialogResult toParent = DialogResult.Cancel;

        private void FormDishTypeInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }


        private void LoadList()
        {
            list = bal.GetList();
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = list;
            if (dgvList.Rows.Count>0)
            {
                dgvList.Rows[0].Selected = false;
            }

        }

        /// <summary>
        /// Add Or Edit
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
                //封装数据对象
            DishTypeInfo data = new DishTypeInfo();
            data.DTitle = txtTitle.Text;
            data.DId = editID;

            CaterMessage msgEnum = CaterMessage.Default;
            bool result = false;
            if (editID==-1) //新增
            {
                result = bal.Add(data, out msgEnum);
            }
            else // 修改
            {
                result = bal.Edit(data, out msgEnum);
            }

            string msg = ReflectHelper.GetFieldDescriptionAttr<CaterMessage>(msgEnum.ToString());
            MessageBox.Show(msg);

            if (result) //操作成功
            {
                LoadList();
                FormRest(() => {
                    if (editedIndex>=0)
                    {
                        dgvList.Rows[editedIndex].Selected = true;
                        editedIndex = -1;
                    }
                });
                toParent = DialogResult.OK;
            }

        } // END LoadList

        /// <summary>
        /// 表单重置
        /// </summary>
        /// <param name="callBack"></param>
        private void FormRest(Action callBack)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = string.Empty;
            editID = -1;
            btnSave.Text = "添加";



            if (callBack!=null)
            {
                callBack();
            }
        }// END FormRest（）

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormRest(null);
            if (dgvList.Rows.Count > 0)
            {
                dgvList.Rows[0].Selected = false;
            }
        }

        /// <summary>
        /// 双击表格行绑定修改数据
        /// </summary>
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells[0].Value);
          var data=  list.SingleOrDefault(s => s.DId == id);

            editedIndex = e.RowIndex;
            editID = id;
            btnSave.Text = "修改";
            txtId.Text = id.ToString();
            txtTitle.Text = data.DTitle;
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
                //判断是否选中删除行
            if ( dgvList.SelectedRows.Count<=0)
            {
                MessageBox.Show("请选中你要删除的行");
                return;
            }

            //确认是否删除
            DialogResult dr = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.YesNo);
            if (dr != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            //获取删除行的主键
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);

            //执行删除
            CaterMessage msgEnum = CaterMessage.Default;
           bool result= bal.Delete(id, out msgEnum);
            string msg = ReflectHelper.GetFieldDescriptionAttr<CaterMessage>(msgEnum.ToString());
            MessageBox.Show(msg);

            if (result)
            {
                LoadList();  //刷新表格
                toParent = DialogResult.OK;
            }

       

        }

        private void FormDishTypeInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = toParent; //告诉父窗体当前窗体进行了数据操作，父窗体需进行数据更新
        } // END btnRemove_Click（）

    }
}
